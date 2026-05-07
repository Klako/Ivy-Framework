using Ivy;
using Ivy.Benchmark.Samples;
using Ivy.Benchmark.Samples.Apps;
using Ivy.Core;
using Ivy.Core.Sync;
using System.Diagnostics;

#if BENCHMARK
Console.WriteLine("BENCHMARKS defined");
#endif

if (args.Length < 1)
{
    throw new Exception("Missing client benchmark dir");
}

var clientBenchmarkDir = args[0];

if (!File.Exists($"{clientBenchmarkDir}/simple.spec.ts"))
{
    throw new Exception($"Cannot find client benchmarking files in {clientBenchmarkDir}");
}

Console.WriteLine($"Using client benchmarking dir: {clientBenchmarkDir}");

// Use core 1 and 2 for this process
if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux())
{
    Process.GetCurrentProcess().ProcessorAffinity = 0b0011;
}

async Task RunBenchmarks(string file,
    string testName,
    Dictionary<string, string> testOptions)
{
    Directory.CreateDirectory($"./results/{file}/{testName}");

    (TreeDifferType, TreeDifferOptions?)[] diffTypeOptions = [
        (TreeDifferType.JsonPatch, null),
        (TreeDifferType.New, new(TreeChildrenDiffer.Linear, false)),
        (TreeDifferType.New, new(TreeChildrenDiffer.Linear, true)),
        (TreeDifferType.New, new(TreeChildrenDiffer.LCS, false)),
        (TreeDifferType.New, new(TreeChildrenDiffer.LCS, true))
    ];

    foreach (var (diffType, treeDifferOptions) in diffTypeOptions)
    {
        // Cleanup resources before running
        GC.Collect();
        GC.WaitForPendingFinalizers();

        var diffTypeName = diffType switch
        {
            TreeDifferType.JsonPatch => "jsonpatch",
            TreeDifferType.New => treeDifferOptions switch
            {
                (TreeChildrenDiffer.Linear, false) => "new_linear",
                (TreeChildrenDiffer.Linear, true) => "new_linear+propdiff",
                (TreeChildrenDiffer.LCS, false) => "new_lcs",
                (TreeChildrenDiffer.LCS, true) => "new_lcs+propdiff",
                _ => "err"
            },
            _ => "err"
        };

        Console.WriteLine($"Benchmarking {file}/{testName} using {diffTypeName}");

        List<(double TotalTime, double DiffTime)> serverSideTimings = new(1000);

        Func<double, double, int> reportCallback = (totalTime, diffTime) =>
        {
            serverSideTimings.Add((totalTime, diffTime));
            return 0;
        };

        var widgetTreeOptions = new WidgetTreeOptions(diffType, treeDifferOptions, reportCallback);

        var serverArgs = new ServerArgs()
        {
            WidgetTreeOptions = widgetTreeOptions
        };

        var cts = new CancellationTokenSource();

        var server = new Ivy.Server(serverArgs);
        server.UseCulture("en-US");
        server.AddApp<SimpleApp>();
        server.AddApp<TreeApp>();
        server.AddApp<ListApp>();
        server.AddApp<FileTreeApp>();

        var serverTask = server.RunAsync(cts);

        Console.WriteLine("Waiting for server");
        Thread.Sleep(1000);
        Console.WriteLine("Starting benchmark");

        var testProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "npx",
                Arguments = $"playwright test {file}.spec.ts -g {testName}",
                WorkingDirectory = clientBenchmarkDir,
                RedirectStandardOutput = true,
                UseShellExecute = false
            }
        };
        // Use core 3 and 4 for client process
        if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux())
        {
            testProcess.ProcessorAffinity = 0b1100;
        }

        foreach (var option in testOptions)
        {
            testProcess.StartInfo.Environment.Add(option.Key, option.Value);
        }

        Console.WriteLine("Warmup runs");
        for (int i = 0; i < 2; i++)
        {
            testProcess.Start();
            var warmupOutput = testProcess.StandardOutput.BaseStream.CopyToAsync(Console.OpenStandardOutput());
            testProcess.WaitForExit();
            await warmupOutput;
        }

        serverSideTimings.Clear(); // clear before real run
        Console.WriteLine("Real run");
        testProcess.Start();
        var testOutput = testProcess.StandardOutput.BaseStream.CopyToAsync(Console.OpenStandardOutput());
        testProcess.WaitForExit();
        await testOutput;

        Console.WriteLine("Stopping server");
        cts.Cancel();
        serverTask.Wait();

        Console.WriteLine("Assembling results");


        var clientCsv = File.ReadAllLines($"{clientBenchmarkDir}/results/{file}/{testName}.csv");

        var serverCsv = serverSideTimings
            .Select(timing => $"{timing.TotalTime},{timing.DiffTime}")
            .Prepend("TotalTime,DiffTime");

        File.WriteAllLines(
            $"./results/{file}/{testName}/{diffTypeName}.csv",
            serverCsv.Zip(clientCsv)
                .Select(e =>
                {
                    var (serverRow, clientRow) = e;
                    return $"{clientRow},{serverRow}";
                })
        );
    }
}

await RunBenchmarks("simple", "write_text", new());
// await RunBenchmarks("simple", "click_checkbox", new());
// await RunBenchmarks("simple", "click_radiobuttons", new());

Console.WriteLine("done");
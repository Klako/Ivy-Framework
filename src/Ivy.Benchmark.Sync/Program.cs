using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Running;

namespace Ivy.Benchmark.Sync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _ = BenchmarkRunner.Run(typeof(Program).Assembly,
                DefaultConfig.Instance
                    .AddDiagnoser(new EtwProfiler()));
        }
    }
}

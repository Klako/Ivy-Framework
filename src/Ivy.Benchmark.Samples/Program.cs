using Ivy.Samples.Shared;

#if BENCHMARK
    Console.WriteLine("BENCHMARKS defined");
#endif

await SamplesServer.RunAsync();
//using Ivy.Samples.Shared;

using Ivy.Benchmark.Samples;

#if BENCHMARK
    Console.WriteLine("BENCHMARKS defined");
#endif

await BenchmarkServer.RunAsync();
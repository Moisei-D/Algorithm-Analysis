using Lab3.Graphs;

namespace Lab3.Benchmarking;

public sealed record BenchmarkResult(GraphType GraphType, int Size, string Algorithm, double AverageMilliseconds);

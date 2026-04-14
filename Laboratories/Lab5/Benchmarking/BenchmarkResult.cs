namespace Lab5.Benchmarking;

public record BenchmarkResult(
    string GraphType,
    string Algorithm,
    int VertexCount,
    double AverageTimeMs
);
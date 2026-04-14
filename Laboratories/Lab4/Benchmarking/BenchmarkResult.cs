namespace Lab4.Benchmarking;

public record BenchmarkResult(
    string GraphType,
    string Algorithm,
    int VertexCount,
    double AverageTimeMs
);
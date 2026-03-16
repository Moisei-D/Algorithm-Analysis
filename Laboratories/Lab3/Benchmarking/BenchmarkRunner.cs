using System.Diagnostics;
using Lab3.Algorithms;
using Lab3.Algorithms.BFS;
using Lab3.Algorithms.DFS;
using Lab3.Graphs;

namespace Lab3.Benchmarking;

public sealed class BenchmarkRunner
{
    private const int MaxRecursiveDfsVertices = 100_000;
    private const int MaxRecursiveDfsVerticesLongPath = 5_000;
    private readonly int _warmupRuns;
    private readonly int _iterations;
    private readonly int _seed;

    public BenchmarkRunner(int warmupRuns = 1, int iterations = 5, int seed = 42)
    {
        _warmupRuns = warmupRuns;
        _iterations = iterations;
        _seed = seed;
    }

    public IReadOnlyList<BenchmarkResult> RunAll(int[] sizes)
    {
        var results = new List<BenchmarkResult>();
        var graphTypes = Enum.GetValues<GraphType>();
        var algorithms = new (string Name, ISearchAlgorithm Algorithm)[]
        {
            ("BfsClassic", new BfsClassic()),
            ("BfsOptimized", new BfsOptimized()),
            ("DfsClassic", new DfsClassic()),
            ("DfsOptimized", new DfsOptimized())
        };

        Console.WriteLine($"{"Graph",-12} {"Size",8} {"Algorithm",-12} {"Avg (ms)",12}");
        Console.WriteLine(new string('-', 48));

        foreach (var graphType in graphTypes)
        {
            var maxSize = GetMaxSize(graphType);
            foreach (var size in sizes)
            {
                if (size > maxSize)
                {
                    continue;
                }

                var graph = GraphFactory.Create(graphType, size, _seed);

                foreach (var (name, algorithm) in algorithms)
                {
                    if (name == "DfsClassic" && graph.VertexCount > GetRecursiveDfsLimit(graphType))
                    {
                        continue;
                    }

                    Warmup(graph, algorithm);
                    var averageMs = MeasureAverage(graph, algorithm);

                    results.Add(new BenchmarkResult(graphType, size, name, averageMs));
                    Console.WriteLine($"{graphType,-12} {size,8} {name,-12} {averageMs,12:F3}");
                }
            }
        }

        return results;
    }

    private static int GetMaxSize(GraphType graphType)
    {
        return graphType switch
        {
            GraphType.RandomDense => 5_000,
            GraphType.Complete => 1_000,
            _ => 10_000
        };
    }

    private static int GetRecursiveDfsLimit(GraphType graphType)
    {
        return graphType switch
        {
            GraphType.Grid => MaxRecursiveDfsVerticesLongPath,
            GraphType.Path => MaxRecursiveDfsVerticesLongPath,
            GraphType.Cycle => MaxRecursiveDfsVerticesLongPath,
            _ => MaxRecursiveDfsVertices
        };
    }

    private void Warmup(IGraph graph, ISearchAlgorithm algorithm)
    {
        for (var i = 0; i < _warmupRuns; i++)
        {
            algorithm.Run(graph, 0);
        }
    }

    private double MeasureAverage(IGraph graph, ISearchAlgorithm algorithm)
    {
        var totalTicks = 0L;
        for (var i = 0; i < _iterations; i++)
        {
            var sw = Stopwatch.StartNew();
            algorithm.Run(graph, 0);
            sw.Stop();
            totalTicks += sw.ElapsedTicks;
        }

        return totalTicks * 1000.0 / Stopwatch.Frequency / _iterations;
    }
}

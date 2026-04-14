using Lab4.Algorithms;
using Lab4.Algorithms;
using Lab4.Graphs;
using System.Diagnostics;

namespace Lab4.Benchmarking;

public static class BenchmarkRunner
{
    private const int Runs = 5;

    // Vertex counts to test
    public static readonly int[] Sizes = { 100, 200, 300, 500, 750, 1000, 1500, 2000, 2500, 3000 };

    // Floyd–Warshall is O(V^3) — limit its sizes to avoid multi-minute waits
    public static readonly int[] FWSizes = { 100, 150, 200, 250, 300, 350, 400, 450, 500 };

    public static List<BenchmarkResult> RunAll()
    {
        var results = new List<BenchmarkResult>();
        var rng = new Random(42);

        foreach (GraphGenerator.Density density in Enum.GetValues<GraphGenerator.Density>())
        {
            string densityName = density.ToString();
            Console.WriteLine($"\n=== Graph Density: {densityName} ===");

            // --- Dijkstra benchmarks (larger sizes) ---
            foreach (int n in Sizes)
            {
                double totalClassic = 0, totalOpt = 0;

                for (int run = 0; run < Runs; run++)
                {
                    var graph = GraphGenerator.Generate(n, density, rng);

                    // Classic
                    var sw = Stopwatch.StartNew();
                    DijkstraClassic.Run(graph, 0);
                    sw.Stop();
                    totalClassic += sw.Elapsed.TotalMilliseconds;

                    // Optimized
                    sw.Restart();
                    DijkstraOptimized.Run(graph, 0);
                    sw.Stop();
                    totalOpt += sw.Elapsed.TotalMilliseconds;
                }

                double avgClassic = totalClassic / Runs;
                double avgOpt = totalOpt / Runs;

                results.Add(new BenchmarkResult(densityName, "DijkstraClassic", n, avgClassic));
                results.Add(new BenchmarkResult(densityName, "DijkstraOptimized", n, avgOpt));

                Console.WriteLine($"  {densityName,-12} {n,5}  DijkstraClassic   {avgClassic:F3} ms");
                Console.WriteLine($"  {densityName,-12} {n,5}  DijkstraOptimized {avgOpt:F3} ms");
            }

            // --- Floyd–Warshall benchmarks (smaller sizes due to O(V^3)) ---
            foreach (int n in FWSizes)
            {
                double totalClassic = 0, totalOpt = 0;

                for (int run = 0; run < Runs; run++)
                {
                    var graph = GraphGenerator.Generate(n, density, rng);
                    var matrix = graph.ToAdjacencyMatrix();

                    // Classic
                    var sw = Stopwatch.StartNew();
                    FloydWarshallClassic.Run(matrix);
                    sw.Stop();
                    totalClassic += sw.Elapsed.TotalMilliseconds;

                    // Optimized
                    sw.Restart();
                    FloydWarshallOptimized.RunMatrix(matrix);
                    sw.Stop();
                    totalOpt += sw.Elapsed.TotalMilliseconds;
                }

                double avgClassic = totalClassic / Runs;
                double avgOpt = totalOpt / Runs;

                results.Add(new BenchmarkResult(densityName, "FloydWarshallClassic", n, avgClassic));
                results.Add(new BenchmarkResult(densityName, "FloydWarshallOptimized", n, avgOpt));

                Console.WriteLine($"  {densityName,-12} {n,5}  FloydWarshallClassic   {avgClassic:F3} ms");
                Console.WriteLine($"  {densityName,-12} {n,5}  FloydWarshallOptimized {avgOpt:F3} ms");
            }
        }

        return results;
    }
}
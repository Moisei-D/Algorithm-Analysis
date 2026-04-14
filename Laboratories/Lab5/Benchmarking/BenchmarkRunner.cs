using Lab5.Algorithms;
using Lab5.Benchmarking;
using Lab5.Graphs;
using System.Diagnostics;

namespace Lab5.Benchmarking;

public static class BenchmarkRunner
{
    private const int Runs = 5;

    // Prim is O((V+E) log V) — can handle large sizes
    public static readonly int[] PrimSizes = { 100, 200, 300, 500, 750, 1000, 1500, 2000, 2500, 3000 };

    // Kruskal is O(E log E) — bottleneck is sorting; limit dense graphs
    public static readonly int[] KruskalSizes = { 100, 200, 300, 500, 750, 1000, 1500, 2000, 2500, 3000 };

    public static List<BenchmarkResult> RunAll()
    {
        var results = new List<BenchmarkResult>();
        var rng = new Random(42);

        foreach (GraphGenerator.Density density in Enum.GetValues<GraphGenerator.Density>())
        {
            string densityName = density.ToString();
            Console.WriteLine($"\n=== Graph Density: {densityName} ===");

            // Use the same size set for all 4 algorithms to keep comparisons fair.
            foreach (int n in PrimSizes)
            {
                double totalPrimClassic = 0, totalPrimOpt = 0;
                double totalKruskalClassic = 0, totalKruskalOpt = 0;

                for (int run = 0; run < Runs; run++)
                {
                    // One graph per run, shared by all algorithms.
                    var graph = GraphGenerator.Generate(n, density, rng);

                    var sw = Stopwatch.StartNew();
                    PrimClassic.Run(graph);
                    sw.Stop();
                    totalPrimClassic += sw.Elapsed.TotalMilliseconds;

                    sw.Restart();
                    PrimOptimized.Run(graph);
                    sw.Stop();
                                    totalPrimOpt += sw.Elapsed.TotalMilliseconds;

                    sw.Restart();
                    KruskalClassic.Run(graph);
                    sw.Stop();
                    totalKruskalClassic += sw.Elapsed.TotalMilliseconds;

                    sw.Restart();
                    KruskalOptimized.Run(graph);
                    sw.Stop();
                    totalKruskalOpt += sw.Elapsed.TotalMilliseconds;
                }

                double avgPrimClassic = totalPrimClassic / Runs;
                double avgPrimOpt = totalPrimOpt / Runs;
                double avgKruskalClassic = totalKruskalClassic / Runs;
                double avgKruskalOpt = totalKruskalOpt / Runs;

                results.Add(new BenchmarkResult(densityName, "PrimClassic", n, avgPrimClassic));
                results.Add(new BenchmarkResult(densityName, "PrimOptimized", n, avgPrimOpt));
                results.Add(new BenchmarkResult(densityName, "KruskalClassic", n, avgKruskalClassic));
                results.Add(new BenchmarkResult(densityName, "KruskalOptimized", n, avgKruskalOpt));

                Console.WriteLine($"  {densityName,-12} {n,5}  PrimClassic       {avgPrimClassic:F3} ms");
                Console.WriteLine($"  {densityName,-12} {n,5}  PrimOptimized     {avgPrimOpt:F3} ms");
                Console.WriteLine($"  {densityName,-12} {n,5}  KruskalClassic    {avgKruskalClassic:F3} ms");
                Console.WriteLine($"  {densityName,-12} {n,5}  KruskalOptimized  {avgKruskalOpt:F3} ms");
            }
        }

        return results;
    }
}
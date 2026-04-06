using Lab5.Benchmarking;
using Lab5.Plotting;

Console.WriteLine("Lab 5 — Dijkstra & Floyd-Warshall Empirical Analysis");
Console.WriteLine("======================================================\n");

var results = BenchmarkRunner.RunAll();

Console.WriteLine("\n\nGenerating plots...");
string outputDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Plotting"));
PlotGenerator.GenerateAll(results, outputDir);

Console.WriteLine($"\nDone. {results.Count} benchmark records collected.");
Console.WriteLine($"Plots saved to: {outputDir}");

Console.WriteLine("Press any key to exit...");
Console.ReadLine();
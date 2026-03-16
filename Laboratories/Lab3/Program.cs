using Lab3.Benchmarking;

var sizes = new[]
{
    100, 200, 300, 400, 500, 750,
    1_000, 1_250, 1_500, 1_750, 2_000,
    2_500, 3_000, 3_500, 4_000, 4_500, 5_000
};
var runner = new BenchmarkRunner(warmupRuns: 1, iterations: 5, seed: 42);

Console.WriteLine("Benchmark averages (ms):");
var results = runner.RunAll(sizes);

var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
var outputDirectory = Path.Combine(projectDirectory, "Plots");
BenchmarkPlotter.SavePlots(results, outputDirectory);

Console.WriteLine($"Plots saved to: {outputDirectory}");

Console.ReadLine();

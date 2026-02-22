using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using Lab2.Utilities;
using Lab2.SortingAlgorithms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.WindowsForms;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║        SORTING ALGORITHMS BENCHMARK - LAB 2                   ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝\n");

            // Define test sizes (10 to 30,000)
            int[] sizes = { 10, 50, 100, 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 12000, 15000 };
            const int runs = 3;

            var results = new List<BenchmarkResult>();

            var algorithms = new Dictionary<string, Action<int[]>>
            {
                { "MergeSort", MergeSort.Sort },
                { "QuickSort", QuickSort.Sort },
                { "HeapSort", HeapSort.Sort },
                { "PatienceSort", PatienceSort.Sort }
            };

            Console.WriteLine($"Benchmarking {algorithms.Count} algorithms across {sizes.Length} sizes...");

            foreach (int size in sizes)
            {
                Console.WriteLine($"\n\n{new string('═', 65)}");
                Console.WriteLine($"  TESTING ARRAY SIZE: {size}");
                Console.WriteLine($"{new string('═', 65)}");

                // Generate arrays for this size
                int[] randomInt = ArrayGenerator.GenerateRandomIntArray(size);
                int[] sortedInt = ArrayGenerator.GenerateSortedIntArray(size);
                int[] reversedInt = ArrayGenerator.GenerateReverseSortedIntArray(size);
                int[] duplicatesInt = ArrayGenerator.GenerateRandomDuplicateIntArray(size);


                var arrayTypes = new Dictionary<string, int[]>
                {
                    { "Random", randomInt },
                    { "Sorted", sortedInt },
                    { "Reversed", reversedInt },
                    { "Duplicates", duplicatesInt }
                };

                foreach (var algo in algorithms)
                {
                    Console.WriteLine($"\n  --- {algo.Key.ToUpper()} ---");

                    foreach (var arrayType in arrayTypes)
                    {
                        var result = BenchmarkAlgorithm(algo.Key, arrayType.Key, arrayType.Value, algo.Value, runs);
                        results.Add(result);
                        DisplayResult(result);
                    }
                }
            }

            // Display summary table for the largest size
            int maxSize = sizes.Max();
            Console.WriteLine($"\n\n{new string('═', 65)}");
            Console.WriteLine($"  BENCHMARK SUMMARY (Size: {maxSize})");
            Console.WriteLine($"{new string('═', 65)}\n");

            // Filter results for max size only
            var finalResults = results.Where(r => r.ArraySize == maxSize).ToList();
            DisplaySummaryTable(finalResults);

            Console.WriteLine("\n\nGenerating Performance Graphs...");
            GenerateLineGraphs(results, sizes);
            Console.WriteLine("✓ Graphs saved to 'Results' directory!");

            Console.WriteLine("\nComparison graphs for each algorithm created.");
            Console.WriteLine("Check your project folder 'Results' for 'MergeSort_Performance.png', etc.");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static BenchmarkResult BenchmarkAlgorithm(string algoName, string arrayType, int[] originalArray, Action<int[]> sortMethod, int runs)
        {
            var result = new BenchmarkResult
            {
                AlgorithmName = algoName,
                ArrayType = arrayType,
                ArraySize = originalArray.Length,
                IndividualRuns = new double[runs]
            };

            for (int i = 0; i < runs; i++)
            {
                int[] testArray = new int[originalArray.Length];
                Array.Copy(originalArray, testArray, originalArray.Length);

                Stopwatch sw = Stopwatch.StartNew();
                sortMethod(testArray);
                sw.Stop();

                result.IndividualRuns[i] = sw.Elapsed.TotalMilliseconds;

                // Verify correctness on first run
                if (i == 0)
                {
                    result.IsCorrect = IsSorted(testArray);
                }
            }

            result.CalculateAverage();
            return result;
        }

        static void DisplayResult(BenchmarkResult result)
        {
            Console.WriteLine($"  {result.ArrayType} Array ({result.ArraySize}):");
            Console.WriteLine($"    Average: {result.AverageTimeMs:F3} ms  [ {string.Join(", ", result.IndividualRuns.Select(r => r.ToString("F3")))} ]");
            Console.WriteLine($"    Sorted: {(result.IsCorrect ? "✓ Yes" : "✗ No")}");
        }

        static void DisplaySummaryTable(List<BenchmarkResult> results)
        {
            Console.WriteLine($"{"Algorithm",-15} {"Random",-12} {"Sorted",-12} {"Reversed",-12} {"Duplicates",-12}");
            Console.WriteLine(new string('─', 80));

            var algorithms = results.Select(r => r.AlgorithmName).Distinct();

            foreach (var algo in algorithms)
            {
                var algoResults = results.Where(r => r.AlgorithmName == algo).ToList();
                var random = algoResults.FirstOrDefault(r => r.ArrayType == "Random")?.AverageTimeMs ?? 0;
                var sorted = algoResults.FirstOrDefault(r => r.ArrayType == "Sorted")?.AverageTimeMs ?? 0;
                var reversed = algoResults.FirstOrDefault(r => r.ArrayType == "Reversed")?.AverageTimeMs ?? 0;
                var duplicates = algoResults.FirstOrDefault(r => r.ArrayType == "Duplicates")?.AverageTimeMs ?? 0;

                Console.WriteLine($"{algo,-15} {random,9:F3} ms  {sorted,9:F3} ms  {reversed,9:F3} ms  {duplicates,9:F3} ms");
            }
        }

        static void GenerateLineGraphs(List<BenchmarkResult> results, int[] sizes)
        {
            try
            {
                // Ensure Results directory exists
                string resultsDir = "Results";
                if (!Directory.Exists(resultsDir))
                {
                    Directory.CreateDirectory(resultsDir);
                }

                var algorithms = results.Select(r => r.AlgorithmName).Distinct();

                // Generate ONE graph PER ALGORITHM
                foreach (var algo in algorithms)
                {
                    var plotModel = new PlotModel { Title = $"{algo} Performance Analysis" };

                    // Add Legend
                    plotModel.Legends.Add(new Legend
                    {
                        LegendTitle = "Data Type",
                        LegendPosition = LegendPosition.TopLeft,
                        LegendPlacement = LegendPlacement.Inside,
                        LegendBorderThickness = 1,
                        LegendBorder = OxyColors.Black,
                        LegendBackground = OxyColors.White, // Solid white background for the legend
                        LegendTextColor = OxyColors.Black,  // Ensure text is black
                        LegendTitleColor = OxyColors.Black
                    });


                    // X-Axis: Array Size
                    plotModel.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Bottom,
                        Title = "Array Size (n)",
                        Minimum = 0,
                        Maximum = sizes.Max() * 1.05
                    });

                    // Y-Axis: Time in ms
                    plotModel.Axes.Add(new LinearAxis
                    {
                        Position = AxisPosition.Left,
                        Title = "Time (ms)",
                        Minimum = 0
                    });

                    // Add 4 lines: Random, Sorted, Reversed, Duplicates
                    var dataTypes = new[] { "Random", "Sorted", "Reversed", "Duplicates" };
                    var colors = new[] { OxyColors.Blue, OxyColors.Green, OxyColors.Red, OxyColors.Orange };
                    var markers = new[] { MarkerType.Circle, MarkerType.Triangle, MarkerType.Square, MarkerType.Diamond };

                    for (int i = 0; i < dataTypes.Length; i++)
                    {
                        var type = dataTypes[i];
                        var lineSeries = new LineSeries
                        {
                            Title = type,
                            Color = colors[i],
                            MarkerType = markers[i],
                            MarkerSize = 4,
                            StrokeThickness = 2
                        };

                        // Filter results for this algorithm + array type, ordered by size
                        var points = results
                            .Where(r => r.AlgorithmName == algo && r.ArrayType == type)
                            .OrderBy(r => r.ArraySize)
                            .ToList();

                        foreach (var p in points)
                        {
                            lineSeries.Points.Add(new DataPoint(p.ArraySize, p.AverageTimeMs));
                        }

                        plotModel.Series.Add(lineSeries);
                    }

                    // Export
                    var pngExporter = new PngExporter { Width = 1000, Height = 700 };
                    string filePath = Path.Combine(resultsDir, $"{algo}_Performance.png");
                    pngExporter.ExportToFile(plotModel, filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating graphs: {ex.Message}");
            }
        }

        static bool IsSorted(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] > array[i + 1])
                    return false;
            }
            return true;
        }
    }

    class BenchmarkResult
    {
        public string AlgorithmName { get; set; } = string.Empty;
        public string ArrayType { get; set; } = string.Empty;
        public double AverageTimeMs { get; set; }
        public double[] IndividualRuns { get; set; } = Array.Empty<double>();
        public int ArraySize { get; set; }
        public bool IsCorrect { get; set; }

        public void CalculateAverage()
        {
            AverageTimeMs = IndividualRuns.Average();
        }
    }
}
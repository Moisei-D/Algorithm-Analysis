using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using Lab4.Benchmarking;

namespace Lab4.Plotting;

public static class PlotGenerator
{
    private static readonly OxyColor[] Colors =
    {
        OxyColors.SteelBlue,
        OxyColors.OrangeRed,
        OxyColors.ForestGreen,
        OxyColors.Purple,
    };

    /// <summary>
    /// Generates one PNG per (graphType × algorithmPair) combination.
    /// E.g. "VerySparse_Dijkstra.png", "Dense_FloydWarshall.png", etc.
    /// 16 plots total: 4 densities × 2 algorithm families × 2 variants = 16.
    /// </summary>
    public static void GenerateAll(List<BenchmarkResult> results, string outputDir)
    {
        Directory.CreateDirectory(outputDir);

        var densities = results.Select(r => r.GraphType).Distinct().ToList();
        var algoGroups = new[]
        {
            new[] { "DijkstraClassic",        "DijkstraOptimized"       },
            new[] { "FloydWarshallClassic",    "FloydWarshallOptimized"  },
        };
        string[] groupNames = { "Dijkstra", "FloydWarshall" };

        foreach (var density in densities)
        {
            for (int g = 0; g < algoGroups.Length; g++)
            {
                string groupName = groupNames[g];
                string[] algos = algoGroups[g];

                var model = BuildPlotModel(
                    title: $"{density} — {groupName}",
                    xLabel: "Vertices",
                    yLabel: "Average Time (ms)",
                    results: results,
                    density: density,
                    algos: algos
                );

                string filePath = Path.Combine(outputDir, $"{density}_{groupName}.png");
                SavePlotAsPng(model, filePath, width: 900, height: 500);
                Console.WriteLine($"  Saved plot: {filePath}");
            }
        }
    }

    private static PlotModel BuildPlotModel(
        string title, string xLabel, string yLabel,
        List<BenchmarkResult> results,
        string density, string[] algos)
    {
        var model = new PlotModel { Title = title, Background = OxyColors.White };

        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Bottom,
            Title = xLabel,
            MajorGridlineStyle = LineStyle.Dot,
            MinorGridlineStyle = LineStyle.None,
        });
        model.Axes.Add(new LinearAxis
        {
            Position = AxisPosition.Left,
            Title = yLabel,
            MajorGridlineStyle = LineStyle.Dot,
            MinorGridlineStyle = LineStyle.None,
        });

        var legend = new Legend
        {
            LegendPlacement = LegendPlacement.Inside,
            LegendPosition = LegendPosition.TopLeft,
            LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
        };
        model.Legends.Add(legend);

        int colorIdx = 0;
        foreach (var algo in algos)
        {
            var points = results
                .Where(r => r.GraphType == density && r.Algorithm == algo)
                .OrderBy(r => r.VertexCount)
                .Select(r => new DataPoint(r.VertexCount, r.AverageTimeMs))
                .ToList();

            if (points.Count == 0) continue;

            var series = new LineSeries
            {
                Title = algo,
                Color = Colors[colorIdx % Colors.Length],
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerStrokeThickness = 1.5,
                StrokeThickness = 2,
            };
            series.Points.AddRange(points);
            model.Series.Add(series);
            colorIdx++;
        }

        return model;
    }

    private static void SavePlotAsPng(PlotModel model, string filePath, int width, int height)
    {
        using var stream = File.Create(filePath);
        var exporter = new OxyPlot.SkiaSharp.PngExporter { Width = width, Height = height };
        exporter.Export(model, stream);
    }
}
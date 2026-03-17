using Lab3.Graphs;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace Lab3.Benchmarking;

public static class BenchmarkPlotter
{
    public static void SavePlots(IEnumerable<BenchmarkResult> results, string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);

        var benchmarkResults = results.ToList();

        foreach (var graphGroup in benchmarkResults.GroupBy(r => r.GraphType))
        {
            var model = new PlotModel
            {
                Title = $"{graphGroup.Key} graph",
                Background = OxyColors.White
            };

            model.Legends.Add(new Legend
            {
                LegendTitle = "Algorithm",
                LegendPosition = LegendPosition.TopLeft,
                LegendPlacement = LegendPlacement.Inside,
                LegendBorder = OxyColors.Black,
                LegendBorderThickness = 1,
                LegendBackground = OxyColors.White,
                LegendTextColor = OxyColors.Black,
                LegendTitleColor = OxyColors.Black
            });

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Nodes" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Average time (ms)" });

            foreach (var algorithmGroup in graphGroup.GroupBy(r => r.Algorithm))
            {
                var series = new LineSeries { Title = algorithmGroup.Key, MarkerType = MarkerType.Circle };
                foreach (var result in algorithmGroup.OrderBy(r => r.Size))
                {
                    series.Points.Add(new DataPoint(result.Size, result.AverageMilliseconds));
                }

                model.Series.Add(series);
            }

            var filePath = Path.Combine(outputDirectory, $"{graphGroup.Key}.png");
            PngExporter.Export(model, filePath, 1200, 800, 96);
        }

        SaveOverallPlot(benchmarkResults, outputDirectory);
    }

    private static void SaveOverallPlot(IEnumerable<BenchmarkResult> results, string outputDirectory)
    {
        var model = new PlotModel
        {
            Title = "Overall algorithm comparison",
            Background = OxyColors.White
        };

        model.Legends.Add(new Legend
        {
            LegendTitle = "Algorithm",
            LegendPosition = LegendPosition.TopLeft,
            LegendPlacement = LegendPlacement.Inside,
            LegendBorder = OxyColors.Black,
            LegendBorderThickness = 1,
            LegendBackground = OxyColors.White,
            LegendTextColor = OxyColors.Black,
            LegendTitleColor = OxyColors.Black
        });

        model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Nodes" });
        model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Average time (ms)" });

        foreach (var algorithmGroup in results.GroupBy(r => r.Algorithm))
        {
            var series = new LineSeries { Title = algorithmGroup.Key, MarkerType = MarkerType.Circle };

            var averagedBySize = algorithmGroup
                .GroupBy(r => r.Size)
                .OrderBy(g => g.Key)
                .Select(g => new { Size = g.Key, Avg = g.Average(x => x.AverageMilliseconds) });

            foreach (var point in averagedBySize)
            {
                series.Points.Add(new DataPoint(point.Size, point.Avg));
            }

            model.Series.Add(series);
        }

        var filePath = Path.Combine(outputDirectory, "Overall.png");
        PngExporter.Export(model, filePath, 1200, 800, 96);
    }
}

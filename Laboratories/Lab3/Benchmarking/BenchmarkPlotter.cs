using Lab3.Graphs;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace Lab3.Benchmarking;

public static class BenchmarkPlotter
{
    public static void SavePlots(IEnumerable<BenchmarkResult> results, string outputDirectory)
    {
        Directory.CreateDirectory(outputDirectory);

        foreach (var graphGroup in results.GroupBy(r => r.GraphType))
        {
            var model = new PlotModel
            {
                Title = $"{graphGroup.Key} graph",
                Background = OxyColors.White
            };
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
    }
}

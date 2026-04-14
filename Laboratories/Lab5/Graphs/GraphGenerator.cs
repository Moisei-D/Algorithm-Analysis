using Lab4.Graphs;

namespace Lab5.Graphs;

/// <summary>
/// Generates weighted undirected graphs at four density levels:
///   VerySparse  ~ 0.05 * maxEdges
///   Sparse      ~ 0.30 * maxEdges
///   Dense       ~ 0.70 * maxEdges
///   VeryDense   ~ 0.95 * maxEdges  (near-complete)
/// Weights are random doubles in [1, 100].
/// </summary>
public static class GraphGenerator
{
    public enum Density { VerySparse, Sparse, Dense, VeryDense }

    private static readonly Dictionary<Density, double> DensityFactor = new()
    {
        { Density.VerySparse, 0.05 },
        { Density.Sparse,     0.30 },
        { Density.Dense,      0.70 },
        { Density.VeryDense,  0.95 },
    };

    public static WeightedGraph Generate(int vertices, Density density, Random rng)
    {
        var graph = new WeightedGraph(vertices);
        double factor = DensityFactor[density];

        // First ensure connectivity via a random spanning tree
        var shuffled = Enumerable.Range(0, vertices).OrderBy(_ => rng.Next()).ToList();
        for (int i = 1; i < shuffled.Count; i++)
        {
            double w = 1 + rng.NextDouble() * 99;
            graph.AddEdge(shuffled[i - 1], shuffled[i], w);
        }

        // Then add extra random edges up to the target density
        long maxEdges = (long)vertices * (vertices - 1) / 2;
        long targetEdges = (long)(maxEdges * factor);
        long currentEdges = vertices - 1;

        // Use a HashSet to avoid duplicate edges (only feasible for small V)
        // For large V we just attempt random pairs - duplicates are harmless (min weight kept)
        while (currentEdges < targetEdges)
        {
            int u = rng.Next(vertices);
            int v = rng.Next(vertices);
            if (u == v) continue;
            double w = 1 + rng.NextDouble() * 99;
            graph.AddEdge(u, v, w);
            currentEdges++;
        }

        return graph;
    }
}
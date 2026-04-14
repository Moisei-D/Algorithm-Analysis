
using Lab5.Graphs;

namespace Lab5.Algorithms;

/// <summary>
/// Classic Kruskal's MST algorithm. Sorts all edges by weight then uses
/// a simple recursive Union-Find (with path compression only, no rank).
/// O(E log E) time dominated by the sort.
/// </summary>
public static class KruskalClassic
{
    public static (double totalWeight, List<(int u, int v, double w)> edges) Run(WeightedGraph graph)
    {
        var edges = graph.GetAllEdges();

        // Sort edges by weight — classic: simple comparison sort via LINQ
        edges.Sort((a, b) => a.w.CompareTo(b.w));

        int n = graph.VertexCount;
        int[] parent = Enumerable.Range(0, n).ToArray();

        double totalWeight = 0;
        var mstEdges = new List<(int, int, double)>();

        foreach (var (u, v, w) in edges)
        {
            int pu = Find(parent, u);
            int pv = Find(parent, v);
            if (pu == pv) continue; // same component — would form a cycle

            // Union: attach one root to the other (no rank)
            parent[pu] = pv;
            totalWeight += w;
            mstEdges.Add((u, v, w));

            if (mstEdges.Count == n - 1) break; // MST complete
        }

        return (totalWeight, mstEdges);
    }

    /// <summary>Path-compression only (no union by rank).</summary>
    private static int Find(int[] parent, int x)
    {
        if (parent[x] != x)
            parent[x] = Find(parent, parent[x]); // recursive path compression
        return parent[x];
    }
}
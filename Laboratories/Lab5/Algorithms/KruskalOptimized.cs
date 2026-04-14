using Lab5.Graphs;

namespace Lab5.Algorithms;

/// <summary>
/// Optimized Kruskal's MST using Union-Find with both path compression
/// AND union by rank, reducing Find to near-O(1) amortized (inverse Ackermann).
/// Also avoids LINQ allocation in Find and uses iterative path compression.
/// </summary>
public static class KruskalOptimized
{
    public static (double totalWeight, List<(int u, int v, double w)> edges) Run(WeightedGraph graph)
    {
        var edges = graph.GetAllEdges();

        // Sort edges — same cost as classic, but rest of algorithm is faster
        edges.Sort((a, b) => a.w.CompareTo(b.w));

        int n = graph.VertexCount;
        int[] parent = Enumerable.Range(0, n).ToArray();
        int[] rank = new int[n]; // union by rank

        double totalWeight = 0;
        var mstEdges = new List<(int, int, double)>(n - 1);

        foreach (var (u, v, w) in edges)
        {
            int pu = Find(parent, u);
            int pv = Find(parent, v);
            if (pu == pv) continue;

            // Union by rank: attach smaller tree under larger
            if (rank[pu] < rank[pv])
                parent[pu] = pv;
            else if (rank[pu] > rank[pv])
                parent[pv] = pu;
            else
            {
                parent[pv] = pu;
                rank[pu]++;
            }

            totalWeight += w;
            mstEdges.Add((u, v, w));

            if (mstEdges.Count == n - 1) break;
        }

        return (totalWeight, mstEdges);
    }

    /// <summary>Iterative path compression (avoids recursive call stack).</summary>
    private static int Find(int[] parent, int x)
    {
        int root = x;
        while (parent[root] != root)
            root = parent[root];

        // Path compression: point every node on the path directly to root
        while (parent[x] != root)
        {
            int next = parent[x];
            parent[x] = root;
            x = next;
        }
        return root;
    }
}
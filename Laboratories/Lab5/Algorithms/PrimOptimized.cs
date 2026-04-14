using Lab5.Graphs;

namespace Lab5.Algorithms;

/// <summary>
/// Optimized Prim's MST using a binary min-heap (PriorityQueue)
/// Reduces per-step extraction from O(V) to O(log V). Overall O((V+E) log V).
/// Stale heap entries are discarded lazily via a weight comparison on extraction.
/// </summary>
public static class PrimOptimized
{
    public static (double totalWeight, List<(int u, int v, double w)> edges) Run(IGraph graph, int source = 0)
    {
        int n = graph.VertexCount;
        double[] minEdge = new double[n];
        int[] parent = new int[n];
        bool[] inMST = new bool[n];

        Array.Fill(minEdge, double.PositiveInfinity);
        Array.Fill(parent, -1);
        minEdge[source] = 0.0;

        // PriorityQueue<vertex, weight>
        var pq = new PriorityQueue<int, double>(n);
        pq.Enqueue(source, 0.0);

        double totalWeight = 0;
        var mstEdges = new List<(int, int, double)>();

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int u, out double w);

            // Stale entry: already added to MST with a cheaper edge
            if (inMST[u]) continue;

            inMST[u] = true;
            totalWeight += w;
            if (parent[u] != -1)
                mstEdges.Add((parent[u], u, w));

            foreach (var (v, edgeW) in graph.GetNeighbors(u))
            {
                if (!inMST[v] && edgeW < minEdge[v])
                {
                    minEdge[v] = edgeW;
                    parent[v] = u;
                    pq.Enqueue(v, edgeW);
                }
            }
        }

        return (totalWeight, mstEdges);
    }
}
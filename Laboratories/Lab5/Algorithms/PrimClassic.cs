using Lab5.Graphs;

namespace Lab5.Algorithms;

public static class PrimClassic
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

        double totalWeight = 0;
        var mstEdges = new List<(int, int, double)>();

        for (int iter = 0; iter < n; iter++)
        {
            // O(V) linear scan: find non-MST vertex with minimum edge weight
            int u = -1;
            for (int i = 0; i < n; i++)
            {
                if (!inMST[i] && (u == -1 || minEdge[i] < minEdge[u]))
                    u = i;
            }
            if (u == -1 || double.IsPositiveInfinity(minEdge[u])) break;

            inMST[u] = true;
            totalWeight += minEdge[u];
            if (parent[u] != -1)
                mstEdges.Add((parent[u], u, minEdge[u]));

            // Relax edges from u
            foreach (var (v, w) in graph.GetNeighbors(u))
            {
                if (!inMST[v] && w < minEdge[v])
                {
                    minEdge[v] = w;
                    parent[v] = u;
                }
            }
        }
        return (totalWeight, mstEdges);
    }

}
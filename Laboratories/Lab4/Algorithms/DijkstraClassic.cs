using Lab4.Graphs;

namespace Lab4.Algorithms;

/// <summary>
/// Classic Dijkstra using a sorted List as a priority queue (O((V+E) log V) via linear scan).
/// Simple and readable but not optimal — uses O(V) scan per extraction.
/// </summary>
/// 

public static class DijkstraClassic
{
    public static double[] Run(IGraph graph, int source)
    {
        int n = graph.VertexCount;
        double[] dist = new double[n];
        Array.Fill(dist, double.PositiveInfinity);
        dist[source] = 0;

        bool[] visited = new bool[n];

        // Simple list-based "priority queue": always scan for minimum
        for (int iter = 0; iter < n; iter++)
        {
            // Find unvisited vertex with minimum distance (O(V) scan)
            int u = -1;
            for (int i = 0; i < n; i++)
            {
                if (!visited[i] && (u == -1 || dist[i] < dist[u]))
                    u = i;
            }
            if (u == -1 || double.IsPositiveInfinity(dist[u])) break;
            visited[u] = true;

            foreach (var (v, w) in graph.GetNeighbors(u))
            {
                double newDist = dist[u] + w;
                if (newDist < dist[v])
                    dist[v] = newDist;
            }
        }
        return dist;
    }
}
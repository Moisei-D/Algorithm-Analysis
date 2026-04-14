using Lab4.Graphs;

namespace Lab4.Algorithms;

/// <summary>
/// Optimized Dijkstra using a binary min-heap (PriorityQueue from .NET 6+).
/// Time complexity: O((V + E) log V). Avoids the O(V) linear scan per step.
/// </summary>
public static class DijkstraOptimized
{
    public static double[] Run(IGraph graph, int source)
    {
        int n = graph.VertexCount;
        double[] dist = new double[n];
        Array.Fill(dist, double.PositiveInfinity);
        dist[source] = 0;

        // PriorityQueue<(vertex), (priority=distance)>
        var pq = new PriorityQueue<int, double>(n);
        pq.Enqueue(source, 0.0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int u, out double d);

            // Stale entry: a shorter path was already found
            if (d > dist[u]) continue;

            foreach (var (v, w) in graph.GetNeighbors(u))
            {
                double newDist = dist[u] + w;
                if (newDist < dist[v])
                {
                    dist[v] = newDist;
                    pq.Enqueue(v, newDist);
                }
            }
        }

        return dist;
    }
}
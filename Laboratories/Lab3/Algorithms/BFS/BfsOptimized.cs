using System.Collections;
using Lab3.Graphs;

namespace Lab3.Algorithms.BFS;

public sealed class BfsOptimized : ISearchAlgorithm
{
    public IReadOnlyList<int> Run(IGraph graph, int start)
    {
        ArgumentNullException.ThrowIfNull(graph);
        if (start < 0 || start >= graph.VertexCount)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        var visited = new BitArray(graph.VertexCount);
        var order = new List<int>(graph.VertexCount);
        var queue = new int[graph.VertexCount];
        var head = 0;
        var tail = 0;
        var count = 0;

        visited.Set(start, true);
        queue[tail++] = start;
        count++;

        while (count > 0)
        {
            var vertex = queue[head++];
            if (head == queue.Length)
            {
                head = 0;
            }
            count--;

            order.Add(vertex);

            foreach (var neighbor in graph.GetNeighbors(vertex))
            {
                if (visited[neighbor])
                {
                    continue;
                }

                visited.Set(neighbor, true);
                queue[tail++] = neighbor;
                if (tail == queue.Length)
                {
                    tail = 0;
                }
                count++;
            }
        }

        return order;
    }
}

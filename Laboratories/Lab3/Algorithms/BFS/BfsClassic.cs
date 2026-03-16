using Lab3.Graphs;

namespace Lab3.Algorithms.BFS;

public sealed class BfsClassic : ISearchAlgorithm
{
    public IReadOnlyList<int> Run(IGraph graph, int start)
    {
        ArgumentNullException.ThrowIfNull(graph);
        if (start < 0 || start >= graph.VertexCount)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        var visited = new bool[graph.VertexCount];
        var order = new List<int>(graph.VertexCount);
        var queue = new Queue<int>();

        visited[start] = true;
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            order.Add(vertex);

            foreach (var neighbor in graph.GetNeighbors(vertex))
            {
                if (visited[neighbor])
                {
                    continue;
                }

                visited[neighbor] = true;
                queue.Enqueue(neighbor);
            }
        }

        return order;
    }
}

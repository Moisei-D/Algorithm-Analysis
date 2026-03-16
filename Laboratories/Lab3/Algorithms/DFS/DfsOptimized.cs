using Lab3.Graphs;

namespace Lab3.Algorithms.DFS;

public sealed class DfsOptimized : ISearchAlgorithm
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
        var stack = new int[graph.VertexCount];
        var top = 0;

        stack[top++] = start;

        while (top > 0)
        {
            var vertex = stack[--top];
            if (visited[vertex])
            {
                continue;
            }

            visited[vertex] = true;
            order.Add(vertex);

            var neighbors = graph.GetNeighbors(vertex);
            for (var i = neighbors.Count - 1; i >= 0; i--)
            {
                var neighbor = neighbors[i];
                if (!visited[neighbor])
                {
                    stack[top++] = neighbor;
                }
            }
        }

        return order;
    }
}

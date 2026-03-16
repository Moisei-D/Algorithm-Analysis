using Lab3.Graphs;

namespace Lab3.Algorithms.DFS;

public sealed class DfsClassic : ISearchAlgorithm
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

        Visit(start, graph, visited, order);
        return order;
    }

    private static void Visit(int vertex, IGraph graph, bool[] visited, ICollection<int> order)
    {
        visited[vertex] = true;
        order.Add(vertex);

        foreach (var neighbor in graph.GetNeighbors(vertex))
        {
            if (!visited[neighbor])
            {
                Visit(neighbor, graph, visited, order);
            }
        }
    }
}

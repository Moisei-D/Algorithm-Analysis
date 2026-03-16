namespace Lab3.Graphs;

public interface IGraph
{
    int VertexCount { get; }
    bool IsDirected { get; }
    IReadOnlyList<int> GetNeighbors(int vertex);
    bool HasEdge(int from, int to);
}

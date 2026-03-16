namespace Lab3.Graphs;

public abstract class GraphBase : IGraph
{
    private readonly List<int>[] _adjacency;

    protected GraphBase(int vertexCount, bool isDirected)
    {
        if (vertexCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(vertexCount));
        }

        VertexCount = vertexCount;
        IsDirected = isDirected;
        _adjacency = new List<int>[vertexCount];
        for (var i = 0; i < vertexCount; i++)
        {
            _adjacency[i] = new List<int>();
        }
    }

    public int VertexCount { get; }

    public bool IsDirected { get; }

    public IReadOnlyList<int> GetNeighbors(int vertex)
    {
        ValidateVertex(vertex);
        return _adjacency[vertex];
    }

    public bool HasEdge(int from, int to)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        return _adjacency[from].Contains(to);
    }

    protected void AddEdge(int from, int to)
    {
        ValidateVertex(from);
        ValidateVertex(to);

        if (!_adjacency[from].Contains(to))
        {
            _adjacency[from].Add(to);
        }

        if (!IsDirected && from != to && !_adjacency[to].Contains(from))
        {
            _adjacency[to].Add(from);
        }
    }

    protected void ValidateVertex(int vertex)
    {
        if (vertex < 0 || vertex >= VertexCount)
        {
            throw new ArgumentOutOfRangeException(nameof(vertex));
        }
    }
}

namespace Lab5.Graphs;

public class WeightedGraph : IGraph
{
    private readonly List<(int neighbor, double weight)>[] _adj;

    public int VertexCount { get; }

    public WeightedGraph(int vertexCount)
    {
        VertexCount = vertexCount;
        _adj = new List<(int, double)>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            _adj[i] = new List<(int, double)>();
        }
    }

    public void AddEdge(int u, int v, double weight)
    {
        _adj[u].Add((v, weight));
        _adj[v].Add((u, weight)); // undirected
    }

    public IReadOnlyList<(int neighbor, double weight)> GetNeighbors(int vertex)
        => _adj[vertex];

    public List<(int u, int v, double w)> GetAllEdges()
    {
        var edges = new List<(int, int, double)>();
        for (int u = 0; u < VertexCount; u++)
        {
            foreach (var (v,w) in _adj[u])
            {
                if (v > u)  //avoid duplicates
                    edges.Add((u, v, w));
            }
        }
        return edges;
    }


    public double[,] ToAdjacencyMatrix()
    {
        double inf = double.PositiveInfinity;
        var matrix = new double[VertexCount, VertexCount];
        for (int i = 0; i < VertexCount; i++)
            for (int j = 0; j < VertexCount; j++)
                matrix[i, j] = (i == j) ? 0.0 : inf;

        for (int u = 0; u < VertexCount; u++)
            foreach (var (v, w) in _adj[u])
                matrix[u, v] = Math.Min(matrix[u, v], w);

        return matrix;
    }

}
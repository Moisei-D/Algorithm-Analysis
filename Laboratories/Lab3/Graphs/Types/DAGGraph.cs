namespace Lab3.Graphs.Types;

public sealed class DAGGraph : GraphBase
{
    public DAGGraph(int vertexCount, int? seed = null)
        : base(vertexCount, isDirected: true)
    {
        if (vertexCount <= 1)
        {
            return;
        }

        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        const double probability = 0.3;

        for (var i = 0; i < vertexCount; i++)
        {
            for (var j = i + 1; j < vertexCount; j++)
            {
                if (random.NextDouble() <= probability)
                {
                    AddEdge(i, j);
                }
            }
        }
    }
}

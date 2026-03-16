namespace Lab3.Graphs.Types;

public sealed class RandomSparseGraph : GraphBase
{
    public RandomSparseGraph(int vertexCount, int? seed = null)
        : base(vertexCount, isDirected: false)
    {
        if (vertexCount <= 1)
        {
            return;
        }

        var random = seed.HasValue ? new Random(seed.Value) : new Random();
        var targetEdges = Math.Max(vertexCount - 1, vertexCount * 2);
        var maxEdges = vertexCount * (vertexCount - 1) / 2;
        targetEdges = Math.Min(targetEdges, maxEdges);

        while (CountEdges() < targetEdges)
        {
            var from = random.Next(vertexCount);
            var to = random.Next(vertexCount);
            if (from == to)
            {
                continue;
            }

            AddEdge(from, to);
        }
    }

    private int CountEdges()
    {
        var total = 0;
        for (var i = 0; i < VertexCount; i++)
        {
            total += GetNeighbors(i).Count;
        }

        return total / 2;
    }
}

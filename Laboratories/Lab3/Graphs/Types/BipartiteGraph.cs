namespace Lab3.Graphs.Types;

public sealed class BipartiteGraph : GraphBase
{
    public BipartiteGraph(int vertexCount)
        : base(vertexCount, isDirected: false)
    {
        if (vertexCount <= 1)
        {
            return;
        }

        var leftCount = vertexCount / 2;
        var rightStart = leftCount;
        for (var left = 0; left < leftCount; left++)
        {
            for (var right = rightStart; right < vertexCount; right++)
            {
                AddEdge(left, right);
            }
        }
    }
}

namespace Lab3.Graphs.Types;

public sealed class CycleGraph : GraphBase
{
    public CycleGraph(int vertexCount)
        : base(vertexCount, isDirected: false)
    {
        if (vertexCount <= 1)
        {
            return;
        }

        for (var i = 0; i < vertexCount; i++)
        {
            var next = (i + 1) % vertexCount;
            AddEdge(i, next);
        }
    }
}

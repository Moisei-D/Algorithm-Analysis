namespace Lab3.Graphs.Types;

public sealed class StarGraph : GraphBase
{
    public StarGraph(int vertexCount)
        : base(vertexCount, isDirected: false)
    {
        if (vertexCount <= 1)
        {
            return;
        }

        for (var i = 1; i < vertexCount; i++)
        {
            AddEdge(0, i);
        }
    }
}

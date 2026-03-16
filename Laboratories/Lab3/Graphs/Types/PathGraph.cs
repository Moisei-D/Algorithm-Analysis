namespace Lab3.Graphs.Types;

public sealed class PathGraph : GraphBase
{
    public PathGraph(int vertexCount)
        : base(vertexCount, isDirected: false)
    {
        if (vertexCount <= 1)
        {
            return;
        }

        for (var i = 0; i < vertexCount - 1; i++)
        {
            AddEdge(i, i + 1);
        }
    }
}

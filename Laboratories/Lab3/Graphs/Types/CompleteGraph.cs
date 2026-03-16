namespace Lab3.Graphs.Types;

public sealed class CompleteGraph : GraphBase
{
    public CompleteGraph(int vertexCount)
        : base(vertexCount, isDirected: false)
    {
        for (var i = 0; i < vertexCount; i++)
        {
            for (var j = i + 1; j < vertexCount; j++)
            {
                AddEdge(i, j);
            }
        }
    }
}

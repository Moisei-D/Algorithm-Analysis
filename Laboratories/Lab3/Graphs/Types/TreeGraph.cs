namespace Lab3.Graphs.Types;

public sealed class TreeGraph : GraphBase
{
    public TreeGraph(int vertexCount)
        : base(vertexCount, isDirected: false)
    {
        for (var i = 0; i < vertexCount; i++)
        {
            var left = 2 * i + 1;
            var right = 2 * i + 2;
            if (left < vertexCount)
            {
                AddEdge(i, left);
            }

            if (right < vertexCount)
            {
                AddEdge(i, right);
            }
        }
    }
}

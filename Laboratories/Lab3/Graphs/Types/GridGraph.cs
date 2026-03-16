namespace Lab3.Graphs.Types;

public sealed class GridGraph : GraphBase
{
    public GridGraph(int size)
        : base(size * size, isDirected: false)
    {
        if (size <= 0)
        {
            return;
        }

        for (var row = 0; row < size; row++)
        {
            for (var col = 0; col < size; col++)
            {
                var index = ToIndex(row, col, size);
                if (row + 1 < size)
                {
                    AddEdge(index, ToIndex(row + 1, col, size));
                }

                if (col + 1 < size)
                {
                    AddEdge(index, ToIndex(row, col + 1, size));
                }
            }
        }
    }

    private static int ToIndex(int row, int col, int size) => row * size + col;
}

namespace Lab5.Graphs;

public interface IGraph
{
    int VertexCount { get; }

    //Returns (neighbor, weight) pairs

    IReadOnlyList<(int neighbor, double weight)> GetNeighbors(int vertex);

    double[,] ToAdjacencyMatrix();
}
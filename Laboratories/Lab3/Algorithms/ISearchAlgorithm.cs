using Lab3.Graphs;

namespace Lab3.Algorithms;

public interface ISearchAlgorithm
{
    IReadOnlyList<int> Run(IGraph graph, int start);
}

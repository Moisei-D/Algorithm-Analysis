using Lab3.Graphs.Types;

namespace Lab3.Graphs;

public static class GraphFactory
{
    public static IGraph Create(GraphType type, int size, int? seed = null)
    {
        return type switch
        {
            GraphType.RandomSparse => new RandomSparseGraph(size, seed),
            GraphType.RandomDense => new RandomDenseGraph(size, seed),
            GraphType.Tree => new TreeGraph(size),
            GraphType.Grid => new GridGraph(size),
            GraphType.Cycle => new CycleGraph(size),
            GraphType.Complete => new CompleteGraph(size),
            GraphType.Bipartite => new BipartiteGraph(size),
            GraphType.DAG => new DAGGraph(size, seed),
            GraphType.Star => new StarGraph(size),
            GraphType.Path => new PathGraph(size),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown graph type")
        };
    }
}

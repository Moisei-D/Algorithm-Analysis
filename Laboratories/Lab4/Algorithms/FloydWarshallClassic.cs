namespace Lab4.Algorithms;

/// <summary>
/// Classic Floyd–Warshall: O(V^3) time, O(V^2) space.
/// Operates directly on the adjacency matrix — straightforward triple loop.
/// </summary>
public static class FloydWarshallClassic
{
    public static double[,] Run(double[,] dist)
    {
        int n = dist.GetLength(0);
        // Work on a copy so the original is not mutated
        double[,] d = (double[,])dist.Clone();

        for (int k = 0; k < n; k++)
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    double via = d[i, k] + d[k, j];
                    if (via < d[i, j])
                        d[i, j] = via;
                }

        return d;
    }
}
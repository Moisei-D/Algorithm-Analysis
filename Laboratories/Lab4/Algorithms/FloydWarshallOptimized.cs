namespace Lab5.Algorithms;

/// <summary>
/// Optimized Floyd–Warshall using:
///   1. Row-cache: cache dist[i,k] before the inner loop to avoid repeated 2D indexing.
///   2. Early exit: skip entire i-row when dist[i,k] is infinity (no path through k from i).
///   3. Flat 1D array instead of 2D array for better cache-line utilization.
/// Still O(V^3) asymptotically, but with lower constant factors on large matrices.
/// </summary>
public static class FloydWarshallOptimized
{
    public static double[] Run(double[,] distMatrix)
    {
        int n = distMatrix.GetLength(0);

        // Flatten to 1D: index [i*n + j]
        double[] d = new double[n * n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                d[i * n + j] = distMatrix[i, j];

        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                double dik = d[i * n + k];
                if (double.IsPositiveInfinity(dik)) continue; // early exit

                int rowI = i * n;
                int rowK = k * n;

                for (int j = 0; j < n; j++)
                {
                    double via = dik + d[rowK + j];
                    if (via < d[rowI + j])
                        d[rowI + j] = via;
                }
            }
        }

        return d; 
    }

    /// <summary>Convenience method that reshapes the flat result back to 2D.</summary>
    public static double[,] RunMatrix(double[,] distMatrix)
    {
        int n = distMatrix.GetLength(0);
        double[] flat = Run(distMatrix);
        double[,] result = new double[n, n];
        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                result[i, j] = flat[i * n + j];
        return result;
    }
}
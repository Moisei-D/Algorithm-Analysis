namespace Lab2.Utilities
{
    public static class ArrayGenerator
    {
        private static Random _random = new Random();

        //======== INTEGERS ARRAYS ============

        /// <summary>
        /// Generates a random integer array
        /// </summary>     
        public static int[] GenerateRandomIntArray(int size)
        {
            int[] array = new int[size];
            for (int i = 0; i< size; i++)
            {
                array[i] = _random.Next();
            }
            return array;
        }
        /// <summary>
        /// Generates a sorted integer array
        /// </summary>
        public static int[] GenerateSortedIntArray(int size)
        {
            int[] array = GenerateRandomIntArray(size);
            Array.Sort(array);
            return array;
        }

        /// <summary>
        /// Generates a reverse sorted integer array
        /// </summary>
        public static int[] GenerateReverseSortedIntArray(int size)
        {
            int[] array = GenerateSortedIntArray(size);
            Array.Reverse(array);
            return array;
        }

        /// <summary>
        /// Generates an integer array with many duplicates (high redundancy)
        /// </summary>
        public static int[] GenerateRandomDuplicateIntArray(int size)
        {
            int[] array = new int[size];
            // Use a small constant range to ensure high duplication regardless of size
            int range = 20;
            for (int i = 0; i < size; i++)
            {
                array[i] = _random.Next(0, range);
            }
            return array;
        }

       
        // ========== DOUBLE ARRAYS ==========

        /// <summary>
        /// Generates a random double array
        /// </summary>
        public static double[] GenerateRandomDoubleArray(int size)
        {
            double[] array = new double[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = _random.NextDouble();
            }
            return array;
        }

        /// <summary>
        /// Generates a sorted double array
        /// </summary>
        public static double[] GenerateSortedDoubleArray(int size)
        {
            double[] array = GenerateRandomDoubleArray(size);
            Array.Sort(array);
            return array;
        }

        /// <summary>
        /// Generates a reverse sorted double array
        /// </summary>
        public static double[] GenerateReverseSortedDoubleArray(int size)
        {
            double[] array = GenerateSortedDoubleArray(size);
            Array.Reverse(array);
            return array;
        }
    }
}
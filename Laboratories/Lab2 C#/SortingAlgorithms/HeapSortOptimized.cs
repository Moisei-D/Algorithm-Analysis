using System;

namespace Lab2.SortingAlgorithms
{
    public static class HeapSortOptimized
    {
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            int n = arr.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
            {
                SiftDown(arr, i, n);
            }

            for (int i = n - 1; i > 0; i--)
            {
                Swap(arr, 0, i);
                SiftDown(arr, 0, i);
            }
        }

        private static void SiftDown(int[] arr, int root, int heapSize)
        {
            while (true)
            {
                int left = 2 * root + 1;
                if (left >= heapSize)
                {
                    break;
                }

                int right = left + 1;
                int swapIndex = left;

                if (right < heapSize && arr[right] > arr[left])
                {
                    swapIndex = right;
                }

                if (arr[root] >= arr[swapIndex])
                {
                    break;
                }

                Swap(arr, root, swapIndex);
                root = swapIndex;
            }
        }

        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}

using System;

namespace Lab2.SortingAlgorithms
{
    public static class HeapSortBasic
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
                Heapify(arr, n, i);
            }

            for (int i = n - 1; i > 0; i--)
            {
                Swap(arr, 0, i);
                Heapify(arr, i, 0);
            }
        }

        private static void Heapify(int[] arr, int heapSize, int rootIndex)
        {
            int largest = rootIndex;
            int left = 2 * rootIndex + 1;
            int right = 2 * rootIndex + 2;

            if (left < heapSize && arr[left] > arr[largest])
            {
                largest = left;
            }

            if (right < heapSize && arr[right] > arr[largest])
            {
                largest = right;
            }

            if (largest != rootIndex)
            {
                Swap(arr, rootIndex, largest);
                Heapify(arr, heapSize, largest);
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

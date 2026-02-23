using System;

namespace Lab2.SortingAlgorithms
{
    public static class QuickSortOptimized
    {
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            Sort(arr, 0, arr.Length - 1);
        }

        private static void Sort(int[] arr, int left, int right)
        {
            while (left < right)
            {
                int pivotIndex = HoarePartition(arr, left, right);

                if (pivotIndex - left < right - pivotIndex)
                {
                    Sort(arr, left, pivotIndex);
                    left = pivotIndex + 1;
                }
                else
                {
                    Sort(arr, pivotIndex + 1, right);
                    right = pivotIndex;
                }
            }
        }

        private static int HoarePartition(int[] arr, int left, int right)
        {
            int pivotIndex = MedianOfThree(arr, left, right);
            Swap(arr, left, pivotIndex);
            int pivot = arr[left];

            int i = left - 1;
            int j = right + 1;

            while (true)
            {
                do
                {
                    i++;
                } while (arr[i] < pivot);

                do
                {
                    j--;
                } while (arr[j] > pivot);

                if (i >= j)
                {
                    return j;
                }

                Swap(arr, i, j);
            }
        }

        private static int MedianOfThree(int[] arr, int left, int right)
        {
            int mid = left + (right - left) / 2;

            if (arr[left] > arr[mid])
            {
                Swap(arr, left, mid);
            }

            if (arr[left] > arr[right])
            {
                Swap(arr, left, right);
            }

            if (arr[mid] > arr[right])
            {
                Swap(arr, mid, right);
            }

            return mid;
        }

        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }
}

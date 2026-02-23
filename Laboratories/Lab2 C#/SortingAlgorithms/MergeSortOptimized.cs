using System;

namespace Lab2.SortingAlgorithms
{
    public static class MergeSortOptimized
    {
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            int[] buffer = new int[arr.Length];
            Sort(arr, buffer, 0, arr.Length - 1);
        }

        private static void Sort(int[] arr, int[] buffer, int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int mid = left + (right - left) / 2;
            Sort(arr, buffer, left, mid);
            Sort(arr, buffer, mid + 1, right);

            if (arr[mid] <= arr[mid + 1])
            {
                return;
            }

            Merge(arr, buffer, left, mid, right);
        }

        private static void Merge(int[] arr, int[] buffer, int left, int mid, int right)
        {
            Array.Copy(arr, left, buffer, left, right - left + 1);

            int i = left;
            int j = mid + 1;
            int k = left;

            while (i <= mid && j <= right)
            {
                if (buffer[i] <= buffer[j])
                {
                    arr[k++] = buffer[i++];
                }
                else
                {
                    arr[k++] = buffer[j++];
                }
            }

            while (i <= mid)
            {
                arr[k++] = buffer[i++];
            }
        }
    }
}

using System;

namespace Lab2.SortingAlgorithms
{
    public static class MergeSortBasic
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
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                Sort(arr, left, mid);
                Sort(arr, mid + 1, right);
                Merge(arr, left, mid, right);
            }
        }

        private static void Merge(int[] arr, int l, int m, int r)
        {
            int[] leftArr = arr[l..(m + 1)];
            int[] rightArr = arr[(m + 1)..(r + 1)];
            int i = 0;
            int j = 0;
            int k = l;

            while (i < leftArr.Length && j < rightArr.Length)
            {
                arr[k++] = leftArr[i] <= rightArr[j] ? leftArr[i++] : rightArr[j++];
            }

            while (i < leftArr.Length)
            {
                arr[k++] = leftArr[i++];
            }

            while (j < rightArr.Length)
            {
                arr[k++] = rightArr[j++];
            }
        }
    }
}

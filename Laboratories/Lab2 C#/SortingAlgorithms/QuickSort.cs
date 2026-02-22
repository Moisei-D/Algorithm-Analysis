using System;
namespace Lab2.SortingAlgorithms
{
    public static class QuickSort
    {
        public static void Sort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = HoarePartition(arr, left, right);
                Sort(arr, left, pivotIndex);
                Sort(arr, pivotIndex + 1, right);
            }
        }

        public static void Sort(int[] arr)
        {
            Sort(arr, 0, arr.Length - 1);
        }

        private static int HoarePartition(int[] arr, int left, int right)
        {
            int pivot = arr[left]; //Choosing first element as pivot
            int i = left - 1;
            int j = right + 1;
            while (true)
            {
                //Move i to the rigth until finding element >= pivot
                do
                {
                    i++;
                } while (arr[i] < pivot);

                //Move j to the left until finding element <= pivot
                do
                {
                    j--;
                } while (arr[j] > pivot);

                //If pointers crossed, return partition index
                if (i >= j)
                    return j;

                //Swap elements at i and j
                Swap(arr, i, j);
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

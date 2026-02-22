using System;

namespace Lab2.SortingAlgorithms
{
    public static class HeapSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            
            //Build the max heap
            for (int i = n/2 -1; i >= 0; i--)
            {
                Heapify(arr, n, i);
            }
            
            //Extract elements from heap one by one
            for (int i= n -1; i > 0; i--)
            {
                //Move current root to end
                Swap(arr, 0, i);

                //Heapify the reduced heap
                Heapify(arr, i, 0);
            }

        }
        private static void Heapify(int[] arr, int heapSize, int rootIndex)
        {
            int largest = rootIndex;
            int left = 2 * rootIndex + 1;
            int right = 2 * rootIndex + 2;

            //If left child is larger than root
            if(left < heapSize && arr[left] > arr[largest])
            {
                largest = left;
            }

            // If right child is larger than largest so far
            if (right < heapSize && arr[right] > arr[largest])
            {
                largest = right;
            }

            //If largest is not root
            if (largest != rootIndex)
            {
                Swap(arr, rootIndex, largest);
                //Recursively heapify the affected sub-tree
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
using System;
using System.Collections.Generic;

namespace Lab2.SortingAlgorithms
{
    public static class PatienceSortOptimized
    {
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
            {
                return;
            }

            var piles = new List<List<int>>();
            var pileTops = new List<int>();

            foreach (int x in arr)
            {
                int pileIndex = BinarySearchPiles(pileTops, x);

                if (pileIndex == -1)
                {
                    piles.Add(new List<int> { x });
                    pileTops.Add(x);
                }
                else
                {
                    piles[pileIndex].Add(x);
                    pileTops[pileIndex] = x;
                }
            }

            var pq = new PriorityQueue<int, int>();

            for (int i = 0; i < piles.Count; i++)
            {
                pq.Enqueue(i, piles[i][piles[i].Count - 1]);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                int winningPileIndex = pq.Dequeue();
                var winningPile = piles[winningPileIndex];

                int val = winningPile[winningPile.Count - 1];
                arr[i] = val;
                winningPile.RemoveAt(winningPile.Count - 1);

                if (winningPile.Count > 0)
                {
                    int newTop = winningPile[winningPile.Count - 1];
                    pq.Enqueue(winningPileIndex, newTop);
                }
            }
        }

        private static int BinarySearchPiles(List<int> tops, int value)
        {
            int left = 0;
            int right = tops.Count - 1;
            int bestPile = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (tops[mid] >= value)
                {
                    bestPile = mid;
                    right = mid - 1;
                }
                else
                {
                    left = mid + 1;
                }
            }

            return bestPile;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Lab2.SortingAlgorithms
{
    public static class PatienceSort
    {
        public static void Sort(int[] arr)
        {
            if (arr == null || arr.Length < 2)
                return;

            // List of piles, where each pile is a list of integers (treated as a stack)
            var piles = new List<List<int>>();

            // Optimization for Phase 1: Maintain a list of top cards for faster cache-friendly binary search
            // This avoids resolving multiple references (List->List->int) during the search
            var pileTops = new List<int>();

            // Phase 1: Distribute elements into piles
            foreach (int x in arr)
            {
                // Find the leftmost pile where the top element >= x
                int pileIndex = BinarySearchPiles(pileTops, x);

                if (pileIndex == -1)
                {
                    // Create new pile
                    piles.Add(new List<int> { x });
                    pileTops.Add(x);
                }
                else
                {
                    // Add to existing pile
                    piles[pileIndex].Add(x);
                    pileTops[pileIndex] = x;
                }
            }

            // Phase 2: Merge piles efficiently
            // Use a PriorityQueue to efficiently find the minimum top card among all piles.
            // PriorityQueue stores the pile index, priority is the value of the top card.
            var pq = new PriorityQueue<int, int>();

            for (int i = 0; i < piles.Count; i++)
            {
                // Enqueue the index of the pile, with the value of its top card as priority
                // The top card is at the end of the List (Stack behavior)
                pq.Enqueue(i, piles[i][piles[i].Count - 1]);
            }

            for (int i = 0; i < arr.Length; i++)
            {
                // 1. Get the pile index with the smallest top card
                int winningPileIndex = pq.Dequeue();
                var winningPile = piles[winningPileIndex];

                // 2. Pop the card (smallest globally) and place it in the array
                int val = winningPile[winningPile.Count - 1];
                arr[i] = val;
                winningPile.RemoveAt(winningPile.Count - 1);

                // 3. If the pile still has cards, check the new top card and add back to PQ
                if (winningPile.Count > 0)
                {
                    int newTop = winningPile[winningPile.Count - 1];
                    pq.Enqueue(winningPileIndex, newTop);
                }
            }
        }

        // Binary search on the 'pileTops' list
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
                    right = mid - 1; // Try to find a pile further to the left
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
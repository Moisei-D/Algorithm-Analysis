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

            // List of piles, where each pile is a list of integers
            var piles = new List<List<int>>();

            // Phase 1: Distribute elements into piles
            foreach (int x in arr)
            {
                // Find the first pile where the top element >= x (Binary Search)
                // If no such pile exists, create a new pile

                int pileIndex = FindPile(piles, x);

                if (pileIndex == -1)
                {
                    // Create new pile
                    var newPile = new List<int> { x };
                    piles.Add(newPile);
                }
                else
                {
                    // Add to existing pile
                    piles[pileIndex].Add(x);
                }
            }

            // Phase 2: Merge piles using a Min-Heap (Priority Queue) approach
            // Since we can't assume C# 6+ PriorityQueue is available in all environments,
            // we'll do a simple K-way merge implementation

            // To reconstruct the sorted array, we repeatedly pick the smallest visible top card
            // Note: In patience sort logic for LIS, we stack distinct piles.
            // For sorting, "piles" are just sorted stacks (buckets).
            // Actually, standard Patience Sort requires merging piles efficiently.

            // Reconstruct array
            for (int i = 0; i < arr.Length; i++)
            {
                int minVal = int.MaxValue;
                int minPileIndex = -1;

                for (int j = 0; j < piles.Count; j++)
                {
                    var pile = piles[j];
                    if (pile.Count > 0)
                    {
                        // In Patience Sort, piles are sorted such that bottom is smallest?
                        // Actually, binary placement puts SMALLER elements on TOP.
                        // So the last element added (Top of Stack) is the smallest in that pile context?
                        // Correct Patience Sort Logic:
                        // - Pile top is always smaller than element below it.
                        // - Piles are sorted from left to right by their top cards.

                        int topVal = pile[pile.Count - 1]; // Top of stack
                        if (topVal < minVal)
                        {
                            minVal = topVal;
                            minPileIndex = j;
                        }
                    }
                }

                arr[i] = minVal;
                var winningPile = piles[minPileIndex];
                winningPile.RemoveAt(winningPile.Count - 1); // Pop

                // If pile empty, we could remove it to optimize, but index stability matters for loop
                if (winningPile.Count == 0)
                {
                    piles.RemoveAt(minPileIndex);
                    // Decrement i is not needed, but loop bounds change
                    // Optimization: We iterate j < piles.Count
                }
            }
        }

        // Binary search to find the pile index
        private static int FindPile(List<List<int>> piles, int value)
        {
            // We need to find the leftmost pile whose TOP card is >= value
            int left = 0;
            int right = piles.Count - 1;
            int bestPile = -1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int topCard = piles[mid][piles[mid].Count - 1];

                if (topCard >= value)
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
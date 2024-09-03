using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _975OddEvenJump
    {
        public int OddEvenJumps(int[] arr)
        {
            GetAdjIndices(arr, out int[] targetIndiceOfOddJump, out int[] targetIndicefEvenJump);

            // dp[i, j] indicates if index i is a good index when it comes to jump j(even 0 or odd 1)
            bool[,] dp = new bool[arr.Length, 2];
            dp[arr.Length - 1, 0] = dp[arr.Length - 1, 1] = true; // base states
            int indices = 1;
            for (int i = arr.Length - 2; i >= 0; --i)
            {
                // test index i with odd jump
                if (targetIndiceOfOddJump[i] != -1)
                {
                    if (dp[i, 1] = dp[targetIndiceOfOddJump[i], 0])
                    {
                        ++indices;
                    }
                }

                if (targetIndicefEvenJump[i] != -1)
                {
                    // test index i with even jump
                    dp[i, 0] = dp[targetIndicefEvenJump[i], 1];
                }
            }

            return indices;
        }

        public int OddEvenJumps_BottomUp(int[] arr)
        {
            var ss = new SortedSet<int>();

            // dp[i, j] indicates if index i is a good index when it comes to jump j(even 0 or odd 1)
            bool[,] dp = new bool[arr.Length, 2];
            dp[arr.Length - 1, 0] = dp[arr.Length - 1, 1] = true; // base states
            int indices = 1;
            ss.Add(arr[arr.Length - 1]);
            var map = new Dictionary<int, int>(arr.Length);
            map.Add(arr[arr.Length - 1], arr.Length - 1);
            for (int i = arr.Length - 2; i >= 0; --i)
            {
                // test index i with odd jump
                var highers = ss.GetViewBetween(arr[i], int.MaxValue);
                if (highers.Any())
                {
                    if (dp[i, 1] = dp[map[highers.First()], 0])
                        ++indices;
                }

                var lowers = ss.GetViewBetween(int.MinValue, arr[i]);
                if (lowers.Any())
                {
                    // test index i with even jump
                    dp[i, 0] = dp[map[lowers.Last()], 1];
                }

                ss.Add(arr[i]);
                map[arr[i]] = i;
            }

            return indices;
        }

        private bool?[,] cache;

        public int OddEvenJumps_TopDown(int[] arr)
        {
            this.cache = new bool?[arr.Length, 2];
            this.GetAdjIndices(arr, out int[] targetIndiceOfOddJum, out int[] targetIndicefEvenJump);

            int indices = 0;
            for (int i = 0; i < arr.Length; ++i)
                if (OddEvenJumpsImpl(arr, i, 1, targetIndiceOfOddJum, targetIndicefEvenJump))
                    ++indices;
            return indices;
        }

        private void GetAdjIndices(int[] arr, out int[] targetIndiceOfOddJump, out int[] targetIndicefEvenJump)
        {
            targetIndiceOfOddJump = new int[arr.Length];
            targetIndicefEvenJump = new int[arr.Length];
            int[] indicesIncreasing = new int[arr.Length], indicesDecreasing = new int[arr.Length];
            for(int i = 0; i < arr.Length; ++i)
            {
                indicesIncreasing[i] = i;
                indicesDecreasing[i] = i;
            }

            Array.Sort(indicesIncreasing, (index1, index2) => arr[index1] - arr[index2] != 0 ? arr[index1] - arr[index2] : index1 - index2);

            Stack<int> stack = new Stack<int>(indicesIncreasing.Length);
            for(int i = indicesIncreasing.Length - 1; i >= 0;  --i)
            {
                targetIndiceOfOddJump[indicesIncreasing[i]] = -1;
                while (stack.Count > 0 && stack.Peek() < indicesIncreasing[i])
                    stack.Pop();

                if (stack.Count > 0)
                    targetIndiceOfOddJump[indicesIncreasing[i]] = stack.Peek();

                stack.Push(indicesIncreasing[i]);
            }

            stack.Clear();
            Array.Sort(indicesDecreasing, (index1, index2) => arr[index2] - arr[index1] != 0 ? arr[index2] - arr[index1] : index1 - index2);
            for (int i = indicesDecreasing.Length - 1; i >= 0; --i)
            {
                targetIndicefEvenJump[indicesDecreasing[i]] = -1;
                while (stack.Count > 0 && stack.Peek() < indicesDecreasing[i])
                    stack.Pop();

                if (stack.Count > 0)
                    targetIndicefEvenJump[indicesDecreasing[i]] = stack.Peek();

                stack.Push(indicesDecreasing[i]);
            }
        }

        private void GetAdjIndicesV1(int[] arr, out int[] targetIndiceOfOddJum, out int[] targetIndicefEvenJump)
        {
            targetIndiceOfOddJum = new int[arr.Length - 1];
            targetIndicefEvenJump = new int[arr.Length - 1];
            for (int i = 0; i < arr.Length - 1; ++i)
            {
                targetIndiceOfOddJum[i] = targetIndicefEvenJump[i] = -1;
                int targetValueOfOddJump = int.MaxValue, targetIndexOfOddJump = -1;
                int targetValueOfEvenJump = int.MinValue, targetIndexOfEvenJump = -1;
                for (int j = i + 1; j < arr.Length; ++j)
                {
                    if (arr[j] >= arr[i] && arr[j] < targetValueOfOddJump)
                    {
                        targetIndexOfOddJump = j;
                        targetValueOfOddJump = arr[j];
                    }

                    if (arr[j] <= arr[i] && arr[j] > targetValueOfEvenJump)
                    {
                        targetIndexOfEvenJump = j;
                        targetValueOfEvenJump = arr[j];
                    }
                }

                targetIndiceOfOddJum[i] = targetIndexOfOddJump;
                targetIndicefEvenJump[i] = targetIndexOfEvenJump;
            }
        }

        private bool OddEvenJumpsImpl(int[] arr, int index, int jumps, int[] targetIndiceOfOddJum, int[] targetIndicefEvenJump)
        {
            if (index == arr.Length - 1)
                return true;

            if (this.cache[index, jumps] != null)
                return this.cache[index, jumps].Value;

            bool goodIndex = false;
            if ((jumps % 2) == 1)
            {
                if (targetIndiceOfOddJum[index] != -1)
                {
                    // odd jump
                    goodIndex = OddEvenJumpsImpl(arr, targetIndiceOfOddJum[index], 0, targetIndiceOfOddJum, targetIndicefEvenJump); // try even as next jump
                }
            }
            else
            {
                if (targetIndicefEvenJump[index] != -1)
                {
                    // even jump
                    goodIndex = OddEvenJumpsImpl(arr, targetIndicefEvenJump[index], 1, targetIndiceOfOddJum, targetIndicefEvenJump); // try odd as next jump
                }
            }

            this.cache[index, jumps] = goodIndex;
            return goodIndex;
        }
    }
}

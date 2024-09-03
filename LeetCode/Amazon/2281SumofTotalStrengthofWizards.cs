using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _2281SumofTotalStrengthofWizards
    {
        private const int M = 1000000007;

        public int TotalStrength(int[] strength)
        {
            // prefixSum[i] is the sum of strength[0, ..., i-1]
            int[] prefixSum = new int[strength.Length + 1];
            for (int i = 1; i < prefixSum.Length; ++i)
            {
                prefixSum[i] = (prefixSum[i - 1] + strength[i - 1]) % M;
            }

            int[] leftBoundary = new int[strength.Length];
            Stack<int> stack = new();
            for (int i = 0; i < strength.Length; ++i)
            {
                while (stack.Count != 0 && strength[stack.Peek()] >= strength[i])
                    stack.Pop();

                leftBoundary[i] = stack.Count != 0 ? stack.Peek() : -1;
                stack.Push(i);
            }

            stack.Clear();
            int[] rightBoundary = new int[strength.Length];
            for (int i = strength.Length - 1; i >= 0; --i)
            {
                while (stack.Count != 0 && strength[stack.Peek()] > strength[i])
                    stack.Pop();

                rightBoundary[i] = stack.Count != 0 ? stack.Peek() : strength.Length;
                stack.Push(i);
            }

            // prefixPrefixSum[i] is the prefix sum of prefixSum[0, ... ,i - 1]
            long[] prefixPrefixSum = new long[prefixSum.Length + 1];
            for (int i = 1; i < prefixPrefixSum.Length; ++i)
            {
                prefixPrefixSum[i] = (prefixPrefixSum[i - 1] + prefixSum[i - 1]) % M;
            }

            long res = 0;
            for (int i = 0; i < strength.Length; ++i)
            {
                int left = leftBoundary[i], right = rightBoundary[i];
                long negative = 0; // (prefixSum[left + 1] + prefixSum[left + 2] + ... + prefixSum[i]) * (right - 1 - i + 1);
                negative = (prefixPrefixSum[i + 1] - prefixPrefixSum[left + 1]) * (right - i) % M;
                long positive = 0; // (prefixSum[i + 1] + prefixSum[i + 2] + ... + prefixSum[right]) * (i - (left + 1) + 1);
                positive = (prefixPrefixSum[right + 1] - prefixPrefixSum[i + 1]) * (i - left) % M;

                /*
                for (int j = left + 1; j <= i; ++j)
                    negative = (negative + prefixSum[j]) % M;
                negative = (negative * (right - 1 - i + 1)) % M;

                for (int j = i + 1; j <= right; ++j)
                    positive = (positive + prefixSum[j]) % M;
                positive = (positive * (i - (left + 1) + 1)) % M;
                */

                res = (res + ((((positive + 2 * M - negative) % M) * strength[i]) % M)) % M;
            }

            return (int)res;
        }
    }
}

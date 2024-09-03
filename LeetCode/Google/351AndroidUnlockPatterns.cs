using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _351AndroidUnlockPatterns
    {
        private static Number[] nums;

        static _351AndroidUnlockPatterns()
        {
            nums = new Number[N + 1];
            nums[1] = new Number(1, new int[,] { { 2, 0 }, { 3, 2 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 4 }, { 8, 0 }, { 9, 5 } });
            nums[2] = new Number(2, new int[,] { { 1, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 8, 5 }, { 9, 0 } });
            nums[3] = new Number(3, new int[,] { { 1, 2 }, { 2, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 5 }, { 8, 0 }, { 9, 6 } });
            nums[4] = new Number(4, new int[,] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 5, 0 }, { 6, 5 }, { 7, 0 }, { 8, 0 }, { 9, 0 } });
            nums[5] = new Number(5, new int[,] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 6, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } });
            nums[6] = new Number(6, new int[,] { { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 5 }, { 5, 0 }, { 7, 0 }, { 8, 0 }, { 9, 0 } });
            nums[7] = new Number(7, new int[,] { { 1, 4 }, { 2, 0 }, { 3, 5 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 8, 0 }, { 9, 8 } });
            nums[8] = new Number(8, new int[,] { { 1, 0 }, { 2, 5 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, { 9, 0 } });
            nums[9] = new Number(9, new int[,] { { 1, 5 }, { 2, 0 }, { 3, 6 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 8 }, { 8, 0 } });
        }

        public int NumberOfPatterns(int m, int n)
        {
            int[,] indices = new int[,] { { 1, 4 }, { 2, 4 }, { 5, 1 } };
            bool[] used = new bool[N + 1];
            var stack = new Stack<State>(n);
            int res = 0;
            for (int i = 0; i < indices.GetLength(0); ++i)
            {
                int seq = 0;
                int index = indices[i, 0], multiplier = indices[i, 1];
                used[index] = true;
                stack.Push(new State(nums[index]));
                while (stack.Count != 0)
                {
                    var state = stack.Peek();
                    if (state.index >= N - 1)
                    {
                        if (stack.Count >= m && stack.Count <= n)
                            ++seq;

                        used[state.num.val] = false;
                        stack.Pop();
                    }
                    else
                    {
                        int nextNum = state.num.nextAdjNumbers[state.index, 0], requiredNum = state.num.nextAdjNumbers[state.index, 1];
                        ++state.index;
                        if (!used[nextNum] && (requiredNum == 0 || (requiredNum != 0 && used[requiredNum])))
                        {
                            used[nextNum] = true;
                            stack.Push(new State(nums[nextNum]));
                        }
                    }
                }

                res += seq * multiplier;
            }

            return res;
        }

        public const int N = 9;

        public int NumberOfPatternsV1(int m, int n)
        {
            bool[] used = new bool[N + 1];
            var stack = new Stack<State>(n);
            int seq = 0;
            for (int i = 1; i <= N; ++i)
            {
                used[i] = true;
                stack.Push(new State(nums[i]));
                while (stack.Count != 0)
                {
                    var state = stack.Peek();
                    if (state.index >= N - 1)
                    {
                        if (stack.Count >= m && stack.Count <= n)
                            ++seq;

                        used[state.num.val] = false;
                        stack.Pop();
                    }
                    else
                    {
                        int nextNum = state.num.nextAdjNumbers[state.index, 0], requiredNum = state.num.nextAdjNumbers[state.index, 1];
                        ++state.index;
                        if (!used[nextNum] && (requiredNum == 0 || (requiredNum != 0 && used[requiredNum])))
                        {
                            used[nextNum] = true;
                            stack.Push(new State(nums[nextNum]));
                        }
                    }
                }
            }

            return seq;
        }
    }

    public class State
    {
        public Number num;

        public int index;

        public State(Number num)
        {
            this.num = num;
            this.index = 0;
        }
    }

    public class Number
    {
        public int val;

        public int[,] nextAdjNumbers;

        public Number(int val, int[,] adjNums)
        {
            this.val = val;
            this.nextAdjNumbers = adjNums;
        }
    }
}

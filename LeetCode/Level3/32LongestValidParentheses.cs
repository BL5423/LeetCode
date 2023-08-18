using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _32LongestValidParentheses
    {
        public int LongestValidParentheses(string s)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            int maxLength = 0;
            int[] dp = new int[s.Length];
            for (int i = 1; i < s.Length; ++i)
            {
                dp[i] = 0;
                if (s[i] == ')')
                {
                    if (s[i - 1] == '(')
                    {
                        dp[i] = 2 + (i - 2 >= 0 ? dp[i - 2] : 0);
                    }
                    else
                    {
                        int index = i - dp[i - 1] - 1;
                        if (index >= 0 && s[index] == '(')
                        {
                            dp[i] = dp[i - 1] + 2;

                            if (index - 1 >= 0 && dp[index - 1] != 0)
                                dp[i] += dp[index - 1];
                        }
                    }

                    maxLength = Math.Max(maxLength, dp[i]);
                }
            }

            return maxLength;
        }

        public int LongestValidParentheses_Stack(string s)
        {
            int length = 0;
            if (!string.IsNullOrEmpty(s))
            {
                // stack tracks the index of characters that haven't been matched yet
                Stack<int> stack = new Stack<int>(s.Length + 1);
                stack.Push(-1);
                for(int index = 0; index < s.Length; ++index)
                {
                    if (s[index] == '(')
                    {
                        stack.Push(index);
                    }
                    else
                    {
                        stack.Pop();
                        if (stack.Count != 0)
                        {
                            length = Math.Max(length, index - stack.Peek());
                        }
                        else
                        {
                            // ')' is pending to be matched although there wont be any match, we just need to remember its index for following valid parenthese
                            stack.Push(index);
                        }
                    }
                }
            }

            return length;
        }

        public int LongestValidParentheses_DP(string s)
        {
            int length = 0;
            if (!string.IsNullOrEmpty(s))
            {
                // dp[i] is the length of valid parenthese that ends at s[i]
                int[] dp = new int[s.Length];
                for(int i = 0; i < s.Length; ++i)
                {
                    if (s[i] == ')')
                    {
                        if (i > 0 && s[i - 1] == ')')
                        {
                            // check the character right before the valid parenthese ends at s[i-1]
                            int index1 = i - 1 - dp[i - 1];
                            if (index1 >= 0 && s[index1] == '(')
                            {
                                // s[index1] matches s[i]
                                dp[i] = 2 + dp[i - 1];
                                if (index1 > 0)
                                {
                                    // append the valid parenthese(if any) right before index1
                                    int index2 = index1 - 1;
                                    dp[i] += dp[index2];
                                }
                            }
                        }
                        else if (i >= 1 && s[i - 1] == '(')
                        {
                            // s[i-1] and s[i] compose a valid pair of parenthese '()'
                            dp[i] = (i > 1 ? dp[i - 2] : 0) + 2;
                        }
                    }

                    if (dp[i] > length)
                        length = dp[i];
                }
            }

            return length;
        }

        public int LongestValidParentheses_StackAndDP(string s)
        {
            int length = 0;
            if (!string.IsNullOrEmpty(s))
            {
                // dp[i] is the max length of valid parenthese starts from s[i]
                int[] dp = new int[s.Length];

                Stack<int> stack = new Stack<int>(s.Length);
                for(int index = 0; index < s.Length; ++index)
                {
                    char p = s[index];
                    if (p == '(')
                    {
                        stack.Push(index);
                    }
                    else if (p == ')')
                    {
                        if (stack.Count > 0)
                        {
                            var prevIndex = stack.Pop();
                            dp[prevIndex] = index - prevIndex + 1;
                        }
                    }
                    else
                    {
                        // invalid character found
                        return -1;
                    }
                }

                // the accumulate length of valid parenthese
                int len = 0;
                for(int index = 0; index < s.Length;)
                {
                    if (dp[index] == 0)
                    {
                        // reset
                        len = 0;
                        ++index;
                        continue;
                    }

                    len += dp[index];
                    if (len > length)
                    {
                        length = len;
                    }

                    index += dp[index];
                }
            }

            return length;
        }
    }
}

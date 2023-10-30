using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _22GenerateParentheses
    {
        public IList<string> GenerateParenthesisDFS(int n)
        {
            var res = new List<string>();
            Stack<Parenthes> stack = new Stack<Parenthes>();
            var list = new LinkedList<string>();
            list.AddLast("(");
            stack.Push(new Parenthes() { processedTimes = 0, leftParenthesToMatch = 1});
            while (stack.Count != 0)
            {
                var parenthes = stack.Peek();
                if (list.Count > n * 2 || parenthes.leftParenthesToMatch < 0 || parenthes.leftParenthesToMatch > n)
                {
                    list.RemoveLast();
                    stack.Pop();
                }
                else if (list.Count == n * 2 && parenthes.leftParenthesToMatch == 0)
                {
                    res.Add(string.Join("", list.ToList()));

                    list.RemoveLast();
                    stack.Pop();
                }
                else
                {
                    ++parenthes.processedTimes;
                    if (parenthes.processedTimes == 1)
                    {
                        // process left branch
                        list.AddLast("(");
                        stack.Push(new Parenthes() { processedTimes = 0, leftParenthesToMatch = parenthes.leftParenthesToMatch + 1 });
                    }
                    else if (parenthes.processedTimes == 2)
                    {
                        // process right branch
                        list.AddLast(")");
                        stack.Push(new Parenthes() { processedTimes = 0, leftParenthesToMatch = parenthes.leftParenthesToMatch - 1 });
                    }
                    else
                    {
                        // we have done processing the item on stack's top
                        list.RemoveLast();
                        stack.Pop();
                    }
                }
            }

            return res;
        }

        public IList<string> GenerateParenthesisBFS(int n)
        {
            var res = new List<string>();
            Queue<(string, int)> queue = new Queue<(string, int)>();
            queue.Enqueue(("(", 1));
            while (queue.Count != 0)
            {
                var item = queue.Dequeue();
                if (item.Item1.Length == n * 2 && item.Item2 == 0)
                {
                    res.Add(item.Item1);
                }
                else if (item.Item2 >= 0 && item.Item1.Length + item.Item2 <= n * 2)
                {
                    queue.Enqueue((string.Concat(item.Item1, "("), item.Item2 + 1));
                    queue.Enqueue((string.Concat(item.Item1, ")"), item.Item2 - 1));
                }
            }

            return res;
        }

        public IList<string> GenerateParenthesisV1(int n)
        {
            var parentTheses = new HashSet<string>();
            if (n < 1)
                return parentTheses.ToList();
            parentTheses.Add("()");
            --n;

            while (--n >= 0)
            {
                parentTheses = GenerateOneMoreParentheses(parentTheses);
            }

            return parentTheses.ToList();
        }

        private HashSet<string> GenerateOneMoreParentheses(HashSet<string> parentheses)
        {
            var newParentheses = new HashSet<string>();
            foreach (var parenthese in parentheses)
            {
                for(int index = 0; index < parenthese.Length; ++index)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(parenthese.Substring(0, index));
                    sb.Append("()");
                    sb.Append(parenthese.Substring(index));

                    newParentheses.Add(sb.ToString());
                }
            }

            return newParentheses;
        }
    }

    public class Parenthes
    {
        public int processedTimes = 0;

        public int leftParenthesToMatch = 0;
    }

    public class _22GenerateParenthesesV2
    {
        public IList<string> GenerateParenthesis(int n)
        {
            var results = new List<string>();
            Generate("", n, n, n*2, results);

            return results;
        }

        private void Generate(string input, int numberOfLeft, int numberOfRight, int total, IList<string> results)
        {
            if (input.Length == total)
            {
                results.Add(input);
                return;
            }

            // We can start an opening bracket if we still have one(of n) left to place.
            
            if (numberOfLeft > 0)
            {
                Generate(input + "(", numberOfLeft - 1, numberOfRight, total, results);
            }

            // And we can start a closing bracket if it would not exceed the number of opening brackets.
            if (numberOfRight > 0 && numberOfLeft < numberOfRight)
            {
                Generate(input + ")", numberOfLeft, numberOfRight - 1, total, results);
            }
        }
    }
}

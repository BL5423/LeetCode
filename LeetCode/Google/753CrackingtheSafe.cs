using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _753CrackingtheSafe
    {
        public string CrackSafe(int n, int k)
        {
            HashSet<string> seen = new HashSet<string>();
            var list = new LinkedList<int>();
            for (int i = 0; i < n; ++i)
                list.AddLast(0);
            seen.Add(new string('0', n));

            for (int i = 0; i < Math.Pow(k, n); ++i)
            {
                string prefix = string.Join("", list.TakeLast(n - 1));
                for (int j = k - 1; j >= 0; --j)
                {
                    string next = prefix + j;
                    if (!seen.Contains(next))
                    {
                        seen.Add(next);
                        list.AddLast(j);
                        break;
                    }
                }
            }

            return string.Join("", list);
        }

        public string CrackSafeDFS(int n, int k)
        {
            int total = (int)Math.Pow(k, n);
            var list = new LinkedList<int>();
            for (int i = 0; i < n; ++i)
                list.AddLast(0);
            var seen = new HashSet<string>();
            seen.Add(new string('0', n));

            var stack = new Stack<int>();
            stack.Push(0);
            while (stack.Count > 0)
            {
                if (seen.Count == total)
                    return string.Join("", list);

                var index = stack.Peek();
                if (index < k)
                {
                    list.AddLast(index);
                    var str = string.Join("", list.TakeLast(n));
                    if (!seen.Contains(str))
                    {
                        seen.Add(str);
                        stack.Pop();
                        stack.Push(index + 1);
                        stack.Push(0);
                    }
                    else
                    {
                        stack.Pop();
                        stack.Push(index + 1);
                        list.RemoveLast();
                    }
                }
                else // index == k
                {
                    stack.Pop();
                    var str = string.Join("", list.TakeLast(n));
                    seen.Remove(str);
                    list.RemoveLast();
                }
            }

            return null;
        }
    }
}

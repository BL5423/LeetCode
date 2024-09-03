using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _254FactorCombinations
    {
        public IList<IList<int>> GetFactors(int n)
        {
            var res = new List<IList<int>>();
            var list = new LinkedList<int>();
            list.AddLast(n);
            GetFactorsRecursiveImpl(res, n, list);
            return res;
        }

        private void GetFactorsRecursiveImpl(IList<IList<int>> res, int n, LinkedList<int> list)
        {
            int lastNum = list.Last();
            list.RemoveLast();
            int startNum = list.Count > 0 ? list.Last() : 2;
            for (int i = startNum; i <= Math.Sqrt(lastNum); ++i)
            {
                if (lastNum % i == 0)
                {
                    int a = i, b = lastNum / i;
                    list.AddLast(Math.Min(a, b));
                    list.AddLast(Math.Max(a, b));
                    GetFactorsRecursiveImpl(res, n, list);
                    list.RemoveLast();
                    list.RemoveLast();
                }
            }

            list.AddLast(lastNum);
            if (lastNum > 1 && lastNum < n)
                res.Add(list.ToList());
        }

        public IList<IList<int>> GetFactorsIterative(int n)
        {
            var res = new List<IList<int>>();
            var queue = new Queue<List<int>>();
            queue.Enqueue(new List<int>() { n });
            while (queue.Count > 0)
            {
                var list = queue.Dequeue();
                if (list.Count > 1)
                    res.Add(list);

                int lastNum = list.Last();
                int startNum = list.Count > 1 ? list[list.Count - 2] : 2;
                for (int i = startNum; i <= Math.Sqrt(lastNum); ++i)
                {
                    if (lastNum % i == 0)
                    {
                        var nextList = new List<int>(list);
                        nextList.RemoveAt(nextList.Count - 1);
                        int a = i, b = lastNum / i;
                        nextList.Add(Math.Min(a, b));
                        nextList.Add(Math.Max(a, b));
                        queue.Enqueue(nextList);
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> GetFactorsIterativeV1(int n)
        {
            var res = new List<IList<int>>();
            var seen = new HashSet<string>();
            var queue = new Queue<(List<int>, int)>();
            for(int i = 2; i <= Math.Sqrt(n); ++i)
            {
                if (n % i == 0)
                {
                    queue.Enqueue((new List<int>() { i, n / i }, 0));
                }
            }

            while (queue.Count > 0)
            {
                var list = queue.Dequeue();
                res.Add(list.Item1);

                while (list.Item2 < list.Item1.Count)
                {
                    int nextNum = list.Item1[list.Item2];
                    if (nextNum > 3)
                    {
                        for (int i = 2; i <= Math.Sqrt(nextNum); ++i)
                        {
                            if (nextNum % i == 0)
                            {
                                var nextList = new List<int>(list.Item1);
                                nextList[list.Item2] = i;
                                nextList.Insert(list.Item2 + 1, nextNum / i);

                                var sortedList = new List<int>(nextList);
                                sortedList.Sort();
                                var key = string.Join(",", sortedList);
                                if (seen.Add(key))
                                {
                                    queue.Enqueue((nextList, list.Item2));
                                }
                            }
                        }
                    }

                    ++list.Item2;
                }
            }

            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _809ExpressiveWords
    {
        public int ExpressiveWords(string s, string[] words)
        {
            if (string.IsNullOrEmpty(s))
                return 0;

            var sList = Convert(s);
            int res = 0;
            foreach (var word in words)
            {
                var list = Convert(word);
                if (sList.Count != list.Count)
                    continue;

                LinkedListNode<(char, int)> sNode = sList.First, node = list.First;
                while (sNode != null)
                {
                    if (sNode.Value.Item1 != node.Value.Item1)
                        break;

                    if (sNode.Value.Item2 < node.Value.Item2 ||
                       (sNode.Value.Item2 != node.Value.Item2 && sNode.Value.Item2 < 3))
                        break;

                    sNode = sNode.Next;
                    node = node.Next;
                }

                if (sNode == null)
                    ++res;
            }

            return res;
        }

        private static LinkedList<(char, int)> Convert(string str)
        {
            var list = new LinkedList<(char, int)>();
            char cur = str[0];
            int count = 0;
            foreach (var ch in str)
            {
                if (ch != cur)
                {
                    list.AddLast((cur, count));
                    count = 0;
                }

                cur = ch;
                ++count;
            }

            list.AddLast((cur, count));

            return list;
        }
    }
}

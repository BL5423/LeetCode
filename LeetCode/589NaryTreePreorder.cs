using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Node
    {
        public int val;
        public IList<Node> children;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
    }

    public class _589NaryTreePreorder
    {
        public IList<int> Preorder(Node root)
        {
            var result = new List<int>();
            if (root != null)
            {
                var stack = new Stack<Tuple<Node, IEnumerator<Node>>>();
                stack.Push(new Tuple<Node, IEnumerator<Node>>(root, root.children.GetEnumerator()));
                while (stack.Count() > 0)
                {
                    var item = stack.Peek();
                    if (item.Item2 == null || item.Item2.Current == null)
                    {
                        result.Add(item.Item1.val);
                    }
                    if (item.Item2 != null && item.Item2.MoveNext())
                    {
                        var current = item.Item2.Current;
                        IEnumerator<Node> enumerator = null;
                        if (current.children != null)
                        {
                            enumerator = current.children.GetEnumerator();
                        }
                        stack.Push(new Tuple<Node, IEnumerator<Node>>(current, enumerator));
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
            }

            return result;
        }

        public IList<int> PreorderV1(Node root)
        {
            var result = new List<int>();

            Preorder(root, result);

            return result;
        }

        private void Preorder(Node root, IList<int> result)
        {
            if (root != null)
            {
                result.Add(root.val);
                foreach(var kid in root.children)
                {
                    Preorder(kid, result);
                }
            }
        }
    }
}

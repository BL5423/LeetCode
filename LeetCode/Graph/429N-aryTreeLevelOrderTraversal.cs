using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph2
{
    public class _429N_aryTreeLevelOrderTraversal
    {
        public IList<IList<int>> LevelOrder_DFS(Node root)
        {
            var res = new List<IList<int>>();
            if (root != null)
            {
                var stack = new Stack<(Node, int)>();
                stack.Push((root, 1));

                while (stack.Count != 0)
                {
                    var item = stack.Pop();
                    var node = item.Item1;
                    var level = item.Item2;

                    if (res.Count < level)
                    {
                        res.Add(new List<int>());
                    }

                    res[level - 1].Add(node.val);

                    if (node.children != null)
                    {
                        for(int i = node.children.Count - 1; i >= 0; --i)
                        {
                            stack.Push((node.children[i], level + 1));
                        }
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> LevelOrder_BFS(Node root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root != null)
            {
                Queue<Node> queue = new Queue<Node>();
                queue.Enqueue(root);

                while (queue.Count != 0)
                {
                    int count = queue.Count;
                    var level = new List<int>(count);
                    for(int i = 0; i < count; ++i)
                    {
                        var node = queue.Dequeue();
                        if (node.children != null)
                        {
                            foreach(var child in node.children)
                            {
                                queue.Enqueue(child);
                            }
                        }

                        level.Add(node.val);
                    }

                    res.Add(level);
                }
            }

            return res;
        }
    }

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
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _133CloneGraph
    {
        public Node CloneGraph__BFS_Iteratively(Node node)
        {
            Node copy = null;
            if (node != null)
            {
                Dictionary<int, Node> cache = new Dictionary<int, Node>();
                Queue<(Node, Node)> queue = new Queue<(Node, Node)>();
                copy = this.ShadowCopy(node);
                queue.Enqueue((node, copy));
                cache.Add(copy.val, copy);

                while (queue.Count != 0)
                {
                    var top = queue.Dequeue();
                    var topNode = top.Item1;
                    var topCopy = top.Item2;
                    foreach(var neighbor in topNode.neighbors)
                    {
                        if (!cache.TryGetValue(neighbor.val, out Node copiedNeightbor))
                        {
                            copiedNeightbor = this.ShadowCopy(neighbor);
                            cache.Add(neighbor.val, copiedNeightbor);
                            queue.Enqueue((neighbor, copiedNeightbor));
                        }

                        topCopy.neighbors.Add(copiedNeightbor);
                    }
                }
            }

            return copy;
        }

        public Node CloneGraph__DFS_Iteratively(Node node)
        {
            Node copy = null;
            if (node != null)
            {
                Dictionary<int, Node> cache = new Dictionary<int, Node>();
                Stack<(Node, IEnumerator<Node>, Node)> stack = new Stack<(Node, IEnumerator<Node>, Node)>();
                copy = this.ShadowCopy(node);
                stack.Push((node, node.neighbors?.GetEnumerator(), copy));
                cache.Add(copy.val, copy);

                while (stack.Count != 0)
                {
                    var top = stack.Peek();
                    var topNode = top.Item1;
                    var topEnum = top.Item2;
                    var topCopy = top.Item3;

                    if (topEnum != null && topEnum.MoveNext())
                    {
                        var nextNode = topEnum.Current;
                        if (!cache.TryGetValue(nextNode.val, out Node nextCopy))
                        {
                            nextCopy = this.ShadowCopy(nextNode);
                            stack.Push((nextNode, nextNode.neighbors?.GetEnumerator(), nextCopy));
                            cache.Add(nextCopy.val, nextCopy);
                        }

                        topCopy.neighbors.Add(nextCopy);
                    }
                    else
                    {
                        stack.Pop();
                    }
                }
            }

            return copy;
        }

        private Node ShadowCopy(Node node)
        {
            List<Node> neighbors = null;
            if (node.neighbors != null)
            {
                neighbors = new List<Node>(node.neighbors.Count);
            }

            return new Node(node.val, neighbors);
        }

        public Node CloneGraph_Recursively(Node node)
        {
            Dictionary<int, Node> cache = new Dictionary<int, Node>();
            return this.Copy(node, cache);
        }

        private Node Copy(Node node, Dictionary<int, Node> cache)
        {
            if (node == null)
                return null;

            if (!cache.TryGetValue(node.val, out Node copy))
            {
                List<Node> neighbors = null;
                if (node.neighbors != null)
                {
                    neighbors = new List<Node>(node.neighbors.Count);
                }

                copy = new Node(node.val, neighbors);
                cache.Add(copy.val, copy);

                foreach (var neighbor in node.neighbors)
                {
                    copy.neighbors.Add(this.Copy(neighbor, cache));
                }
            }

            return copy;
        }
    }

    public class Node
    {
        public int val;
        public IList<Node> neighbors;

        public Node()
        {
            val = 0;
            neighbors = new List<Node>();
        }

        public Node(int _val)
        {
            val = _val;
            neighbors = new List<Node>();
        }

        public Node(int _val, List<Node> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }
}

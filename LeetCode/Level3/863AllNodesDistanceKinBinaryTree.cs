using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public enum Branch
    {
        Root,

        Left,

        Right
    }

    public class _863AllNodesDistanceKinBinaryTree
    {
        public IList<int> DistanceK(TreeNode root, TreeNode target, int k)
        {
            var v2e = new Dictionary<int, LinkedList<TreeNode>>();
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                if (!v2e.TryGetValue(node.val, out LinkedList<TreeNode> edges))
                {
                    edges = new LinkedList<TreeNode>();
                    v2e.Add(node.val, edges);
                }

                if (node.left != null)
                {
                    queue.Enqueue(node.left);

                    edges.AddLast(node.left);
                    if (!v2e.TryGetValue(node.left.val, out LinkedList<TreeNode> edges1))
                    {
                        edges1 = new LinkedList<TreeNode>();
                        v2e.Add(node.left.val, edges1);
                    }
                    edges1.AddLast(node);
                }
                if (node.right != null)
                {
                    queue.Enqueue(node.right);

                    edges.AddLast(node.right);
                    if (!v2e.TryGetValue(node.right.val, out LinkedList<TreeNode> edges2))
                    {
                        edges2 = new LinkedList<TreeNode>();
                        v2e.Add(node.right.val, edges2);
                    }
                    edges2.AddLast(node);
                }
            }

            HashSet<int> visited = new HashSet<int>();
            var bfsQueue = new Queue<int>();
            bfsQueue.Enqueue(target.val);
            visited.Add(target.val);
            int dis = 0;
            while (bfsQueue.Count != 0 && dis++ < k)
            {
                int count = bfsQueue.Count;
                for(int i = 0; i < count; ++i)
                {
                    var node = bfsQueue.Dequeue();
                    if (v2e.TryGetValue(node, out LinkedList<TreeNode> edges))
                    {
                        foreach(var edge in edges)
                        {
                            if (visited.Add(edge.val))
                                bfsQueue.Enqueue(edge.val);
                        }
                    }
                }
            }

            return new List<int>(bfsQueue);
        }

        public IList<int> DistanceK_V1(TreeNode root, TreeNode target, int k)
        {
            LinkedList<LinkedList<(int, int, Branch)>> layersCache = new LinkedList<LinkedList<(int, int, Branch)>>();
            Queue<(TreeNode, Branch)> queue = new Queue<(TreeNode, Branch)>();
            queue.Enqueue((root, Branch.Root));
            Branch targetBranch = Branch.Root;
            int targetLayer = -1, layers = 0, index = 0, layersInScope = 2 * k + 1;
            while (queue.Count != 0)
            {
                if (targetLayer != -1 && layersCache.Count >= layersInScope)
                    break;

                // roll over on cache to keep only the most recent k * 2 + 1 layers
                if (index / layersInScope > 0)
                {
                    layersCache.RemoveFirst();
                    index -= layersInScope;
                }

                var nodes = new LinkedList<(int, int, Branch)>();
                layersCache.AddLast(nodes);

                int count = queue.Count;
                for(int i = 0; i < count; ++i)
                {
                    var item = queue.Dequeue();
                    var node = item.Item1;
                    var branch = item.Item2;
                    nodes.AddLast((node.val, layers, branch));

                    if (target.val == node.val)
                    {
                        targetLayer = layers;
                        targetBranch = branch;
                    }

                    if (node.left != null)
                        queue.Enqueue((node.left, branch != Branch.Root ? branch : Branch.Left));
                    if (node.right != null)
                        queue.Enqueue((node.right, branch != Branch.Root ? branch : Branch.Right));
                }

                ++layers;
                ++index;
            }

            var res = new List<int>();
            foreach(var layer in layersCache)
            {
                foreach(var item in layer)
                {
                    int val = item.Item1;
                    int currentLayer = item.Item2;
                    var branch = item.Item3;
                    if ((branch == targetBranch || branch == Branch.Root || targetBranch == Branch.Root) 
                        && Math.Abs(currentLayer - targetLayer) == k)
                    {
                        res.Add(val);
                    }
                    else if (branch != targetBranch && currentLayer + targetLayer == k)
                    {
                        res.Add(val);
                    }
                }
            }

            return res;
        }
    }
}

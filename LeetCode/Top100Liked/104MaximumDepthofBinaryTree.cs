using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _104MaximumDepthofBinaryTree
    {
        public int MaxDepthV3(TreeNode root)
        {
            int depth = 0;
            if (root != null)
            {
                Stack<(TreeNode, int)> stack = new Stack<(TreeNode, int)>();
                stack.Push((root, 1));
                while (stack.Count > 0)
                {
                    var item = stack.Pop();
                    if (item.Item2 > depth)
                    {
                        depth = item.Item2;
                    }

                    if (item.Item1.right != null)
                    {
                        stack.Push((item.Item1.right, item.Item2 + 1));
                    }
                    if (item.Item1.left != null)
                    {
                        stack.Push((item.Item1.left, item.Item2 + 1));
                    }
                }
            }

            return depth;
        }

        public int MaxDepthV2(TreeNode root)
        {
            int depth = 0;
            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    ++depth;
                    int count = queue.Count;
                    for(int i = 0; i < count; ++i)
                    {
                        var node = queue.Dequeue();
                        if (node.left != null)
                            queue.Enqueue(node.left);
                        if (node.right != null)
                            queue.Enqueue(node.right);
                    }
                }
            }

            return depth;
        }

        private int maxDepth = 0;

        public int MaxDepthV1(TreeNode root)
        {
            if (root != null)
                this.MaxDepthRecursive(root, 1);

            return maxDepth;
        }

        private void MaxDepthRecursive(TreeNode node, int depth) 
        {
            if (depth > maxDepth)
            {
                maxDepth = depth;
            }

            if (node.left != null)
            {
                this.MaxDepthRecursive(node.left, depth + 1);
            }
            if (node.right != null)
            {
                this.MaxDepthRecursive(node.right, depth + 1);
            }
        }
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}

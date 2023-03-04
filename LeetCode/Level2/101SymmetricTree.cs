using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _101SymmetricTree
    {
        public bool IsSymmetric(TreeNode root)
        {
            Stack<(TreeNode, TreeNode)> stack = new Stack<(TreeNode, TreeNode)>();
            stack.Push((root, root));
            while (stack.Count > 0)
            {
                var top = stack.Pop();
                var node1 = top.Item1;
                var node2 = top.Item2;

                if (node1 == null && node2 == null)
                    continue;

                if (node1 == null || node2 == null || node1.val != node2.val)
                    return false;

                stack.Push((node1.left, node2.right));
                stack.Push((node1.right, node2.left));
            }

            return true;
        }

        public bool IsSymmetric_LevelTraverse(TreeNode root)
        {
            if (root == null)
                return true;

            Queue<TreeNode> leftQueue = new Queue<TreeNode>();
            leftQueue.Enqueue(root.left);
            Queue<TreeNode> rightQueue = new Queue<TreeNode>();
            rightQueue.Enqueue(root.right);
            while (leftQueue.Count == rightQueue.Count)
            {
                int count = leftQueue.Count;
                if (count == 0)
                    return true;

                while (--count >= 0)
                {
                    var left = leftQueue.Dequeue();
                    var right = rightQueue.Dequeue();
                    if (left == null && right == null)
                        continue;

                    if ((left == null && right != null) ||
                        (left != null && right == null) ||
                        (left.val != right.val))
                    {
                        return false;
                    }

                    leftQueue.Enqueue(left.right);
                    leftQueue.Enqueue(left.left);

                    rightQueue.Enqueue(right.left);
                    rightQueue.Enqueue(right.right);
                }
            }

            return false;
        }

        public bool IsSymmetric_Recursively(TreeNode root)
        {
            if (root == null)
                return true;

            return IsMirror(root.left, root.right);
        }

        private bool IsMirror(TreeNode node1, TreeNode node2)
        {
            if (node1 == null && node2 == null)
                return true;
            
            if ((node1 == null && node2 != null) ||
                (node1 != null && node2 == null) ||
                (node1.val != node2.val))
                return false;
                        
            if (!IsMirror(node1.left, node2.right))
                return false;

            if (!IsMirror(node1.right, node2.left))
                return false;

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
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

    public class _110BalancedBinaryTree
    {
        public bool IsBalanced(TreeNode root)
        {
            if (root != null)
            {
                Dictionary<TreeNode, int> heights = new Dictionary<TreeNode, int>();
                heights.Add(root, 0);
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var top = stack.Peek();
                    if (top.left != null && !heights.ContainsKey(top.left))
                    {
                        stack.Push(top.left);
                    }
                    else if (top.right != null && !heights.ContainsKey(top.right))
                    {
                        stack.Push(top.right);
                    }
                    else
                    {
                        int lHeight = top.left == null ? 0 : heights[top.left];
                        int rHeight = top.right == null ? 0 : heights[top.right];
                        if (lHeight > rHeight + 1 ||
                            rHeight > lHeight + 1)
                            return false;

                        heights[top] = Math.Max(lHeight, rHeight) + 1;
                        stack.Pop();
                    }
                }
            }

            return true;
        }

        public bool IsBalancedV2(TreeNode root)
        {
            return Height(root) != -1;
        }

        private int Height(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int leftHeight = Height(root.left);
            int rightHeight = Height(root.right);

            if (leftHeight == -1 || 
                rightHeight == -1 ||
                leftHeight > rightHeight + 1 ||
                rightHeight > leftHeight + 1)
            {
                return -1;
            }

            return Math.Max(leftHeight, rightHeight) + 1;
        }

        public bool IsBalancedV1(TreeNode root)
        {
            bool ret = true;
            if (root != null)
            {
                int leftHeight = HeightV1(root.left);
                int rightHeight = HeightV1(root.right);
                if (leftHeight > rightHeight + 1 ||
                    rightHeight > leftHeight + 1)
                {
                    ret = false;
                }
                else if (!IsBalancedV1(root.left) || !IsBalancedV1(root.right))
                {
                    ret = false;
                }
            }

            return ret;
        }

        private int HeightV1(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int leftHeight = HeightV1(root.left);
            int rightHeigh = HeightV1(root.right);

            return Math.Max(leftHeight, rightHeigh) + 1;
        }
    }
}

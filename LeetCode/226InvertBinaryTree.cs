using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp226
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

    public class _226InvertBinaryTree
    {
        public TreeNode InvertTree(TreeNode root)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var node = stack.Pop();
                    var left = node.left;
                    node.left = node.right;
                    node.right = left;

                    if (node.left != null)
                    {
                        stack.Push(node.left);
                    }
                    if (node.right != null)
                    {
                        stack.Push(node.right);
                    }
                }
            }

            return root;
        }
                 
        public TreeNode InvertTreeRecursive(TreeNode root)
        {
            if (root == null)
                return root;

            TreeNode left = root.left;
            root.left = root.right;
            root.right = left;

            InvertTree(root.left);
            InvertTree(root.right);

            return root;
        }
    }
}

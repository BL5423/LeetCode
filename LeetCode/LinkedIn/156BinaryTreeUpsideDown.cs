using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _156BinaryTreeUpsideDown
    {
        private TreeNode globalRoot = null;

        public TreeNode UpsideDownBinaryTree(TreeNode root)
        {
            if (root != null)
            {
                UpsideDownIterativelyV2(root);
            }

            return globalRoot;
        }

        private void UpsideDownIterativelyV2(TreeNode node)
        {
            var root = node;
            var left = node.left;
            var right = node.right;
            root.left = null;
            root.right = null;
            while (left != null)
            {
                var nextRoot = left;
                var nextLeft = left.left;
                var nextRight = left.right;

                left.left = right;
                left.right = root;

                root = nextRoot;
                left = nextLeft;
                right = nextRight;
            }

            this.globalRoot = root;
        }

        private void UpsideDownIterativelyV1(TreeNode node)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            while (node != null)
            {
                stack.Push(node);
                node = node.left;
            }

            var left = this.globalRoot = stack.Pop();
            while (stack.Count != 0)
            {
                var root = stack.Pop();
                var right = root.right;

                left.left = right;
                left.right = root;

                root.left = null;
                root.right = null;

                left = root;
            }
        }

        private void UpsideDownRecursively(TreeNode node)
        {
            if (node.left != null)
            {
                UpsideDownBinaryTree(node.left);

                var left = node.left;
                var right = node.right;
                left.left = right;
                left.right = node;

                node.left = null;
                node.right = null;
            }
            else
            {
                globalRoot = node;
            }
        }
    }
}

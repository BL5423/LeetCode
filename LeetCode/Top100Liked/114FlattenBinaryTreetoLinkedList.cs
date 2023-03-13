using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _114FlattenBinaryTreetoLinkedList
    {
        public void FlattenV3(TreeNode root)
        {
            if (root != null)
            {
                var prior = new TreeNode(0);
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    TreeNode node = stack.Pop();
                    prior.right = node;
                    prior.left = null;

                    if (node.right != null)
                        stack.Push(node.right);
                    if (node.left != null)
                        stack.Push(node.left);

                    prior = node;
                }
            }
        }

        public void FlattenV2(TreeNode root)
        {
            // morris algo
            TreeNode node = root;
            while (node != null)
            {
                var pre = node.left;
                if (pre != null)
                {
                    while (pre.right != null)
                        pre = pre.right;

                    pre.right = node.right;
                    node.right = node.left;
                    node.left = null;
                }

                node = node.right;
            }
        }

        public void FlattenV1(TreeNode root)
        {
            FlattenRecursive(root);
        }

        private TreeNode FlattenRecursive(TreeNode node)
        {
            if (node != null)
            {
                var left = this.FlattenRecursive(node.left);
                var right = this.FlattenRecursive(node.right);
                if (left == null)
                    node.right = right;
                else
                {
                    var end = left;
                    while (end.right != null)
                        end = end.right;

                    end.right = right;

                    node.right = left;
                }

                node.left = null;
            }

            return node;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _235LowestCommonAncestorofaBinarySearchTree
    {
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
                return null;

            var left = p.val < q.val ? p : q;
            var right = p.val > q.val ? p : q;

            if (root.val >= left.val && root.val <= right.val)
                return root;

            if (root.val > right.val)
            {
                // search in left
                return LowestCommonAncestor(root.left, p, q);
            }
            else
            {
                // search in right
                return LowestCommonAncestor(root.right, p, q);
            }
        }

        public TreeNode LowestCommonAncestorIterative(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode node = root;
            var left = p.val < q.val ? p : q;
            var right = p.val > q.val ? p : q;
            while (node != null)
            {
                if (node.val >= left.val && node.val <= right.val)
                    return node;

                if (node.val > right.val)
                {
                    // search in left
                    node = node.left;
                }
                else
                {
                    // search in right
                    node = node.right;
                }
            }

            return null;
        }
    }
}

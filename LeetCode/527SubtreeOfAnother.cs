using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp527
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

    public class _527SubtreeOfAnother
    {
        public bool IsSubtree(TreeNode root, TreeNode subRoot)
        {
            if (root == null && subRoot == null)
                return true;
            if (root != null && subRoot == null)
                return false;
            if (root == null && subRoot != null)
                return false;

            if (root.val == subRoot.val)
            {
                // compare subtrees
                if (IsSameTree(root.left, subRoot.left) && IsSameTree(root.right, subRoot.right))
                    return true;
            }

            if (IsSubtree(root.left, subRoot))
                return true;

            if (IsSubtree(root.right, subRoot))
                return true;

            return false;
        }

        public bool IsSameTree(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
                return true;
            if ((root1 != null && root2 == null) ||
                    (root1 == null && root2 != null))
                return false;

            if (root1.val == root2.val)
            {
                return IsSameTree(root1.left, root2.left) && IsSameTree(root1.right, root2.right);
            }

            return false;
        }
    }
}

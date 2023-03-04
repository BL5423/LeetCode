using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp669
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

    public class _669TrimBinarySearchTree
    {
        public TreeNode TrimBST(TreeNode root, int low, int high)
        {
            if (root == null)
                return root;

            if (root.val < low)
            {
                root = root.right;
                return TrimBST(root, low, high);
            }
            if (root.val > high)
            {
                root = root.left;
                return TrimBST(root, low, high);
            }

            root.left = TrimBST(root.left, low, high);
            root.right = TrimBST(root.right, low, high);
            return root;
        }
    }
}

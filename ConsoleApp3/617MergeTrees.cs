using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp617
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

    public class _617MergeTrees
    {
        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            // merge root
            TreeNode root = null;
            if (root1 == null)
                root = root2;
            else if (root2 == null)
                root = root1;
            else
            {
                root = root1;
                root.val += root2.val;
            }

            if (root1 != null && root2 != null)
            {
                // merge subtress
                root.left = MergeTrees(root1.left, root2.left);
                root.right = MergeTrees(root1.right, root2.right);
            }

            return root;
        }
    }
}

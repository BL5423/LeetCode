using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.DP
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

    public class _337HouseRobberIII
    {
        private Dictionary<TreeNode, int> cache = new Dictionary<TreeNode, int>();

        public int Rob(TreeNode root)
        {
            if (root == null)
                return 0;

            if (root.left == null && root.right == null) 
                return root.val;

            if (cache.ContainsKey(root))
                return cache[root];

            var robLeft = this.Rob(root.left);
            var robRight = this.Rob(root.right);
            var robLeftBranch = 0;
            if (root.left != null)
                robLeftBranch = this.Rob(root.left.left) + this.Rob(root.left.right);

            var robRightBranch = 0;
            if (root.right != null)
                robRightBranch = this.Rob(root.right.left) + this.Rob(root.right.right);

            return cache[root] = Math.Max(robLeft + robRight, root.val + robLeftBranch + robRightBranch);
        }
    }
}

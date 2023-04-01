using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _270ClosestBinarySearchTreeValue
    {
        private double minDiff = double.MaxValue;

        private int value = -1;

        public int ClosestValue(TreeNode root, double target)
        {
            this.ClosetValueIterative(root, target);
            return this.value;
        }

        private void ClosetValueIterative(TreeNode root, double target)
        {
            TreeNode node = root;
            while (node != null)
            {
                double diff = Math.Abs(node.val - target);
                if (diff < this.minDiff)
                {
                    this.minDiff = diff;
                    this.value = node.val;
                }

                if (node.val < target)
                {
                    node = node.right;
                }
                else // node.val >= target
                {
                    node = node.left;
                }
            }
        }

        private void ClosestValueImpl(TreeNode root, double target)
        {
            double diff = Math.Abs(root.val - target);
            if (diff < this.minDiff)
            {
                this.minDiff = diff;
                this.value = root.val;
            }
            if (root.val < target && root.right != null)
            {
                this.ClosestValueImpl(root.right, target);
            }
            else if (root.val > target && root.left != null)
            {
                this.ClosestValueImpl(root.left, target);
            }
        }
    }

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
}

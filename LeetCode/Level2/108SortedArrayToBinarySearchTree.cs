using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
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

    public class _108SortedArrayToBinarySearchTree
    {
        public TreeNode SortedArrayToBST(int[] nums)
        {
            int start = 0, end = nums.Length - 1;
            int middle = start + ((end - start) >> 1);
            TreeNode root = new TreeNode(nums[middle]);
            Stack<(TreeNode, int, int, int)> stack = new Stack<(TreeNode, int, int, int)>();
            stack.Push((root, start, middle, end));
            while (stack.Count > 0)
            {
                var r = stack.Peek();
                if (r.Item2 < r.Item3 && r.Item1.left == null)
                {
                    // build left branch
                    int s = r.Item2;
                    int e = r.Item3 - 1;
                    int m = s + ((e - s) >> 1);
                    var left = new TreeNode(nums[m]);
                    r.Item1.left = left;
                    stack.Push((left, s, m, e));
                }
                else if (r.Item4 > r.Item3 && r.Item1.right == null)
                {
                    // build right branch
                    int s = r.Item3 + 1;
                    int e = r.Item4;
                    int m = s + ((e - s) >> 1);
                    var right = new TreeNode(nums[m]);
                    r.Item1.right = right;
                    stack.Push((right, s, m, e));
                }
                else
                {
                    stack.Pop();
                }
            }

            return root;
        }
        
        public TreeNode SortedArrayToBSTRecursive(int[] nums)
        {
            return SortedArrayToBSTV1(nums, 0, nums.Length - 1);
        }

        private TreeNode SortedArrayToBSTV1(int[] nums, int start, int end)
        {
            if (start > end)
                return null;

            int middle = start + (end - start) / 2;
            TreeNode root = new TreeNode(nums[middle]);
            root.left = SortedArrayToBSTV1(nums, start, middle - 1);
            root.right = SortedArrayToBSTV1(nums, middle + 1, end);

            return root;
        }
    }
}

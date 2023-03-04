using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _124BinaryTreeMaximumPathSum
    {
        const int MinVal = -1001;

        private int maxSum = MinVal;

        private int[] tempVals = new int[6];

        public int MaxPathSum(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            //this.MaxPathSumRecursively(root);
            this.MaxPathSumIteratively(root);
            return maxSum;
        }

        private int MaxPathSumRecursively(TreeNode root)
        {
            int maxLeft = MinVal;
            if (root.left != null)
                maxLeft = MaxPathSumRecursively(root.left);

            int maxRight = MinVal;
            if (root.right != null)
                maxRight = MaxPathSumRecursively(root.right);

            tempVals[0] = maxLeft;
            tempVals[1] = maxRight;
            tempVals[2] = maxLeft + maxRight + root.val;
            tempVals[3] = maxLeft + root.val;
            tempVals[4] = maxRight + root.val;
            tempVals[5] = root.val;
            maxSum = Math.Max(tempVals.Max(), maxSum);

            return tempVals.Skip(3).Max();
        }

        private void MaxPathSumIteratively(TreeNode root)
        {
            Dictionary<TreeNode, int> maxPathSums = new Dictionary<TreeNode, int>();
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var node = stack.Peek();
                int maxLeft = MinVal, maxRight = MinVal;
                if (node.left != null && !maxPathSums.TryGetValue(node.left, out maxLeft))
                {
                    stack.Push(node.left);
                }
                else if (node.right != null && !maxPathSums.TryGetValue(node.right, out maxRight))
                {
                    stack.Push(node.right);
                }
                else
                {
                    tempVals[0] = maxLeft;
                    tempVals[1] = maxRight;
                    tempVals[2] = maxLeft + maxRight + node.val;
                    tempVals[3] = maxLeft + node.val;
                    tempVals[4] = maxRight + node.val;
                    tempVals[5] = node.val;
                    maxSum = Math.Max(tempVals.Max(), maxSum);

                    var potentialMaxForNode = tempVals.Skip(3).Max();
                    if (!maxPathSums.TryGetValue(node, out int curMaxVal) || curMaxVal < potentialMaxForNode)
                    {
                        maxPathSums[node] = potentialMaxForNode;
                    }

                    stack.Pop();
                }
            }
        }
    }
}

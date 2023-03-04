using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public enum Status
    {
        None = 0,

        Left = 1,

        Right = 2
    }

    public class _437PathSumIII
    {
        // similar problem about prefix sum : https://leetcode.com/problems/contiguous-array/
        public int PathSum(TreeNode root, int targetSum)
        {
            int count = 0;
            if (root != null)
            {
                Dictionary<TreeNode, Status> nodeStatus = new Dictionary<TreeNode, Status>();
                nodeStatus.Add(root, Status.None);
                Dictionary<long, int> sumCounts = new Dictionary<long, int>();
                sumCounts.Add(0, 1);
                Stack<(TreeNode, long)> stack = new Stack<(TreeNode, long)>();
                stack.Push((root, root.val));
                if (!sumCounts.TryGetValue(root.val, out int c))
                {
                    sumCounts.Add(root.val, 0);
                }
                ++sumCounts[root.val];

                while (stack.Count > 0)
                {
                    var item = stack.Peek();
                    var node = item.Item1;
                    var sum = item.Item2;
                    var status = nodeStatus[node];

                    if (status == Status.None)
                    {
                        if (node.left != null)
                        {
                            long leftSum = sum + node.left.val;
                            stack.Push((node.left, leftSum));
                            nodeStatus.Add(node.left, Status.None);
                            if (!sumCounts.TryGetValue(leftSum, out c))
                            {
                                sumCounts.Add(leftSum, 0);
                            }
                            ++sumCounts[leftSum];
                        }

                        nodeStatus[node] = Status.Left;
                    }
                    else if (status == Status.Left)
                    {
                        if (node.right != null)
                        {
                            long rightSum = sum + node.right.val;
                            stack.Push((node.right, rightSum));
                            nodeStatus.Add(node.right, Status.None);
                            if (!sumCounts.TryGetValue(rightSum, out c))
                            {
                                sumCounts.Add(rightSum, 0);
                            }
                            ++sumCounts[rightSum];
                        }

                        nodeStatus[node] = Status.Right;
                    }
                    else
                    {
                        // We're done with node and its children
                        stack.Pop();

                        // We should remove its appeareance from the cache when we encounter the node for the first time
                        if (--sumCounts[sum] == 0)
                        {
                            sumCounts.Remove(sum);
                        }

                        long potentialPrevSum = sum - targetSum;
                        if (sumCounts.TryGetValue(potentialPrevSum, out int prevCount))
                        {
                            count += prevCount;
                        }
                    }
                }
            }

            return count;
        }

        public int PathSum_Iteratively(TreeNode root, int targetSum)
        {
            int total = 0;
            if (root != null)
            {
                Dictionary<long, int> sumToCounts = new Dictionary<long, int>();
                Dictionary<TreeNode, long> nodeToSum = new Dictionary<TreeNode, long>();
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                nodeToSum.Add(root, root.val);
                sumToCounts.Add(root.val, 1);
                long sum = 0;
                while (stack.Count > 0)
                {
                    TreeNode top = stack.Peek();
                    if (top.left != null && !nodeToSum.ContainsKey(top.left))
                    {
                        sum = nodeToSum[top] + top.left.val;
                        nodeToSum.Add(top.left, sum);

                        if (sumToCounts.TryGetValue(sum, out int c))
                        {
                            sumToCounts[sum] = ++c;
                        }
                        else
                        {
                            c = 1;
                            sumToCounts[sum] = c;
                        }

                        stack.Push(top.left);
                    }
                    else if (top.right != null && !nodeToSum.ContainsKey(top.right))
                    {
                        sum = nodeToSum[top] + top.right.val;
                        nodeToSum.Add(top.right, sum);

                        if (sumToCounts.TryGetValue(sum, out int c))
                        {
                            sumToCounts[sum] = ++c;
                        }
                        else
                        {
                            c = 1;
                            sumToCounts[sum] = c;
                        }

                        stack.Push(top.right);
                    }
                    else
                    {
                        top = stack.Pop();
                        sum = nodeToSum[top];
                        if (sum == targetSum)
                        {
                            ++total;
                        }

                        --sumToCounts[sum];

                        long prevSum = sum - targetSum;
                        if (sumToCounts.TryGetValue(prevSum, out int c))
                        {
                            total += c;
                        }
                    }
                }
            }

            return total;
        }

        public int PathSumRecursively(TreeNode root, int targetSum)
        {
            int total = 0;
            if (root != null)
            {
                Dictionary<long, int> sumToCounts = new Dictionary<long, int>();
                DFS(root, 0, targetSum, sumToCounts, ref total);
            }

            return total;
        }

        private void DFS(TreeNode root, long sum, int targetSum, Dictionary<long, int> sumToCounts, ref int total)
        {
            sum += root.val;
            if (sum == targetSum)
            {
                ++total;
            }
            long prevSum = sum - targetSum;
            if (sumToCounts.TryGetValue(prevSum, out int count))
            {
                total += count;
            }

            if (sumToCounts.TryGetValue(sum, out int c))
            {
                sumToCounts[sum] = ++c;
            }
            else
            {
                c = 1;
                sumToCounts.Add(sum, c);
            }

            if (root.left != null)
            {
                DFS(root.left, sum, targetSum, sumToCounts, ref total);
            }
            if (root.right != null)
            {
                DFS(root.right, sum, targetSum, sumToCounts, ref total);
            }

            if (c > 1)
            {
                --sumToCounts[sum];
            }
            else
            {
                sumToCounts.Remove(sum);
            }
        }
    }
}

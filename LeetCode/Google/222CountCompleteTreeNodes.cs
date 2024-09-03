using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _222CountCompleteTreeNodes
    {
        public int CountNodes(TreeNode root)
        {
            int count = 0;
            if (root != null)
            {
                int height = 0;
                var node = root;
                while (node != null)
                {
                    ++height;
                    node = node.left;
                }

                if (height == 1)
                {
                    count = 1;
                }
                else
                {
                    count = (int)Math.Pow(2, height - 1) - 1;

                    int start = 1, end = (int)Math.Pow(2, height - 1);
                    int left = start, right = end;
                    while (left <= right)
                    {
                        int mid = left + ((right - left) >> 1);
                        if (Exists(mid, start, end, root))
                        {
                            left = mid + 1;
                        }
                        else // does not exist
                        {
                            right = mid - 1;
                        }
                    }

                    // left > right
                    count += left - 1;
                }
            }

            return count;
        }

        private static bool Exists(int index, int start, int end, TreeNode root)
        {
            var node = root;
            while (start < end && node != null)
            {
                int mid = start + ((end - start) >> 1);
                if (index > mid)
                {
                    node = node.right;
                    start = mid + 1;
                }
                else // index <= mid
                {
                    node = node.left;
                    end = mid;
                }
            }

            return node != null;
        }

        public int CountNodesV1(TreeNode root)
        {
            int count = 0;
            if (root != null)
            {
                int height = 0;
                var node = root;
                while (node != null)
                {
                    ++height;
                    node = node.left;
                }
                count = height > 1 ? (int)Math.Pow(2, height - 1) - 1 : 1;

                var queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                int h = 1;
                while (h < height - 1)
                {
                    ++h;
                    for (int c = queue.Count; c > 0; --c)
                    {
                        var treeNode = queue.Dequeue();
                        if (treeNode.left != null)
                            queue.Enqueue(treeNode.left);
                        if (treeNode.right != null)
                            queue.Enqueue(treeNode.right);
                    }
                }

                while (queue.Count != 0)
                {
                    var treeNode = queue.Dequeue();
                    if (treeNode.left == null)
                    {
                        break;
                    }
                    else if (treeNode.right == null)
                    {
                        ++count;
                        break;
                    }

                    count += 2;
                }
            }

            return count;
        }
    }
}

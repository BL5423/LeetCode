using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _662MaximumWidthofBinaryTree
    {
        private const int NULL = int.MinValue;

        public int WidthOfBinaryTree_Indexing(TreeNode root)
        {
            int maxWidth = 0;
            if (root != null)
            {
                Queue<(TreeNode, int)> queue = new Queue<(TreeNode, int)>();
                queue.Enqueue((root, 0));
                while (queue.Count != 0)
                {
                    int count = queue.Count;
                    int leftMostIndex = -1, rightMostIndex = -1;
                    for (int i = 0; i < count; ++i)
                    {
                        var item = queue.Dequeue();
                        var node = item.Item1;
                        var index = item.Item2;
                        if (leftMostIndex == -1)
                        {
                            leftMostIndex = index;
                        }
                        rightMostIndex = index;

                        if (node.left != null)
                            queue.Enqueue((node.left, index * 2 + 1));

                        if (node.right != null)
                            queue.Enqueue((node.right, index * 2 + 2));
                    }

                    int width = rightMostIndex - leftMostIndex + 1;
                    if (width > maxWidth)
                        maxWidth = width;
                }
            }

            return maxWidth;
        }

        public int WidthOfBinaryTree_VirtualNode(TreeNode root)
        {
            //TLE
            int maxWidth = 0;
            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count != 0)
                {
                    int count = queue.Count;
                    int leftMostIndex = -1, rightMostIndex = -1;
                    for(int i = 0; i < count; ++i)
                    {
                        var node = queue.Dequeue();
                        if (node.val != NULL)
                        {
                            if (leftMostIndex == -1)
                            {
                                leftMostIndex = i;
                            }

                            rightMostIndex = i;
                        }

                        if (leftMostIndex != -1)
                        {
                            queue.Enqueue(node.left != null ? node.left : new TreeNode(NULL));
                            queue.Enqueue(node.right != null ? node.right : new TreeNode(NULL));
                        }
                    }

                    int width = rightMostIndex - leftMostIndex + 1;
                    if (width > maxWidth)
                        maxWidth = width;
                }
            }

            return maxWidth;
        }
    }
}

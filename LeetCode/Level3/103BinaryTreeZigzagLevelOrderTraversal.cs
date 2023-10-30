using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _103BinaryTreeZigzagLevelOrderTraversal
    {
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var res = new List<IList<int>>();
            if (root != null)
            {
                Stack<(TreeNode, int)> stack = new Stack<(TreeNode, int)>();
                stack.Push((root, 1));

                while (stack.Count != 0)
                {
                    var item = stack.Pop();
                    if (item.Item1.left != null)
                        stack.Push((item.Item1.left, item.Item2 + 1));
                    if (item.Item1.right != null)
                        stack.Push((item.Item1.right, item.Item2 + 1));

                    IList<int> list = null;
                    if (res.Count < item.Item2)
                    {
                        list = new List<int>();
                        res.Add(list);
                    }
                    else
                    {
                        list = res[item.Item2 - 1];
                    }

                    if (item.Item2 % 2 == 0)
                        list.Add(item.Item1.val);
                    else
                        list.Insert(0, item.Item1.val);
                }
            }

            return res;
        }

        public IList<IList<int>> ZigzagLevelOrderV1(TreeNode root)
        {
            var res = new List<IList<int>>();
            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                bool zigZag = true;
                while (queue.Count != 0)
                {
                    int count = queue.Count;
                    var list = new List<int>(count);
                    for (int i = 0; i < count; ++i)
                    {
                        var node = queue.Dequeue();
                        list.Add(node.val);

                        if (node.left != null)
                        {
                            queue.Enqueue(node.left);
                        }
                        if (node.right != null)
                        {
                            queue.Enqueue(node.right);
                        }
                    }

                    if (!zigZag)
                    {
                        list.Reverse();
                    }
                    zigZag = !zigZag;
                    res.Add(list);
                }
            }

            return res;
        }
    }
}

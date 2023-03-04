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

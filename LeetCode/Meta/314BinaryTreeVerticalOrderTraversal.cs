using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _314BinaryTreeVerticalOrderTraversal
    {
        public IList<IList<int>> VerticalOrder(TreeNode root)
        {
            var res = new List<IList<int>>();
            if (root != null)
            {
                var index2List = new Dictionary<int, IList<int>>();
                var queue = new Queue<(TreeNode, int)>();
                int leftMostIndex = 0, rightMostIndex = 0;
                queue.Enqueue((root, 0));
                while (queue.Count != 0)
                {
                    var item = queue.Dequeue();
                    var node = item.Item1;
                    var index = item.Item2;
                    if (index < leftMostIndex)
                        leftMostIndex = index;
                    if (index > rightMostIndex)
                        rightMostIndex = index;
                    if (!index2List.TryGetValue(index, out IList<int> list))
                    {
                        list = new List<int>();
                        index2List.Add(index, list);
                    }
                    list.Add(node.val);

                    if (node.left != null)
                        queue.Enqueue((node.left, index - 1));
                    if (node.right != null)
                        queue.Enqueue((node.right, index + 1));
                }

                for (int index = leftMostIndex; index <= rightMostIndex; ++index)
                {
                    res.Add(index2List[index]);
                }
            }

            return res;
        }
    }
}

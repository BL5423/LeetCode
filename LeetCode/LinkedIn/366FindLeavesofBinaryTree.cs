using ConsoleApp236;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class Item
    {
        public TreeNode node;

        public bool leftProcessed, rightProcessed;
    }

    public class _366FindLeavesofBinaryTree
    {
        public IList<IList<int>> FindLeaves(TreeNode root)
        {
            var list = new List<IList<int>>();
            GetHeightIteratively(root, list);

            return list;
        }

        private void GetHeightIteratively(TreeNode root, IList<IList<int>> list)
        {
            var heights = new Dictionary<TreeNode, int>();
            var stack = new Stack<Item>();
            stack.Push(new Item() { node = root });
            while (stack.Count != 0)
            {
                var item = stack.Peek();
                if (!item.leftProcessed && item.node.left != null)
                {
                    stack.Push(new Item() { node = item.node.left });
                    item.leftProcessed = true;
                }
                else if (!item.rightProcessed && item.node.right != null)
                {
                    stack.Push(new Item() { node = item.node.right });
                    item.rightProcessed = true;
                }
                else // if (item.leftPrcessed && item.rightProcessed) or leave
                {
                    int leftHeight = -1, rightHeight = -1;
                    if (item.node.left != null)
                        leftHeight = heights[item.node.left];
                    if (item.node.right != null)
                        rightHeight = heights[item.node.right];

                    int height = Math.Max(leftHeight, rightHeight) + 1;
                    if (height >= list.Count)
                    {
                        list.Add(new List<int>());
                    }
                    list[height].Add(item.node.val);
                    heights.Add(item.node, height);

                    stack.Pop();
                }
            }
        }

        private int GetHeightRecursively(TreeNode node, IList<IList<int>> list)
        {
            if (node == null)
                return -1;

            int leftHeight = GetHeightRecursively(node.left, list);
            int rightHeight = GetHeightRecursively(node.right, list);
            int height = Math.Max(leftHeight, rightHeight) + 1;
            if (height >= list.Count)
            {
                var res = new List<int>();
                list.Add(res);
            }

            list[height].Add(node.val);

            return height;
        }

        public IList<IList<int>> FindLeavesV1(TreeNode root)
        {
            var res = new List<IList<int>>();
            var ingress = new Dictionary<TreeNode, int>();
            var outgress = new Dictionary<TreeNode, TreeNode>();
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            var leaves = new LinkedList<TreeNode>();
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                int _in = 0;
                if (node.right != null)
                {
                    ++_in;
                    outgress.Add(node.right, node);

                    stack.Push(node.right);
                }
                if (node.left != null)
                {
                    ++_in;
                    outgress.Add(node.left, node);

                    stack.Push(node.left);
                }

                ingress.Add(node, _in);
                if (_in == 0)
                    leaves.AddLast(node);
            }

            while (leaves.Count != 0)
            {
                var newLeaves = new LinkedList<TreeNode>();
                var list = new List<int>(leaves.Count);
                var node = leaves.First;
                while (node != null)
                {
                    list.Add(node.Value.val);
                    if (outgress.TryGetValue(node.Value, out TreeNode parent) && --ingress[parent] == 0)
                        newLeaves.AddLast(parent);

                    node = node.Next;
                }

                res.Add(list);
                leaves = newLeaves;
            }

            return res;
        }
    }
}

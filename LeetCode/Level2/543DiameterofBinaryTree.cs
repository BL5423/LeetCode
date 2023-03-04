using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class TempNode
    {
        public TreeNode node;

        public int leftHeight;

        public int rightHeight;

        public int height;

        public bool leftRight;

        public TempNode(TreeNode node, bool leftRight)
        {
            this.node = node;
            this.leftHeight = -2;
            this.rightHeight = -2;
            this.height = -1;
            this.leftRight = leftRight;
        }
    }

    public class _543DiameterofBinaryTree
    {
        public int DiameterOfBinaryTree(TreeNode root)
        {
            int maxDiameter = 0;
            Stack<TempNode> stack = new Stack<TempNode>();
            stack.Push(new TempNode(root, false));
            while (stack.Count > 0)
            {
                var top = stack.Peek();
                if (top.leftHeight != -2 && top.rightHeight != -2)
                {
                    top.height = Math.Max(top.leftHeight, top.rightHeight) + 1;
                    var diameter = top.leftHeight + top.rightHeight + 2;
                    maxDiameter = Math.Max(maxDiameter, diameter);

                    stack.Pop();

                    // update parent
                    if (stack.Count > 0)
                    {
                        var t = stack.Peek();
                        if (top.leftRight)
                            t.leftHeight = top.height;
                        else
                            t.rightHeight = top.height;
                    }
                }
                else if (top.leftHeight == -2)
                {
                    if (top.node.left != null)
                    {
                        stack.Push(new TempNode(top.node.left, true));
                    }
                    else
                    {
                        // no left child
                        top.leftHeight = -1;
                    }
                }
                else
                {
                    if (top.node.right != null)
                    {
                        stack.Push(new TempNode(top.node.right, false));
                    }
                    else
                    {
                        // no right child
                        top.rightHeight = -1;
                    }
                }
            }

            return maxDiameter;
        }

        private int diameter = 0;

        public int DiameterOfBinaryTreeRecursively(TreeNode root)
        {
            this.diameter = 0;
            GetHeight(root);

            return this.diameter;
        }

        private int GetHeight(TreeNode root)
        {
            if (root != null)
            {
                var leftHeight = GetHeight(root.left);
                var righHeight = GetHeight(root.right);
                int d = leftHeight + righHeight + 2;
                if (d > this.diameter)
                {
                    this.diameter = d;
                }

                return Math.Max(leftHeight, righHeight) + 1;
            }

            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _671SecondMinimumNodeInaBinaryTree
    {
        public int FindSecondMinimumValue(TreeNode root)
        {
            bool found = false;
            int secondMinimal = int.MaxValue, minimal = root.val;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var node = stack.Pop();
                if (node.val > minimal && node.val <= secondMinimal)
                {
                    found = true;
                    secondMinimal = node.val;
                }

                if (node.val < secondMinimal)
                {
                    if (node.left != null)
                        stack.Push(node.left);
                    if (node.right != null)
                        stack.Push(node.right);
                }
            }

            return found ? secondMinimal : -1;
        }
    }
}

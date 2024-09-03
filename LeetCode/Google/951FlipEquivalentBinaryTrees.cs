using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _951FlipEquivalentBinaryTrees
    {
        public bool FlipEquiv(TreeNode root1, TreeNode root2)
        {
            var stack = new Stack<(TreeNode, TreeNode)>();
            stack.Push((root1, root2));
            while (stack.Count != 0)
            {
                var pair = stack.Pop();
                TreeNode node1 = pair.Item1, node2 = pair.Item2;
                if (node1 == null && node2 == null)
                    continue;

                if (node1 == null && node2 != null ||
                    node1 != null && node2 == null ||
                    node1.val != node2.val)
                    return false;

                // assumption: val is unique
                if (node1.left?.val == node2.left?.val)
                {
                    stack.Push((node1.left, node2.left));
                    stack.Push((node1.right, node2.right));
                }
                else // node1.left.val != node2.left.val     
                {
                    stack.Push((node1.left, node2.right));
                    stack.Push((node1.right, node2.left));
                }
            }

            return true;
        }

        private static bool FlipEquivImpl(TreeNode node1, TreeNode node2)
        {
            if (node1 == null && node2 == null)
                return true;

            if (node1 == null && node2 != null ||
                node1 != null && node2 == null ||
                node1.val != node2.val)
                return false;

            return FlipEquivImpl(node1.left, node2.left) && FlipEquivImpl(node1.right, node2.right) ||
                FlipEquivImpl(node1.left, node2.right) && FlipEquivImpl(node1.right, node2.left);
        }
    }
}

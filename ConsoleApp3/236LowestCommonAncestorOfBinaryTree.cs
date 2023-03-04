using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp236
{ 
    public class TreeNode
    {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int x) { val = x; }
     }

    public enum State
    {
        BothPending = 0,

        LeftDone = 1,

        RightDone = 2
    }

    public class Wrapper
    {
        public State state;

        public TreeNode node;
    }


    public class _236LowestCommonAncestorOfBinaryTree
    {
        public TreeNode LowestCommonAncestorIterative(TreeNode root, TreeNode p, TreeNode q)
        {
            Stack<Wrapper> stack = new Stack<Wrapper>();
            TreeNode ancestor = null;
            stack.Push(new Wrapper() { node = root, state = State.BothPending });
            bool foundP = false, foundQ = false;
            while (stack.Count > 0)
            {
                var wrapper = stack.Peek();
                if (wrapper.state == State.BothPending)
                {
                    if (wrapper.node == p || wrapper.node == q)
                    {
                        if (ancestor != null)
                            return ancestor;

                        ancestor = wrapper.node;
                    }

                    if (wrapper.node.left != null)
                        stack.Push(new Wrapper() { node = wrapper.node.left, state = State.BothPending });
                    wrapper.state = State.LeftDone;
                }
                else if (wrapper.state == State.LeftDone)
                {
                    if (wrapper.node.right != null)
                        stack.Push(new Wrapper() { node = wrapper.node.right, state = State.BothPending });
                    wrapper.state = State.RightDone;
                }
                else if (wrapper.state == State.RightDone)
                {
                    stack.Pop();
                    if (wrapper.node == ancestor)
                    {
                        ancestor = stack.Peek().node;
                    }
                }
            }

            return null;
        }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            var ancestors = GetAllAncestors(root, p, q);
            var ancestors1 = ancestors[0];
            var ancestors2 = ancestors[1];
            int length1 = ancestors1.Count - 1;
            int length2 = ancestors2.Count - 1;
            while(length1 >= 0 && length2 >= 0)
            {
                if (ancestors1[length1] == ancestors2[length2])
                    return ancestors1[length1];
                else if (length1 > length2)
                {
                    --length1;
                }
                else if (length1 < length2)
                {
                    --length2;
                }
                else
                {
                    --length1;
                    --length2;
                }
            }

            return null;
        }

        private IList<TreeNode>[] GetAllAncestors(TreeNode root, TreeNode node1, TreeNode node2)
        {
            var ancestors = new List<TreeNode>();
            var results = new List<TreeNode>[2];
            GetAllAncestorsRur(root, node1, node2, ancestors, results);
            return results;
        }

        private bool GetAllAncestorsRur(TreeNode root, TreeNode node1, TreeNode node2, IList<TreeNode> ancestors, List<TreeNode>[] results)
        {
            if (root == null)
                return false;

            ancestors.Add(root);
            if (root == node1)
            {
                results[0] = new List<TreeNode>(ancestors);
                if (results[1] != null)
                    return true;
            }
            if (root == node2)
            {
                results[1] = new List<TreeNode>(ancestors);
                if (results[0] != null)
                    return true;
            }

            if (GetAllAncestorsRur(root.left, node1, node2, ancestors, results))
                return true;

            if (GetAllAncestorsRur(root.right, node1, node2, ancestors, results))
                return true;

            ancestors.RemoveAt(ancestors.Count - 1);
            return false;
        }
    }
}

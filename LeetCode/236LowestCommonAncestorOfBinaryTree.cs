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

    public enum Flag
    {
        FindingOneNode,

        FindingAnotherNode
    }

    public class Item
    {
        public TreeNode node;

        public Flag flag;

        public bool leftBranchSearched, rightBranchSearched;
    }


    public class _236LowestCommonAncestorOfBinaryTree
    {

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            Flag curFlag = Flag.FindingOneNode;
            Stack<Item> stack = new Stack<Item>();
            stack.Push(new Item() { node = root, flag = curFlag });
            TreeNode foundNode = null;
            while (stack.Count != 0)
            {
                var item = stack.Peek();
                if (item.node == p || item.node == q)
                {
                    if (curFlag == Flag.FindingOneNode)
                    {
                        // find another node
                        curFlag = Flag.FindingAnotherNode;
                    }
                    else if (foundNode != null && foundNode != item.node)
                    {
                        // find all nodes
                        break;
                    }

                    if (foundNode == null)
                        foundNode = item.node;
                }

                if (!item.leftBranchSearched && item.node.left != null)
                {
                    stack.Push(new Item() { node = item.node.left, flag = curFlag });
                    item.leftBranchSearched = true;
                }
                else if (!item.rightBranchSearched && item.node.right != null)
                {
                    stack.Push(new Item() { node = item.node.right, flag = curFlag });
                    item.rightBranchSearched = true;
                }
                else
                {
                    stack.Pop();
                }
            }

            while (stack.Count != 0)
            {
                var item = stack.Pop();
                // the first node on the top of stack which indicates it was searching the first target node is the lowest ancestor of p and q
                if (item.flag == Flag.FindingOneNode)
                    return item.node;
            }

            // should never reach here
            return null;
        }


        public TreeNode LowestCommonAncestorV3(TreeNode root, TreeNode p, TreeNode q)
        {
            bool pInLeft = root == p || Find(root.left, p);
            bool qInRight = root == q || Find(root.right, q);
            if (pInLeft && qInRight)
                return root;

            bool pInRight = root == p || Find(root.right, p);
            bool qInLeft = root == q || Find(root.left, q);
            if (pInRight && qInLeft)
                return root;

            if (pInLeft && qInLeft)
                return LowestCommonAncestor(root.left, p, q);

            return LowestCommonAncestor(root.right, p, q);
        }

        private Dictionary<(TreeNode, TreeNode), bool> cache = new Dictionary<(TreeNode, TreeNode), bool>();

        private bool Find(TreeNode node, TreeNode targetNode)
        {
            if (node == null)
                return false;

            if (cache.TryGetValue((node, targetNode), out bool val))
                return val;

            if (node == targetNode)
                return true;

            bool find = false;
            find |= Find(node.left, targetNode);

            if (!find)
                find |= Find(node.right, targetNode);

            cache.Add((node, targetNode), find);
            return find;
        }

        public TreeNode LowestCommonAncestorV2(TreeNode root, TreeNode p, TreeNode q)
        {
            var path2P = FindPath(root, p);
            var path2Q = FindPath(root, q);

            TreeNode lca = null;
            var node1 = path2P.First;
            var node2 = path2Q.First;
            while (node1 != null && node2 != null && node1.Value == node2.Value)
            {
                lca = node1.Value;
                node1 = node1.Next;
                node2 = node2.Next;
            }

            return lca;
        }

        private static LinkedList<TreeNode> FindPath(TreeNode root, TreeNode targetNode)
        {
            var res = new LinkedList<TreeNode>();
            DFS(root, targetNode, res);
            return res;
        }

        private static bool DFS(TreeNode node, TreeNode targetNode, LinkedList<TreeNode> res)
        {
            res.AddLast(node);
            if (node != targetNode)
            {
                if (node.left != null)
                {
                    if (DFS(node.left, targetNode, res))
                        return true;
                }
                if (node.right != null)
                {
                    if (DFS(node.right, targetNode, res))
                        return true;
                }
            }
            else
                return true;

            res.RemoveLast();
            return false;
        }

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

        public TreeNode LowestCommonAncestorV1(TreeNode root, TreeNode p, TreeNode q)
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

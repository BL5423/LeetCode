using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _144BinaryTreePreorderTraversal
    {
        public IList<int> PreorderTraversal(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    res.Add(top.val);
                    if (top.right != null)
                    {
                        stack.Push(top.right);
                    }
                    if (top.left != null)
                    {
                        stack.Push(top.left);
                    }
                }
            }

            return res;
        }

        public IList<int> PreorderTraversalMorris(TreeNode root)
        {
            var res = new List<int>();
            var node = root;
            while (node != null)
            {
                if (node.left != null)
                {
                    var predecessor = node.left;
                    while (predecessor.right != null && predecessor.right != node)
                    {
                        predecessor = predecessor.right;
                    }

                    if (predecessor.right == null)
                    {
                        res.Add(node.val);
                        predecessor.right = node;
                        node = node.left;
                    }
                    else // predecessor.right == node
                    {
                        predecessor.right = null;
                        node = node.right;
                    }
                }
                else
                {
                    res.Add(node.val);
                    node = node.right;
                }
            }

            return res;
        }

        public IList<int> PreorderTraversalV1(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                res.Add(root.val);
                var top = root;
                while (top != null || stack.Count > 0)
                {
                    while (top != null && top.left != null)
                    {
                        stack.Push(top.left);
                        res.Add(top.left.val);

                        top = top.left;
                    }

                    top = stack.Pop();
                    top = top.right;
                    if (top != null)
                    {
                        stack.Push(top);
                        res.Add(top.val);
                    }
                }
            }

            return res;
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    if (top.right != null)
                    {
                        stack.Push(top.right);
                    }
                    if (top.left != null)
                    {
                        stack.Push(new TreeNode(top.val));
                        stack.Push(top.left);
                    }
                    else
                    {
                        res.Add(top.val);
                    }
                }
            }

            return res;
        }

        public IList<int> InorderTraversalMorris(TreeNode root)
        {
            var res = new List<int>();
            var node = root;
            while (node != null)
            {
                if (node.left != null)
                {
                    var pre = node.left;
                    while (pre.right != null && pre.right != node)
                    {
                        pre = pre.right;
                    }

                    if (pre.right == null)
                    {
                        pre.right = node;
                        node = node.left;
                    }
                    else // pre.right == node
                    {
                        res.Add(node.val);
                        pre.right = null;
                        node = node.right;
                    }
                }
                else
                {
                    res.Add(node.val);
                    node = node.right;
                }
            }

            return res;
        }

        public IList<int> InorderTraversalV1(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                var top = root;
                while (top != null || stack.Count > 0)
                {
                    while (top != null && top.left != null)
                    {
                        stack.Push(top.left);

                        top = top.left;
                    }

                    top = stack.Pop();
                    res.Add(top.val);
                    top = top.right;
                    if (top != null)
                    {
                        stack.Push(top);
                    }
                }
            }

            return res;
        }

        public IList<int> PostOrderTraversalV1(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                HashSet<TreeNode> visited = new HashSet<TreeNode>();
                stack.Push(root);
                var top = root;
                while (stack.Count > 0)
                {
                    while (top != null && top.left != null)
                    {
                        stack.Push(top.left);
                        top = top.left;
                    }

                    if (top == null)
                    {
                        top = stack.Peek();
                    }

                    top = top.right;
                    if (top != null && visited.Contains(top) == false)
                    {
                        stack.Push(top);
                    }
                    else // top == null
                    {
                        top = null;
                        var node = stack.Pop();
                        res.Add(node.val);
                        visited.Add(node);
                    }
                }
            }

            return res;
        }

        public IList<int> PostOrderTraversal(TreeNode root)
        {
            var res = new List<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    var top = stack.Pop();
                    res.Add(top.val);
                    if (top.left != null)
                    {
                        stack.Push(top.left);
                    }
                    if (top.right != null)
                    {
                        stack.Push(top.right);
                    }
                }
            }

            res.Reverse();
            return res;
        }

        public IList<int> PostOrderTraversalMorris(TreeNode root)
        {
            var res = new List<int>();
            var node = root;
            while (node != null)
            {
                if (node.right != null)
                {
                    var pre = node.right;
                    while (pre.left != null && pre.left != node)
                    {
                        pre = pre.left;
                    }

                    if (pre.left == null)
                    {
                        res.Add(node.val);
                        pre.left = node;
                        node = node.right;
                    }
                    else // pre.left == node
                    {
                        pre.left = null;
                        node = node.left;
                    }
                }
                else
                {
                    res.Add(node.val);
                    node = node.left;
                }
            }

            res.Reverse();
            return res;
        }
    }
}

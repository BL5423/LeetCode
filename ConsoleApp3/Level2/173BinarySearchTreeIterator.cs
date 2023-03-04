using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class BSTIterator
    {
        Stack<TreeNode> stack;

        TreeNode top;

        public BSTIterator(TreeNode root)
        {
            this.stack = new Stack<TreeNode>();
            if (root != null)
            {
                this.top = root;
                this.stack.Push(top);
            }
        }

        private TreeNode InorderIterative()
        {
            while (this.HasNext())
            {
                while (this.top != null && this.top.left != null)
                {
                    this.stack.Push(this.top.left);
                    this.top = this.top.left;
                }

                var ret = this.stack.Pop();
                this.top = ret.right;
                if (this.top != null)
                {
                    this.stack.Push(this.top);
                }

                return ret;
            }

            return null;
        }

        public int Next()
        {
            return this.InorderIterative().val;
        }

        public bool HasNext()
        {
            return stack.Count > 0;
        }
    }

    public class BSTIteratorV2
    {
        Stack<TreeNode> stack;

        public BSTIteratorV2(TreeNode root)
        {
            this.stack = new Stack<TreeNode>();
            while (root != null)
            {
                this.stack.Push(root);
                root = root.left;
            }
        }

        public int Next()
        {
            var top = this.stack.Pop();
            int ret = top.val;
                        
            top = top.right;
            while (top != null)
            {
                this.stack.Push(top);
                top = top.left;
            }            

            return ret;
        }

        public bool HasNext()
        {
            return stack.Count > 0;
        }
    }

    public class BSTIteratorV1
    {
        LinkedList<int> inorder;

        bool initialized = false;

        LinkedList<int>.Enumerator enumerator;

        bool hasNext = false;

        public BSTIteratorV1(TreeNode root)
        {
            this.inorder = new LinkedList<int>();
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                var top = root;
                while (stack.Count > 0)
                {
                    while (top != null && top.left != null)
                    {
                        stack.Push(top.left);
                        top = top.left;
                    }

                    top = stack.Pop();
                    this.inorder.AddLast(top.val);
                    top = top.right;
                    if (top != null)
                    {
                        stack.Push(top);
                    }
                }
            }
        }

        public BSTIteratorV1(TreeNode root, bool test)
        {
            this.inorder = new LinkedList<int>();
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
                        this.inorder.AddLast(top.val);
                    }
                }
            }
        }

        public int Next()
        {
            if (this.initialized == false)
            {
                this.enumerator = this.inorder.GetEnumerator();
                this.enumerator.MoveNext();
                this.initialized = true;
            }

            int ret = this.enumerator.Current;
            this.hasNext = this.enumerator.MoveNext();
            return ret;
        }

        public bool HasNext()
        {
            if (this.initialized == false)
            {
                this.enumerator = this.inorder.GetEnumerator();
                this.hasNext = this.enumerator.MoveNext();
                this.initialized = true;
            }

            return this.hasNext;
        }
    }
}

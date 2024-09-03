using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class Node
    {
        public int val;
        public Node left;
        public Node right;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
            left = null;
            right = null;
        }

        public Node(int _val, Node _left, Node _right)
        {
            val = _val;
            left = _left;
            right = _right;
        }
    }

    public class _426ConvertBinarySearchTreetoSortedDoublyLinkedList
    {
        public Node TreeToDoublyList_Morris(Node root)
        {
            Node preHead = new Node(), prev = preHead;
            if (root != null)
            {
                var node = root;
                while (node != null)
                {
                    var left = node.left;
                    if (left != null)
                    {
                        var p = left;
                        while (p.right != null && p.right != node)
                            p = p.right;

                        if (p.right == null)
                        {
                            // first time meet p
                            p.right = node; // connect p and node
                            node = node.left;
                        }
                        else
                        { // p.right == node
                          // second time meet p
                            p.right = null; // disconnect p and node

                            // output node
                            prev.right = node;
                            node.left = prev;
                            prev = node;

                            // go to right branch
                            node = node.right;
                        }
                    }
                    else
                    {
                        // output node
                        prev.right = node;
                        node.left = prev;
                        prev = node;

                        // go to right branch
                        node = node.right;
                    }
                }

                // prev is the last node in inorder traverse
                prev.right = preHead.right;
                preHead.right.left = prev;
            }

            return preHead.right;
        }

        public Node TreeToDoublyListStackDFS(Node root)
        {
            Node preHead = new Node(), prev = preHead;
            if (root != null)
            {
                Stack<Node> stack = new Stack<Node>();
                stack.Push(root);
                while (stack.Count != 0)
                {
                    var node = stack.Pop();
                    if (node.right != null)
                    {
                        stack.Push(node.right);
                    }
                    if (node.left != null)
                    {
                        var left = node.left;
                        node.left = node.right = null;
                        stack.Push(node);
                        stack.Push(left);
                    }
                    else
                    {
                        // output node in inorder
                        prev.right = node;
                        node.left = prev;

                        prev = node;
                    }
                }

                // prev is now the last node in the tree
                prev.right = preHead.right;
                preHead.right.left = prev;
            }

            return preHead.right;
        }
    }
}

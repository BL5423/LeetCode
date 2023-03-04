using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph1
{
    public class _116PopulatingNextRightPointersinEachNode
    {
        public Node Connect(Node root)
        {
            if (root != null)
            {
                // morris traverse
                // first pass, link left to right and right to parent
                var node = root;
                while (node != null)
                {
                    var prev = node.left;
                    if (prev != null)
                    {
                        while (prev.right != null && prev.right != node)
                        {
                            prev = prev.right;
                        }

                        if (prev.right == null)
                        {
                            // link prev and node
                            prev.right = node;
                            // process next node
                            node = node.left;
                        }
                        else
                        {
                            if (node.left != null)
                                // link left to right
                                node.left.next = node.right;

                            if (node.right != null)
                                // link right to parent
                                node.right.next = node;

                            // delink prev and node
                            prev.right = null;
                            // move to right branch
                            node = node.right;
                        }
                    }
                    else
                    {
                        // move to right branch
                        node = node.right;
                    }
                }

                // second pass, link right to the left of its parent's sibling
                node = root;
                while (node != null)
                {
                    var prev = node.left;
                    if (prev != null)
                    {
                        while (prev.right != null && prev.right != node)
                        {
                            prev = prev.right;
                        }

                        if (prev.right == null)
                        {
                            prev.right = node;
                            node = node.left;
                        }
                        else
                        {
                            // if node.next points to its parent
                            if (node.next != null && node.next.right == node)
                            {
                                // link prev.next to its parent's sibling's left
                                var sibling = node.next.next;
                                node.next = sibling != null ? sibling.left : null;
                            }

                            prev.right = null;
                            node = node.right;
                        }
                    }
                    else
                    {
                        // if node.next points to its parent
                        if (node.next != null && node.next.right == node)
                        {
                            // link prev.next to its parent's sibling's left
                            var sibling = node.next.next;
                            node.next = sibling != null ? sibling.left : null;
                        }

                        node = node.right;
                    }
                }
            }

            return root;
        }
    }

    public class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node next;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, Node _left, Node _right, Node _next)
        {
            val = _val;
            left = _left;
            right = _right;
            next = _next;
        }
    }
}

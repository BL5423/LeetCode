using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp117
{
    // Definition for a Node.
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

    public class _117PopulateNextRightPointersInEachNode
    {
        public Node Connect(Node root)
        {
            if (root != null)
            {
                Stack<Node> stack = new Stack<Node>();
                stack.Push(root);
                while (stack.Count != 0)
                {
                    var node = stack.Pop();

                    var child = node.left;
                    if (node.left != null)
                        child.next = node.right;
                    if (node.right != null)
                        child = node.right;

                    if (child != null)
                    {
                        var next = node.next;
                        while (next != null)
                        {
                            if (next.left != null)
                            {
                                child.next = next.left;
                                break;
                            }
                            if (next.right != null)
                            {
                                child.next = next.right;
                                break;
                            }
                            next = next.next;
                        }
                    }

                    if (node.right != null)
                        stack.Push(node.right);
                    if (node.left != null)
                        stack.Push(node.left);
                }
            }

            return root;
        }

        public Node ConnectV3(Node root)
        {
            Node leftMost = root;
            while (leftMost != null)
            {
                Node headOfNextLevel = null, tailOfNextLevel = null;

                // build the linked list, which is tracked by head and tail
                var headOfCurLevel = leftMost;
                while (headOfCurLevel != null)
                {
                    if (headOfCurLevel.left != null)
                    {
                        if (headOfNextLevel == null)
                            headOfNextLevel = headOfCurLevel.left;
                        if (tailOfNextLevel == null)
                            tailOfNextLevel = headOfCurLevel.left;
                        else
                        {
                            tailOfNextLevel.next = headOfCurLevel.left;
                            tailOfNextLevel = headOfCurLevel.left;
                        }
                    }
                    if (headOfCurLevel.right != null)
                    {
                        if (headOfNextLevel == null)
                            headOfNextLevel = headOfCurLevel.right;
                        if (tailOfNextLevel == null)
                            tailOfNextLevel = headOfCurLevel.right;
                        else
                        {
                            tailOfNextLevel.next = headOfCurLevel.right;
                            tailOfNextLevel = headOfCurLevel.right;
                        }
                    }

                    headOfCurLevel = headOfCurLevel.next;
                }

                leftMost = headOfNextLevel;
            }

            return root;
        }

        public Node ConnectV2(Node root)
        {
            var leftMost = root;
            while (leftMost != null)
            {
                var head = leftMost;
                while (head != null)
                {
                    if (head.left != null)
                    {
                        if (head.right != null)
                            head.left.next = head.right;
                        else 
                        {
                            var sibling = head.next;
                            while (sibling != null && sibling.left == null && sibling.right == null)
                            {
                                sibling = sibling.next;
                            }

                            if (sibling != null)
                                head.left.next = sibling.left != null ? sibling.left : sibling.right;
                        }
                    }
                    if (head.right != null)
                    {
                        var sibling = head.next;
                        while (sibling != null && sibling.left == null && sibling.right == null)
                        {
                            sibling = sibling.next;
                        }

                        if (sibling != null)
                            head.right.next = sibling.left != null ? sibling.left : sibling.right;
                    }

                    head = head.next;
                }

                while (leftMost != null && leftMost.left == null && leftMost.right == null)
                    leftMost = leftMost.next;

                if (leftMost != null)
                    leftMost = leftMost.left != null ? leftMost.left : leftMost.right;
            }

            return root;
        }

        public Node ConnectV1(Node root)
        {
            // Set next of every sub node to its parent
            // Traverse(root);

            // Reset next of every sub node to its sibling
            SetNext(root);
            return root;
        }

        private bool IsNextPointToParent(Node node)
        {
            if (node.next != null && (node.next.left == node || node.next.right == node))
                return true;

            return false;
        }

        private void SetNext(Node node)
        {
            if (node != null)
            {
                if (node.left != null)
                    node.left.next = node.right != null ? node.right : FindNextFromParent(node);
                if (node.right != null)
                    node.right.next = FindNextFromParent(node);

                //SetNextInSameLevel(node);
                SetNext(node.right);
                SetNext(node.left);
            }
        }

        private Node FindNextFromParent(Node node)
        {
            node = node.next;
            while (node != null)
            {
                if (node.left != null)
                    return node.left;
                if (node.right != null)
                    return node.right;

                node = node.next;
            }

            return null;
        }

        private void SetNextInSameLevel(Node node)
        {
            while (node != null)
            {
                if (IsNextPointToParent(node))
                {
                    var parent = node.next;
                    var nextParentNode = parent.next;
                    node.next = null;
                    while (nextParentNode != null)
                    {
                        if (nextParentNode.left != null || nextParentNode.right != null)
                            break;
                        nextParentNode = nextParentNode.next;
                    }

                    if (nextParentNode != null)
                    {
                        node.next = nextParentNode.left != null ? nextParentNode.left : nextParentNode.right;
                    }
                }

                node = node.next;
            }
        }

        private void Traverse(Node root)
        {
            if (root != null)
            {
                if (root.left != null)
                    root.left.next = root.right != null ? root.right : root;
                if (root.right != null)
                    root.right.next = root;

                Traverse(root.left);
                Traverse(root.right);
            }
        }
    }
}

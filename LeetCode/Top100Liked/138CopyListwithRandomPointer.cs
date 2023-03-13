using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _138CopyListwithRandomPointer
    {
        public Node CopyRandomList(Node head)
        {
            Node newHead = null;
            if (head != null)
            {
                var node = head;
                while (node != null)
                {
                    var next = node.next;
                    var copiedNode = new Node(node.val);
                    node.next = copiedNode;
                    copiedNode.next = next;

                    node = next;
                }

                node = head;
                while (node != null)
                {
                    if (node.random != null)
                        node.next.random = node.random.next;

                    node = node.next.next;
                }

                newHead = head.next;
                node = head;
                while (node != null)
                {
                    var next = node.next.next;                    
                    node.next.next = next != null ? next.next : null;
                    node.next = next;

                    node = next;
                }
            }

            return newHead;
        }

        public Node CopyRandomListV2(Node head)
        {
            Node newHead = null;
            if (head != null)
            {
                var node2Node = new Dictionary<Node, Node>();
                Node node = head, priorNode = null;
                while (node != null)
                {
                    var newNode = new Node(node.val);
                    node2Node.Add(node, newNode);
                    if (priorNode != null)
                        priorNode.next = newNode;

                    priorNode = newNode;
                    node = node.next;
                }

                newHead = node2Node[head];

                node = head;
                Node copiedNode = newHead;
                while (node != null)
                {
                    if (node.random != null)
                    {
                        copiedNode.random = node2Node[node.random];
                    }

                    node = node.next;
                    copiedNode = copiedNode.next;
                }
            }

            return newHead;
        }

        public Node CopyRandomListV1(Node head)
        {
            Node newHead = null;
            if (head != null)
            {
                var node2Index = new Dictionary<Node, int>();
                var index2Node = new Dictionary<int, Node>();
                Node node = head, priorNode = null;
                int index = 0;
                while (node != null)
                {
                    var newNode = new Node(node.val);
                    node2Index.Add(node, index);
                    index2Node.Add(index, newNode);
                    if (priorNode != null)
                        priorNode.next = newNode;

                    priorNode = newNode;
                    node = node.next;
                    ++index;
                }

                newHead = index2Node[0];

                node = head;
                Node copiedNode = newHead;
                while (node != null)
                {
                    if (node.random != null)
                    {
                        var randomNodeIndex = node2Index[node.random];
                        var copiedRandomNode = index2Node[randomNodeIndex];

                        copiedNode.random = copiedRandomNode;
                    }

                    node = node.next;
                    copiedNode = copiedNode.next;
                }
            }

            return newHead;
        }
    }

    // Definition for a Node.
    public class Node
    {
        public int val;
        public Node next;
        public Node random;

        public Node(int _val)
        {
            val = _val;
            next = null;
            random = null;
        }
    }
}

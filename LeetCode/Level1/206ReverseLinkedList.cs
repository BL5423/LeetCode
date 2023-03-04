using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class _206ReverseLinkedList
    {
        public ListNode ReverseListV1(ListNode head)
        {
            ListNode prev = null, node = head;
            while (node != null)
            {
                ListNode next = node.next;
                node.next = prev;
                prev = node;
                node = next;
            }

            return prev;
        }

        public ListNode ReverseList(ListNode head)
        {
            return ReverseListRecursively(head, null);
        }

        private ListNode ReverseListRecursively(ListNode node, ListNode prev)
        {
            if (node == null)
                return prev;

            if (node.next != null)
            {
                var nextNext = node.next.next;
                var next = node.next;
                next.next = node;
                node.next = prev;

                return ReverseListRecursively(nextNext, next);
            }
            else
            {
                node.next = prev;
                return node;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _25ReverseNodesink_Group
    {

        public ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode preHead = new ListNode(-1), newHead = null;
            int reversedNodes = k;
            while (head != null && (reversedNodes = Reverse(preHead, head, k, out ListNode next)) == k)
            {
                if (newHead == null)
                    newHead = preHead.next;

                preHead = head;
                head = next;
            }

            if (reversedNodes != k)
            {
                head = preHead.next;
                preHead.next = null;
                Reverse(preHead, head, k, out ListNode next);
            }

            return newHead;
        }

        private static int Reverse(ListNode preHead, ListNode firstNode, int k, out ListNode next)
        {
            next = null;
            ListNode node = firstNode;
            int i = 0;
            for (; i < k && node != null; ++i)
            {
                next = node.next;

                node.next = preHead.next;
                preHead.next = node;

                node = next;
            }

            return i;
        }

        public ListNode ReverseKGroupV1(ListNode head, int k)
        {
            if (head == null || k == 1)
                return head;

            ListNode tail = null, newHead = null, prevTail = null;
            do
            {
                var nextHead = this.ReverseK(head, k, out tail);
                if (newHead == null)
                    newHead = nextHead;
                if (prevTail != null)
                {
                    prevTail.next = nextHead;
                }
                 
                prevTail = head;
                head = tail;
            } while (head != null);

            return newHead;
        }

        private ListNode ReverseK(ListNode head, int k, out ListNode tail)
        {
            ListNode prev = head, next = head.next;
            head.next = null;
            while (--k > 0 && prev != null && next != null)
            {
                var nextNext = next.next;

                next.next = prev;
                prev = next;
                next = nextNext;
            }

            if (k != 0)
            {
                // not enough nodes, undo the change above
                next = prev.next;
                prev.next = null;
                while (prev != head)
                {
                    var nextNext = next.next;

                    next.next = prev;
                    prev = next;
                    next = nextNext;
                }

                tail = null;
                return head;
            }
            else
            {
                tail = next;
                return prev;
            }
        }
    }
}

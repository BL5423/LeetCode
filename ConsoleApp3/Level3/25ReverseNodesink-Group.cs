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

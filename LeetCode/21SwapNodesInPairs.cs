using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp21
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

    public class _21SwapNodesInPairs
    {
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null)
                return null;
            if (head.next == null)
                return head;

            ListNode newHead = head.next;
            ListNode pre = null;
            ListNode cur = head;
            ListNode next = cur.next;
            while (cur != null && next != null)
            {
                ListNode nextNext = next.next;
                next.next = cur;
                cur.next = nextNext;
                if (pre != null)
                {
                    pre.next = next;
                }

                pre = cur;
                cur = nextNext;
                if (cur != null)
                {
                    next = cur.next;
                }
            }

            return newHead;
        }
    }
}

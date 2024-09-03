using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
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

    public class _143ReorderList
    {
        public void ReorderList(ListNode head)
        {
            // find the pivot
            ListNode slow = head, fast = slow.next;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            if (slow.next != null)
            {
                var secondHalf = slow.next;
                slow.next = null;

                // reverse the second half
                ListNode prev = null;
                while (secondHalf != null)
                {
                    var next = secondHalf.next;
                    secondHalf.next = prev;
                    prev = secondHalf;

                    secondHalf = next;
                }

                // merge
                ListNode node1 = head, node2 = prev, preHead = new ListNode();
                prev = preHead;
                while (node1 != null)
                {
                    var next1 = node1.next;
                    var next2 = node2?.next;

                    prev.next = node1;
                    node1.next = node2;
                    prev = node2;

                    node1 = next1;
                    node2 = next2;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _876MiddleoftheLinkedList
    {
        public ListNode MiddleNode(ListNode head)
        {
            var preHead = new ListNode();
            preHead.next = head;
            var p1 = preHead.next;
            var p2 = preHead.next?.next;
            while (p1 != null && p2 != null)
            {
                p1 = p1.next;
                p2 = p2.next?.next;
            }

            return p1;
        }
    }
}

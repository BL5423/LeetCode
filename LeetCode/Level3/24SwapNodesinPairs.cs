using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _24SwapNodesinPairs
    {
        public ListNode SwapPairs(ListNode head)
        {
            var preHead = new ListNode(0, head);
            var p = preHead;
            while (p.next != null && p.next.next != null)
            {
                var nextPnext = p.next.next.next;
                var pNextNext = p.next.next;

                p.next.next.next = p.next;
                p.next.next = nextPnext;
                p.next = pNextNext;

                p = p.next.next;
            }

            return preHead.next;
        }
    }
}

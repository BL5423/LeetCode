using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _141LinkedListCycle
    {
        public bool HasCycle(ListNode head)
        {
            bool cycle = false;
            if (head != null)
            {
                var fast = head;
                var slow = head;
                while (fast != null && slow != null)
                {
                    fast = fast.next != null ? fast.next.next : null;
                    slow = slow.next;
                    if (fast != null && fast == slow)
                    {
                        cycle = true;
                        break;
                    }
                }
            }

            return cycle;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
            next = null;
        }
    }
}

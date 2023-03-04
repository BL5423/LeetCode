using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _61RotateList
    {
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null)
                return null;

            int len = 0;
            ListNode node = head, tail = null;
            while (node != null)
            {
                ++len;
                tail = node;
                node = node.next;
            }

            if (k >= len)
                k = k % len;

            ListNode p = head;
            int forwards = len - k;
            while (--forwards > 0)
            {
                p = p.next;
            }

            tail.next = head;
            var res = p.next;
            p.next = null;

            return res;
        }
    }
}

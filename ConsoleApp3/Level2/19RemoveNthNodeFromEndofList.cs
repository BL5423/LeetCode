using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
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

    public class _19RemoveNthNodeFromEndofList
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            var preHead = new ListNode(-1, head);
            var faster = preHead;
            var slower = preHead;
            for(int i = 0; i < n; ++i)
            {
                faster = faster.next;
            }

            while (faster.next != null)
            {
                faster = faster.next;
                slower = slower.next;
            }

            slower.next = slower.next?.next;

            return preHead.next;
        }
    }
}

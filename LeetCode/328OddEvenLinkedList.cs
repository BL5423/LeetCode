using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp328
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

    public class _328OddEvenLinkedList
    {
        public ListNode OddEvenList(ListNode head)
        {
            ListNode oddHead = head;
            if (head != null)
            {
                ListNode evenHead = head?.next;
                ListNode oddTail = oddHead;
                ListNode evenTail = evenHead;
                while (oddTail.next != null && evenTail.next != null)
                {
                    oddTail.next = oddTail.next.next;
                    evenTail.next = evenTail.next.next;

                    oddTail = oddTail.next;
                    evenTail = evenTail.next;
                }

                if (evenHead != null)
                    oddTail.next = evenHead;
            }

            return oddHead;
        }
    }
}

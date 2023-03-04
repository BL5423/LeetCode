using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _234PalindromeLinkedList
    {
        public bool IsPalindrome(ListNode head)
        {
            // get middle
            var dummyHead = new ListNode(-1, head);
            var faster = dummyHead;
            var slower = dummyHead;
            while (faster != null && faster.next != null)
            {
                slower = slower.next;
                faster = faster.next.next;
            }

            var head1 = head;
            var head2 = slower.next;

            // reverse head2
            var revHead2 = Reverse(head2);
            head2 = revHead2;

            // compare the two lists with heads of head1 and prev
            bool isPalindrome = true;
            while (isPalindrome && head1 != null && revHead2 != null)
            {
                if (head1.val != revHead2.val)
                {
                    isPalindrome = false;
                    break;
                }

                head1 = head1.next;
                revHead2 = revHead2.next;
            }

            // restore the second list
            Reverse(head2);

            return isPalindrome;
        }

        public bool IsPalindromeV1(ListNode head)
        {
            // get length
            int length = 0;
            var p = head;
            while (p != null)
            {
                ++length;
                p = p.next;
            }

            // split
            var head1 = head;
            var head2 = head;
            for(int i = 0; i < length / 2; ++i)
            {
                head2 = head2.next;
            }

            if ((length & 1) == 1)
            {
                // skip the node right in middle if the length is odd
                head2 = head2.next;
            }

            // reverse head2
            var revHead2 = Reverse(head2);
            head2 = revHead2;

            // compare the two lists with heads of head1 and prev
            bool isPalindrome = true;
            int count = 0;
            while (isPalindrome && count < length / 2)
            {
                if (head1.val != revHead2.val)
                {
                    isPalindrome = false;
                    break;
                }

                ++count;
                head1 = head1.next;
                revHead2 = revHead2.next;
            }

            // restore the second list
            Reverse(head2);

            return isPalindrome;
        }

        private ListNode Reverse(ListNode head)
        {
            ListNode prev = null;
            var node = head;
            while (node != null)
            {
                var next = node.next;
                node.next = prev;
                prev = node;
                node = next;
            }

            return prev;
        }
    }
}

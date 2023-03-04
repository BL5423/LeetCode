using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Top100Liked
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

    public class _2AddTwoNumbers
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int carry = 0;
            ListNode first = null, preHead = null;
            first = preHead = new ListNode();
            while ((carry != 0 && (l1 != null || l2 != null)) ||
                   (carry == 0 && l1 != null && l2 != null))
            {
                int sum = carry 
                    + (l1 != null ? l1.val : 0)
                    + (l2 != null ? l2.val : 0);
                carry = 0;
                if (sum >= 10)
                {
                    sum -= 10;
                    carry = 1;
                }
                l1 = l1?.next;
                l2 = l2?.next;

                var node = new ListNode(sum);
                preHead.next = node;
                preHead = node;
            }

            if (carry != 0)
            {
                var node = new ListNode(carry);
                preHead.next = node;
                preHead = node;
            }
            else if (l1 != null)
            {
                preHead.next = l1;
            }
            else if (l2 != null)
            {
                preHead.next = l2;
            }

            return first.next;
        }

        public ListNode AddTwoNumbersV1(ListNode l1, ListNode l2)
        {
            ListNode lSum = l1;
            ListNode node = lSum;
            int carry = 0;
            while (l1 != null || l2 != null)
            {
                node.val = (l1 != null ? l1.val : 0) + (l2 != null ? l2.val : 0) + carry;
                carry = 0;
                if (node.val >= 10)
                {
                    carry = 1;
                    node.val -= 10;
                }

                l1 = l1?.next;
                l2 = l2?.next;
                if (l1 != null)
                {
                    node.next = l1;
                    node = l1;
                }
                else if (l2 != null)
                {
                    node.next = l2;
                    node = l2;
                }
            }

            if (carry != 0)
            {
                node.next = new ListNode(carry);
            }

            return lSum;
        }
    }
}

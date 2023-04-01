using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Liked
{
    public class _160IntersectionofTwoLinkedLists
    {
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            ListNode intersect = null;
            if (headA != null && headB != null)
            {
                var tailA = headA;
                while (tailA.next != null)
                {
                    tailA = tailA.next;
                }

                // create a cycle in list A
                tailA.next = headA;

                var node = headB;
                while (node != null)
                {
                    if (node == headA)
                    {
                        // there must be an intersect
                        break;
                    }

                    node = node.next;
                }

                if (node != null)
                {
                    var fast = headB;
                    var slow = headB;
                    do
                    {
                        fast = fast.next.next;
                        slow = slow.next;
                    }
                    while (fast != slow);

                    node = headB;
                    while (node != slow)
                    {
                        node = node.next;
                        slow = slow.next;
                    }

                    intersect = node;
                }

                // restore list A
                tailA.next = null;
            }

            return intersect;
        }

        public ListNode GetIntersectionNodeV1(ListNode headA, ListNode headB)
        {
            ListNode intersect = null;
            if (headA == null && headB == null)
            {
                var node = headA;
                while (node != null) 
                {
                    node.val *= -1;
                    node = node.next;
                }

                node = headB;
                while (node != null)
                {
                    if (node.val < 0)
                    {
                        intersect = node;
                        break;
                    }

                    node = node.next;
                }

                if (intersect != null)
                {
                    node = headA;
                    while (node != null)
                    {
                        node.val *= -1;
                        node = node.next;
                    }
                }
            }

            return intersect;
        }
    }
}

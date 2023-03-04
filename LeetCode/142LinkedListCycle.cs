using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp146
{
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

    public class _142LinkedListCycle
    {
        public ListNode DetectCycle(ListNode head)
        {
            ListNode node = null;
            if (head != null)
            {
                int steps1 = 0;
                int steps2 = 0;

                ++steps1;
                ListNode node1 = head.next;
                steps2 += 2;
                ListNode node2 = head.next?.next;
                while (node1 != node2)
                {
                    ++steps1;
                    node1 = node1?.next;
                    steps2 += 2;
                    node2 = node2?.next?.next;
                }

                if (node1 != null)
                {
                    // cycle detected and steps2 is twice of steps1
                    node = head;
                    while (node != node1)
                    {
                        node = node.next;
                        node1 = node1.next;
                    }
                }
            }

            return node;
        }

        public ListNode DetectCycleV2(ListNode head)
        {
            var p1 = head;
            var p2 = head;
            ListNode p = null;
            while (p1 != null && p2 != null)
            {
                p1 = p1.next;
                p2 = p2.next?.next;
                if (p1 == p2)
                {
                    // where p2 catches up p1
                    p = p1;
                    break;
                }
            }

            if (p == null)
            {
                // no cycle
                return null;
            }

            p1 = head;
            p2 = p;
            while (p1 != p2)
            {
                p1 = p1.next;
                p2 = p2.next;
            }

            return p1;
        }
    }
}

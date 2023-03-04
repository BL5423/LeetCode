using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _148SortList
    {
        public ListNode SortList(ListNode head)
        {
            var dummy = new ListNode(-1);
            dummy.next = head;
            int n = 1;
            while (dummy.next != null)
            {
                int merges = 0;
                ListNode p1 = dummy.next, p2 = null;
                var lastTail = dummy;
                do
                {
                    // split list from p1 to two sublists with length of n
                    p2 = Split(p1, n, out ListNode newHead);

                    // can't split a new sublist from p1
                    if (p2 == null)
                    {
                        lastTail.next = p1;
                        break;
                    }

                    // merge the splited two sublists and append the head to the last tail to make sure we don't lose track any node
                    lastTail.next = Merge(p1, p2, out ListNode nextTail);
                    ++merges;

                    // set p1 as the head of nodes left
                    p1 = newHead;
                    // update the last tail to the last node of the merged list
                    lastTail = nextTail;

                    // no more to split with
                    if (p1 == null)
                    {
                        break;
                    }
                } while (true);

                if (merges == 0)
                {
                    break;
                }

                //if (merges == 1)
                //{
                //    if (p1 != null)
                //    {
                //        lastTail.next = null;
                //        dummy.next = Merge(p1, dummy.next, out ListNode nextTail);
                //    }

                //    break;
                //}

                n *= 2;
            }

            return dummy.next;
        }

        private ListNode Split(ListNode start, int n, out ListNode newHead)
        {
            var p1 = start;

            // move forward n steps to get p2;
            var p2 = p1;
            var prev = p2;
            for (int i = 0; i < n && p2 != null; ++i)
            {
                prev = p2;
                p2 = p2.next;
            }

            prev.next = null;

            // move forward n steps from p2 to get newHead
            newHead = null;
            if (p2 != null)
            {
                newHead = p2;
                prev = newHead;
                for (int i = 0; i < n && newHead != null; ++i)
                {
                    prev = newHead;
                    newHead = newHead.next;
                }

                prev.next = null;
            }

            return p2;
        }

        private ListNode Merge(ListNode p1, ListNode p2, out ListNode nextTail)
        {
            var dummy = new ListNode(-1);
            var tail = dummy;
            while (p1 != null && p2 != null)
            {
                if (p1.val < p2.val)
                {
                    tail.next = p1;
                    p1 = p1.next;
                }
                else
                {
                    tail.next = p2;
                    p2 = p2.next;
                }

                tail = tail.next;
            }

            if (p1 != null)
            {
                tail.next = p1;
            }
            else if (p2 != null)
            {
                tail.next = p2;
            }

            // forward dummy to the end of the merged list
            while (tail.next != null)
            {
                tail = tail.next;
            }

            nextTail = tail;
            return dummy.next;
        }

        public ListNode SortListV1(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            // spilt
            var faster = head.next?.next;
            var slower = head;
            
            while (faster != null && faster.next != null)
            {
                faster = faster.next.next;
                slower = slower.next;
            }

            var head1 = head;
            var head2 = slower.next;
            slower.next = null;
            head1 = SortListV1(head1);
            head2 = SortListV1(head2);

            // merge
            var dummy = new ListNode(-1);
            var tail = dummy;
            while (head1 != null && head2 != null)
            {
                if (head1.val < head2.val)
                {
                    tail.next = head1;
                    head1 = head1.next;
                }
                else
                {
                    tail.next = head2;
                    head2 = head2.next;
                }

                tail = tail.next;
            }

            if (head1 != null)
            {
                tail.next = head1;
            }
            else if (head2 != null)
            {
                tail.next = head2;
            }

            return dummy.next;
        }
    }
}

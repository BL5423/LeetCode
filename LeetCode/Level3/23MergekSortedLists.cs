using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _23MergekSortedLists
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists.Length <= 0)
                return null;

            //return this.MergeRecursively(lists, 0, lists.Length - 1);
            return this.MergeIterativelyV2(lists);
        }

        private ListNode MergeRecursively(ListNode[] lists, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
                return lists[startIndex];

            int middle = startIndex + ((endIndex - startIndex) >> 1);
            var left = this.MergeRecursively(lists, startIndex, middle);
            var right = this.MergeRecursively(lists, middle + 1, endIndex);

            var preHead = new ListNode(0);
            var tail = preHead;
            while (left != null && right != null)
            {
                if (left.val <= right.val)
                {
                    tail.next = left;
                    left = left.next;
                }
                else
                {
                    tail.next = right;
                    right = right.next;
                }

                tail = tail.next;
            }

            if (left != null)
                tail.next = left;
            else if (right != null)
                tail.next = right;

            return preHead.next;
        }

        private ListNode MergeIterativelyV1(ListNode[] lists)
        {
            LinkedList<ListNode> mergingList = new LinkedList<ListNode>(lists);
            while (mergingList.Count > 1)
            {
                var mergedList = new LinkedList<ListNode>();
                while (mergingList.Count > 1)
                {
                    var list1 = mergingList.First();
                    mergingList.RemoveFirst();
                    var list2 = mergingList.First();
                    mergingList.RemoveFirst();

                    var preHead = new ListNode(0);
                    var tail = preHead;
                    while (list1 != null && list2 != null)
                    {
                        if (list1.val <= list2.val)
                        {
                            tail.next = list1;
                            list1 = list1.next;
                        }
                        else
                        {
                            tail.next = list2;
                            list2 = list2.next;
                        }

                        tail = tail.next;
                    }

                    if (list1 != null)
                        tail.next = list1;
                    else if (list2 != null)
                        tail.next = list2;

                    mergedList.AddLast(preHead.next);
                }

                if (mergingList.Count > 0)
                    mergedList.AddLast(mergingList.First());

                mergingList = mergedList;
            }

            return mergingList.First();
        }

        private ListNode MergeIterativelyV2(ListNode[] lists)
        {
            int interval = 1;
            while (interval < lists.Length)
            {
                for (int index = 0; index <= lists.Length - 1 - interval; index += interval * 2)
                {
                    lists[index] = this.MergeTwoList(lists[index], lists[index + interval]);
                }
                interval *= 2;
            }

            return lists[0];
        }

        private ListNode MergeTwoList(ListNode list1, ListNode list2)
        {
            var preHead = new ListNode(0);
            var tail = preHead;
            while (list1 != null && list2 != null)
            {
                if (list1.val <= list2.val)
                {
                    tail.next = list1;
                    list1 = list1.next;
                }
                else
                {
                    tail.next = list2;
                    list2 = list2.next;
                }

                tail = tail.next;
            }

            if (list1 != null)
                tail.next = list1;
            else if (list2 != null)
                tail.next = list2;

            return preHead.next;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
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

    public class MergeSortedLists
    {
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            ListNode preHead = new ListNode();
            ListNode tail = preHead;
            while(list1 != null && list2 != null)
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
            {
                tail.next = list1;
            }
            else if (list2 != null)
            {
                tail.next = list2;
            }

            return preHead.next;
        }
    }
}

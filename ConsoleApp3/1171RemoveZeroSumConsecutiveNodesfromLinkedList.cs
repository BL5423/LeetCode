using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1171
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

    public class _1171RemoveZeroSumConsecutiveNodesfromLinkedList
    {
        public ListNode RemoveZeroSumSublistsV2(ListNode head)
        {
            Dictionary<int, ListNode> sumToFarestNode = new Dictionary<int, ListNode>();
            var preHead = new ListNode(0);
            preHead.next = head;
            var node = preHead;
            int sum = 0;
            while(node != null)
            {
                sum += node.val;
                sumToFarestNode[sum] = node;
                node = node.next;
            }

            sum = 0;
            for(node = preHead; node != null; node = node.next)
            {
                sum += node.val;
                node.next = sumToFarestNode[sum].next;
            }

            return preHead.next;
        }

        public ListNode RemoveZeroSumSublists(ListNode head)
        {
            Dictionary<int, ListNode> map = new Dictionary<int, ListNode>();
            ListNode node = head, newHead = head;
            int sum = 0;
            bool removed = true;
            while(newHead != null && removed)
            {
                if (node == null)
                {
                    node = newHead;
                    sum = 0;
                    map.Clear();
                }

                removed = false;
                while (node != null)
                {
                    sum += node.val;
                    if (sum == 0)
                    {
                        // drop everything before and including node
                        newHead = node.next;
                        map.Clear();
                        removed = true;
                    }
                    else if (map.TryGetValue(sum, out ListNode prev))
                    {
                        // remove nodes from map
                        int tempSum = sum;
                        var next = prev.next;
                        while (next != node)
                        {
                            tempSum += next.val;
                            if (map.TryGetValue(tempSum, out ListNode toRemove) && toRemove == next)
                            {
                                map.Remove(tempSum);
                            }

                            next = next.next;
                        }

                        // drop everthing between prev and node
                        prev.next = node.next;
                        removed = true;
                    }
                    else
                    {
                        map[sum] = node;
                    }

                    node = node.next;
                }
            }

            return newHead;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _146LRUCache
    {
        public class LRUCache
        {
            private Dictionary<int, DoubleLinkedNode> val2LinkedNode;

            private Dictionary<int, int> cache;

            private DoubleLinkedNode preHead, afterTail;

            private int capacityCap;

            public LRUCache(int capacity)
            {
                this.preHead = new DoubleLinkedNode(-1);
                this.afterTail = new DoubleLinkedNode(-2);
                this.preHead.next = this.afterTail;
                this.afterTail.prev = preHead;

                this.capacityCap = capacity;
                this.val2LinkedNode = new Dictionary<int, DoubleLinkedNode>(capacity);
                this.cache = new Dictionary<int, int>(capacity);
            }

            public int Get(int key)
            {
                if (this.cache.TryGetValue(key, out int val))
                {
                    // promote the node to the tail of the list
                    var node = this.val2LinkedNode[key];

                    // remove node from cur pos
                    this.Delink(node);

                    // then move the node to tail
                    this.PromoteToTail(node);

                    return val;
                }

                return -1;
            }

            public void Put(int key, int value)
            {
                if (this.cache.TryGetValue(key, out int val))
                {
                    this.cache[key] = value;

                    var node = this.val2LinkedNode[key];

                    // remove node from cur pos
                    this.Delink(node);

                    // then move the node to tail
                    this.PromoteToTail(node);
                }
                else
                {
                    if (this.capacityCap == this.cache.Count)
                    {
                        // remove the first node(least recently used) from the list
                        var evictedNode = this.preHead.next;
                        this.Delink(evictedNode);

                        this.val2LinkedNode.Remove(evictedNode.key);
                        this.cache.Remove(evictedNode.key);
                    }

                    this.cache.Add(key, value);

                    // link the node to the tail of the list
                    var node = new DoubleLinkedNode(key);
                    this.PromoteToTail(node);

                    this.val2LinkedNode.Add(node.key, node);
                }
            }

            private void PromoteToTail(DoubleLinkedNode node)
            {
                node.next = this.afterTail;
                var prevTail = this.afterTail.prev;
                this.afterTail.prev = node;
                node.prev = prevTail;
                prevTail.next = node;
            }

            private void Delink(DoubleLinkedNode node)
            {
                var curPrev = node.prev;
                var curNext = node.next;
                curPrev.next = curNext;
                curNext.prev = curPrev;
            }
        }

        public class DoubleLinkedNode
        {
            public DoubleLinkedNode prev, next;

            public int key;

            public DoubleLinkedNode(int key)
            {
                this.key = key;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _460LFUCache
    {
        public class LFUCache
        {
            private Dictionary<int, int> cache;

            private int capacityCap;

            private Dictionary<int, DoubleLinkedList> usageCounters;

            private Dictionary<int, DoubleLinkedNode> key2LinkedNode;

            private DoubleLinkedList leastUsedKeys;

            public LFUCache(int capacity)
            {
                this.capacityCap = capacity;
                this.cache = new Dictionary<int, int>(capacity);
                this.usageCounters = new Dictionary<int, DoubleLinkedList>();
                this.key2LinkedNode = new Dictionary<int, DoubleLinkedNode>();
                this.leastUsedKeys = null;
            }

            public int Get(int key)
            {
                if (!this.cache.TryGetValue(key, out int value))
                {
                    return -1;
                }

                this.IncreaseUsage(key);
                return value;
            }

            public void Put(int key, int value)
            {
                if (this.capacityCap == 0)
                    return;

                if (this.cache.TryGetValue(key, out int oldVal))
                {
                    this.IncreaseUsage(key);
                    this.cache[key] = value;
                }
                else
                {
                    if (this.cache.Count == this.capacityCap)
                    {
                        var nodeToRemove = this.leastUsedKeys.preHead.next;
                        this.leastUsedKeys.Remove(nodeToRemove);
                        if (this.leastUsedKeys.IsEmpty())
                            this.leastUsedKeys = null;

                        this.cache.Remove(nodeToRemove.key);
                        this.key2LinkedNode.Remove(key);
                    }

                    var node = new DoubleLinkedNode(key);
                    if (!this.usageCounters.TryGetValue(node.counter, out DoubleLinkedList list))
                    {
                        list = new DoubleLinkedList();
                        this.usageCounters.Add(node.counter, list);
                    }
                    list.Add(node);
                    
                    // least used keys to track the keys that are least least frequently
                    this.leastUsedKeys = list;
                    this.key2LinkedNode.Add(key, node);

                    this.cache.Add(key, value);
                }
            }

            private void IncreaseUsage(int key)
            {
                // increment the usage counter for key
                var node = this.key2LinkedNode[key];
                var list = this.usageCounters[node.counter];
                list.Remove(node);
                if (list.IsEmpty())
                {
                    this.usageCounters.Remove(node.counter);
                    if (list == this.leastUsedKeys)
                        this.leastUsedKeys = null;
                }

                if (!this.usageCounters.TryGetValue(++node.counter, out DoubleLinkedList nextList))
                {
                    nextList = new DoubleLinkedList();
                    this.usageCounters.Add(node.counter, nextList);
                }

                nextList.Add(node);
                if (this.leastUsedKeys == null)
                    this.leastUsedKeys = nextList;
            }
        }
    }

    public class DoubleLinkedList
    {
        public DoubleLinkedNode preHead, afterTail;

        public DoubleLinkedList()
        {
            this.preHead = new DoubleLinkedNode(-1);
            this.afterTail = new DoubleLinkedNode(-2);
            this.preHead.next = this.afterTail;
            this.afterTail.prev = preHead;
        }

        public void Add(DoubleLinkedNode node)
        {
            node.next = this.afterTail;
            var prevTail = this.afterTail.prev;
            this.afterTail.prev = node;
            node.prev = prevTail;
            prevTail.next = node;
        }

        public void Remove(DoubleLinkedNode node)
        {
            var curPrev = node.prev;
            var curNext = node.next;
            curPrev.next = curNext;
            curNext.prev = curPrev;
        }

        public bool IsEmpty()
        {
            return this.preHead.next == this.afterTail;
        }
    }

    public class DoubleLinkedNode
    {
        public DoubleLinkedNode prev, next;

        public int key, counter;

        public DoubleLinkedNode(int key)
        {
            this.key = key;
            this.counter = 1;
        }
    }
}

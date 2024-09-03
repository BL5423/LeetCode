using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class AllOne
    {
        private Dictionary<string, int> key2Count;

        private Dictionary<int, LinkedListNode<HashSet<string>>> count2Keys;

        private LinkedList<HashSet<string>> list;

        public AllOne()
        {
            this.key2Count = new Dictionary<string, int>();
            this.count2Keys = new Dictionary<int, LinkedListNode<HashSet<string>>>();
            this.list = new LinkedList<HashSet<string>>();
        }

        public void Inc(string key)
        {
            if (this.key2Count.TryGetValue(key, out int count))
            {
                ++this.key2Count[key];

                var group = this.count2Keys[count];
                group.Value.Remove(key);

                if (!this.count2Keys.TryGetValue(count + 1, out LinkedListNode<HashSet<string>> nextGroup))
                {
                    nextGroup = new LinkedListNode<HashSet<string>>(new HashSet<string>());
                    this.count2Keys.Add(count + 1, nextGroup);

                    // insert nextGroup to the list
                    this.list.AddAfter(this.count2Keys[count], nextGroup);
                }

                nextGroup.Value.Add(key);

                // if the previous keys set is empty, then remove it from the list
                if (group.Value.Count == 0)
                {
                    this.list.Remove(group);
                    this.count2Keys.Remove(count);
                }
            }
            else
            {
                this.key2Count.Add(key, 1);

                if (!this.count2Keys.TryGetValue(1, out LinkedListNode<HashSet<string>> group))
                {
                    group = new LinkedListNode<HashSet<string>>(new HashSet<string>());
                    this.count2Keys.Add(1, group);
                    this.list.AddFirst(group);
                }

                group.Value.Add(key);
            }
        }

        public void Dec(string key)
        {
            if (this.key2Count.TryGetValue(key, out int count))
            {
                count = --this.key2Count[key];
                var group = this.count2Keys[count + 1];
                group.Value.Remove(key);

                if (count != 0)
                {
                    if (!this.count2Keys.TryGetValue(count, out LinkedListNode<HashSet<string>> nextGroup))
                    {
                        nextGroup = new LinkedListNode<HashSet<string>>(new HashSet<string>());
                        this.count2Keys.Add(count, nextGroup);

                        // insert nextGroup to the list
                        this.list.AddBefore(group, nextGroup);
                    }

                    nextGroup.Value.Add(key);
                }
                else
                {
                    this.key2Count.Remove(key);
                }

                if (group.Value.Count == 0)
                {
                    this.list.Remove(group);
                    this.count2Keys.Remove(count + 1);
                }
            }
        }

        public string GetMaxKey()
        {
            if (this.list.Count != 0)
                return this.list.Last().First();

            return string.Empty;
        }

        public string GetMinKey()
        {
            if (this.list.Count != 0)
                return this.list.First().First();

            return string.Empty;
        }
    }

    public class Node
    {
        public Node prev, next;

        public string key;

        public int count;
    }

    public class AllOneV1
    {
        private Node head, tail;

        private Dictionary<string, Node> mapping;

        public AllOneV1()
        {
            this.head = new Node() { count = int.MinValue, key = string.Empty };
            this.tail = new Node() { count = int.MaxValue, key = string.Empty };
            this.head.next = this.tail;
            this.tail.prev = this.head;
            this.mapping = new Dictionary<string, Node>();
        }

        public void Inc(string key)
        {
            if (this.mapping.TryGetValue(key, out Node node))
            {
                ++node.count;
                while (node.count > node.next.count)
                {
                    var prev = node.prev;
                    var next = node.next;

                    prev.next = next;
                    next.prev = prev;

                    node.prev = next;
                    node.next = next.next;

                    next.next = node;
                    node.next.prev = node;
                }
            }
            else
            {
                node = new Node() { key = key, count = 1 };
                node.next = this.head.next;
                this.head.next.prev = node;
                this.head.next = node;
                node.prev = this.head;

                this.mapping.Add(key, node);
            }
        }

        public void Dec(string key)
        {
            if (this.mapping.TryGetValue(key, out Node node))
            {
                if (--node.count != 0)
                {
                    while (node.count < node.prev.count)
                    {
                        var prev = node.prev;
                        var next = node.next;

                        prev.next = next;
                        next.prev = prev;

                        node.next = prev;
                        node.prev = prev.prev;

                        node.prev.next = node;
                        prev.prev = node;
                    }
                }
                else
                {
                    this.mapping.Remove(key);
                    node.prev.next = node.next;
                    node.next.prev = node.prev;
                }
            }
        }

        public string GetMaxKey()
        {
            return this.tail.prev.key;
        }

        public string GetMinKey()
        {
            return this.head.next.key;
        }
    }

    /**
     * Your AllOne object will be instantiated and called as such:
     * AllOne obj = new AllOne();
     * obj.Inc(key);
     * obj.Dec(key);
     * string param_3 = obj.GetMaxKey();
     * string param_4 = obj.GetMinKey();
     */
}

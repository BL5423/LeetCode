using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class Node
    {
        public Node prev, next;

        public int key;
    }

    public class RandomizedSet
    {

        private Dictionary<int, Node> key2Node;

        private Dictionary<int, Node> index2Node;

        private Dictionary<Node, int> node2Index;

        private int lastIndex = -1;

        public RandomizedSet()
        {
            this.key2Node = new Dictionary<int, Node>();
            this.index2Node = new Dictionary<int, Node>();
            this.node2Index = new Dictionary<Node, int>();
        }

        public bool Insert(int key)
        {
            if (!key2Node.TryGetValue(key, out Node node))
            {
                node = new Node() { key = key };
                this.key2Node[key] = node;

                Node prevNode = null;
                if (this.lastIndex >= 0)
                {
                    prevNode = this.index2Node[this.lastIndex];
                    prevNode.next = node;
                }
                node.prev = prevNode;

                this.index2Node.Add(++this.lastIndex, node);
                this.node2Index.Add(node, this.lastIndex);
                return true;
            }

            return false;
        }

        public bool Remove(int key)
        {
            if (this.key2Node.Remove(key, out Node removedNode))
            {
                if (this.key2Node.Count != 0)
                {
                    // move the key from the end to the spot of the removed val
                    int indexOfRemovedNode = this.node2Index[removedNode];
                    if (indexOfRemovedNode != this.lastIndex)
                    {
                        Node lastNode = this.index2Node[this.lastIndex];
                        this.index2Node[indexOfRemovedNode] = lastNode;
                        this.node2Index[lastNode] = indexOfRemovedNode;
                    }

                    this.node2Index.Remove(removedNode);
                    this.index2Node.Remove(this.lastIndex);
                    --this.lastIndex;
                }
                else
                {
                    this.index2Node.Clear();
                    this.node2Index.Clear();

                    // nothing left
                    this.lastIndex = -1;
                }

                return true;
            }

            return false;
        }

        public int GetRandom()
        {
            Random rnd = new Random();
            var index = rnd.Next(0, this.lastIndex + 1);
            return this.index2Node[index].key;
        }
    }

    public class RandomizedSetV1
    {
        private const int SIZE = 200000 + 1;

        private Dictionary<int, int> dictionary;

        private int[] numbers = new int[SIZE];

        public RandomizedSetV1()
        {
            this.dictionary = new Dictionary<int, int>();
        }

        public bool Insert(int val)
        {
            if (!this.dictionary.TryGetValue(val, out int index))
            {
                index = this.dictionary.Count;
                this.dictionary.Add(val, index);
                this.numbers[index] = val;
                return true;
            }

            return false;
        }

        public bool Remove(int val)
        {
            if (this.dictionary.TryGetValue(val, out int index))
            {
                int count = this.dictionary.Count;
                if (count > 1)
                {
                    // use the last number to overwrite val, which is being deleted
                    this.numbers[index] = this.numbers[count - 1];
                    this.dictionary[this.numbers[count - 1]] = index;
                }

                this.dictionary.Remove(val);
                return true;
            }

            return false;
        }

        public int GetRandom()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int index = random.Next(0, this.dictionary.Count);
            return this.numbers[index];
        }
    }
}

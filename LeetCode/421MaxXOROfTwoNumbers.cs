using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _421MaxXOROfTwoNumbers
    {
        public int FindMaximumXOR(int[] nums)
        {
            BinaryTrie trie = new BinaryTrie();
            foreach(var num in nums)
            {
                trie.Insert(num);
            }

            int max = 0;
            foreach(var num in nums)
            {
                max = Math.Max(max, trie.FindMostDifferences(num) ^ num);
            }

            return max;
        }

        public int FindMaximumXORV2(int[] nums)
        {
            int mask = 0, max = 0;
            HashSet<int> prefixes = new HashSet<int>();
            for(int i = 30; i >= 0; --i)
            {
                mask |= (1 << i);
                int potentialMax = (max | (1 << i));

                foreach(int num in nums)
                {
                    int prefix = num & mask;
                    prefixes.Add(prefix);
                }

                foreach(var prefix in prefixes)
                {
                    int anotherPrefix = prefix ^ potentialMax;
                    if (prefixes.Contains(anotherPrefix))
                    {
                        // if anotherPrefix does exist in prefixes, it means potentialMax = prefix ^ anotherPrefix, which is the max with current mask
                        max = potentialMax;
                        break;
                    }
                }

                prefixes.Clear();
            }

            return max;
        }
    }

    public class BinaryTrie
    {
        const int T = 2;

        BinaryTrie[] kids;

        int? val;

        public BinaryTrie()
        {
            this.kids = new BinaryTrie[T];
            this.val = null;
        }

        public void Insert(int num)
        {
            BinaryTrie cur = this;
            for(int i = 30; i >= 0; --i)
            {
                int bit = (num >> i) & 1;
                if (cur.kids[bit] == null)
                    cur.kids[bit] = new BinaryTrie();
                cur = cur.kids[bit];
            }

            cur.val = num;
        }

        public int FindMostDifferences(int num)
        {
            BinaryTrie cur = this;
            for (int i = 30; i >= 0; --i)
            {
                int rbit = (num >> i) & 1 ^ 1;
                if (cur.kids[rbit] == null)
                    cur = cur.kids[rbit ^ 1];
                else
                    cur = cur.kids[rbit];
            }

            return cur.val.Value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level1
{
    public class _692TopKFrequentWords
    {
        // Another solution: counting sort + Tire(to sort by alphabets/lexicographical)

        public IList<string> TopKFrequent(string[] words, int k)
        {
            Dictionary<string, int> counts = new Dictionary<string, int>(words.Length);
            foreach(var word in words)
            {
                if (counts.TryGetValue(word, out int count))
                {
                    ++counts[word];
                }
                else
                {
                    counts.Add(word, 1);
                }
            }

            MinHeap<Word> minHeap = new MinHeap<Word>(k);
            foreach(var c in counts)
            {
                minHeap.Push(new Word() { word = c.Key, count = c.Value });
            }

            var res = new List<string>(k);
            while (minHeap.Size() > 0)
            {
                res.Add(minHeap.Pop().word);
            }
            res.Reverse();

            return res;
        }
    }
}

public class Word : IComparable<Word>
{
    public string word;

    public int count;

    public int CompareTo(Word other)
    {
        int diff = this.count - other.count;
        if (diff == 0)
        {
            diff = other.word.CompareTo(this.word);
        }

        return diff;
    }
}

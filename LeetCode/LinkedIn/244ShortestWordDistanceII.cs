using ConsoleApp2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{

    public class WordDistance
    {
        private Dictionary<string, List<int>> dictionary;

        public WordDistance(string[] wordsDict)
        {
            this.dictionary = new Dictionary<string, List<int>>(wordsDict.Length);
            for (int i = 0; i < wordsDict.Length; ++i)
            {
                var word = wordsDict[i];
                if (!this.dictionary.TryGetValue(word, out List<int> pos))
                {
                    pos = new List<int>();
                    this.dictionary.Add(word, pos);
                }

                pos.Add(i);
            }
        }

        public int Shortest(string word1, string word2)
        {
            if (word1 == word2)
                return 0;

            var list1 = this.dictionary[word1];
            var list2 = this.dictionary[word2];
            int index1 = 0, index2 = 0;
            int minDist = int.MaxValue;
            while (index1 < list1.Count && index2 < list2.Count && minDist != 1)
            {
                int word1Pos = list1[index1], word2Pos = list2[index2];
                minDist = Math.Min(minDist, Math.Abs(word1Pos - word2Pos));
                if (word1Pos < word2Pos)
                {
                    // find the largest next index1 that smaller than current index2
                    // if not found, return the first index1 that is larger than current index2
                    // the returned value is basically the pos that word2Pos can be inserted in on list1
                    index1 = BinarySearch(list1, word2Pos, index1 + 1, list1.Count - 1);
                }
                else
                {
                    index2 = BinarySearch(list2, word1Pos, index2 + 1, list2.Count - 1);
                }
            }

            return minDist;
        }

        private int BinarySearch(List<int> list, int targetIndex, int left, int right)
        {
            int index = -1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (list[mid] < targetIndex)
                {
                    index = mid;
                    left = mid + 1;
                }
                else // list[mid] > targetIndex
                {
                    // go too far to right
                    right = mid - 1;
                }
            }

            // left > right
            return index != -1 ? index : left;
        }
    }

    public class WordDistanceV1
    {
        private Dictionary<string, LinkedList<int>> dictionary;

        public WordDistanceV1(string[] wordsDict)
        {
            this.dictionary = new Dictionary<string, LinkedList<int>>(wordsDict.Length);
            for (int i = 0; i < wordsDict.Length; ++i)
            {
                var word = wordsDict[i];
                if (!this.dictionary.TryGetValue(word, out LinkedList<int> pos))
                {
                    pos = new LinkedList<int>();
                    this.dictionary.Add(word, pos);
                }

                pos.AddLast(i);
            }
        }

        public int Shortest(string word1, string word2)
        {
            if (word1 == word2)
                return 0;

            var node1 = this.dictionary[word1].First;
            var node2 = this.dictionary[word2].First;
            int minDist = int.MaxValue;
            while (node1 != null && node2 != null && minDist != 1)
            {
                minDist = Math.Min(minDist, Math.Abs(node1.Value - node2.Value));
                if (node1.Value < node2.Value)
                {
                    node1 = node1.Next;
                }
                else
                {
                    node2 = node2.Next;
                }
            }

            return minDist;
        }
    }
}

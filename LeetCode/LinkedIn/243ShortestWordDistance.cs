using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _243ShortestWordDistance
    {
        public int ShortestDistance(string[] wordsDict, string word1, string word2)
        {
            int index1 = FindNext(wordsDict, 0, word1);
            int index2 = FindNext(wordsDict, 0, word2);
            int dist = int.MaxValue;
            while (index1 < wordsDict.Length && index2 < wordsDict.Length)
            {
                dist = Math.Min(dist, Math.Abs(index1 - index2));

                if (index1 > index2)
                {
                    // word2 ... word1
                    index2 = FindNext(wordsDict, index2 + 1, word2);
                }
                else // index1 < index2
                {
                    // word1 ... word2
                    index1 = FindNext(wordsDict, index1 + 1, word1);
                }
            }

            return dist;
        }

        private static int FindNext(string[] wordsDict, int start, string word)
        {
            while (start < wordsDict.Length && wordsDict[start] != word)
                ++start;

            return start;
        }
    }
}

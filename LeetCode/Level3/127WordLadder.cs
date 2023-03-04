using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _127WordLadder
    {
        public int LadderLength_BFSV1(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> allWords = new HashSet<string>(wordList.Count);
            foreach(var word in wordList)
            {
                allWords.Add(word);
            }

            if (!allWords.Contains(endWord))
                return 0;

            Dictionary<string, HashSet<string>> word2Words = new Dictionary<string, HashSet<string>>(wordList.Count);
            // init word2Words based on wordList
            foreach (var word in wordList)
            {
                foreach (var newWord in this.FindAllAdjWords(word, allWords))
                {
                    if (!word2Words.TryGetValue(word, out HashSet<string> adjWords1))
                    {
                        adjWords1 = new HashSet<string>();
                        word2Words.Add(word, adjWords1);
                    }
                    adjWords1.Add(newWord);

                    if (!word2Words.TryGetValue(newWord, out HashSet<string> adjWords2))
                    {
                        adjWords2 = new HashSet<string>();
                        word2Words.Add(newWord, adjWords2);
                    }
                    adjWords2.Add(word);
                }
            }

            HashSet<string> visited = new HashSet<string>(wordList.Count);
            bool found = false;
            int layers = 0;
            Queue<string> queue = new Queue<string>(wordList.Count);
            if (allWords.Contains(beginWord))
            {
                queue.Enqueue(beginWord);
                visited.Add(beginWord);
            }
            else
            {
                foreach(var startWord in this.FindAllAdjWords(beginWord, allWords))
                {
                    queue.Enqueue(startWord);
                    visited.Add(startWord);
                }

                if (queue.Count != 0)
                    ++layers; // have done transformation once
            }

            while (!found && queue.Count != 0)
            {
                int count = queue.Count;
                for(int i = 0; i < count; ++i)
                {
                    var word = queue.Dequeue();
                    if (string.Equals(word, endWord))
                    {
                        found = true;
                        break;
                    }

                    if (word2Words.TryGetValue(word, out HashSet<string> adjWords))
                    {
                        foreach (var adjWord in adjWords)
                        {
                            if (visited.Add(adjWord))
                                queue.Enqueue(adjWord);
                        }
                    }
                }

                ++layers;
            }

            return found ? layers : 0;
        }

        public int LadderLength_BFSV2(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> allWords = new HashSet<string>(wordList.Count);
            foreach (var word in wordList)
            {
                allWords.Add(word);
            }

            if (!allWords.Contains(endWord))
                return 0;

            Dictionary<string, HashSet<string>> word2Words = new Dictionary<string, HashSet<string>>(wordList.Count);
            // init word2Words based on wordList
            foreach (var word in wordList)
            {
                foreach (var newWord in this.FindAllAdjWords(word, allWords))
                {
                    if (!word2Words.TryGetValue(word, out HashSet<string> adjWords1))
                    {
                        adjWords1 = new HashSet<string>();
                        word2Words.Add(word, adjWords1);
                    }
                    adjWords1.Add(newWord);

                    if (!word2Words.TryGetValue(newWord, out HashSet<string> adjWords2))
                    {
                        adjWords2 = new HashSet<string>();
                        word2Words.Add(newWord, adjWords2);
                    }
                    adjWords2.Add(word);
                }
            }

            Dictionary<string, int> visitedFromBegining = new Dictionary<string, int>(wordList.Count);
            Dictionary<string, int> visitedFromEnd = new Dictionary<string, int>(wordList.Count);
            Queue<(string, int)> queueFromBegining = new Queue<(string, int)>(wordList.Count);
            Queue<(string, int)> queueFromEnd = new Queue<(string, int)>(wordList.Count);
            if (allWords.Contains(beginWord))
            {
                queueFromBegining.Enqueue((beginWord, 1));
                visitedFromBegining.Add(beginWord, 1);
            }
            else
            {
                foreach (var startWord in this.FindAllAdjWords(beginWord, allWords))
                {
                    queueFromBegining.Enqueue((startWord, 2));
                    visitedFromBegining.Add(startWord, 2);
                }
            }

            queueFromEnd.Enqueue((endWord, 1));
            visitedFromEnd.Add(endWord, 1);

            while (queueFromBegining.Count != 0 && queueFromEnd.Count != 0)
            {
                Queue<(string, int)> queue;
                Dictionary<string, int> visited, visitedFromOtherEnd;

                // Progress forward one step from the shorter queue
                if (queueFromBegining.Count <= queueFromEnd.Count)
                {
                    queue = queueFromBegining;
                    visited = visitedFromBegining;
                    visitedFromOtherEnd = visitedFromEnd;
                }
                else
                {
                    queue = queueFromEnd;
                    visited = visitedFromEnd;
                    visitedFromOtherEnd = visitedFromBegining;
                }

                int count = queue.Count;
                for (int i = 0; i < count; ++i)
                {
                    var item = queue.Dequeue();
                    var word = item.Item1;
                    var level = item.Item2;
                    if (visitedFromOtherEnd.TryGetValue(word, out int otherLevel))
                    {
                        return level + otherLevel - 1; // minus 1 because the word is shared on the paths from begining and end
                    }

                    if (word2Words.TryGetValue(word, out HashSet<string> adjWords))
                    {
                        foreach (var adjWord in adjWords)
                        {
                            if (visited.TryAdd(adjWord, level + 1))
                                queue.Enqueue((adjWord, level + 1));
                        }
                    }
                }
            }

            return 0;
        }

        private IEnumerable<string> FindAllAdjWords(string word, HashSet<string> allWords)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                for (char ch = 'a'; ch <= 'z'; ++ch)
                {
                    var newWord = string.Concat(word.Substring(0, i), ch, word.Substring(i + 1));
                    if (!string.Equals(newWord, word) && allWords.Contains(newWord))
                    {
                        yield return newWord;
                    }
                }
            }
        }
    }
}

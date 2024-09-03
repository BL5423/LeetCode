using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _127WordLadder
    {
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            // *ot -> hot ...
            var wildwords2Words = new Dictionary<string, HashSet<string>>(wordList.Count);
            var word2Wildwords = new Dictionary<string, HashSet<string>>(wordList.Count);

            Preprocess(beginWord, word2Wildwords, wildwords2Words);
            foreach (var word in wordList)
            {
                Preprocess(word, word2Wildwords, wildwords2Words);
            }

            if (!word2Wildwords.ContainsKey(endWord))
                return 0;

            Dictionary<string, int> forwardSeen = new Dictionary<string, int>();
            Dictionary<string, int> backwardSeen = new Dictionary<string, int>();
            backwardSeen.Add(endWord, 0);

            Queue<string> forwardQueue = new Queue<string>();
            forwardQueue.Enqueue(beginWord);
            Queue<string> backwardQueue = new Queue<string>();
            backwardQueue.Enqueue(endWord);

            int transformations = 0, leastTranformations = int.MaxValue;
            while (forwardQueue.Count != 0)
            {
                ++transformations;
                int count = forwardQueue.Count;
                for (int w = 0; w < count; ++w)
                {
                    var curWord = forwardQueue.Dequeue();
                    if (backwardSeen.TryGetValue(curWord, out int backwardTransformations))
                        leastTranformations = Math.Min(leastTranformations, transformations + backwardTransformations);

                    foreach (var wildWord in word2Wildwords[curWord])
                    {
                        foreach (var nextWord in wildwords2Words[wildWord])
                        {
                            if (!forwardSeen.ContainsKey(nextWord))
                            {
                                forwardSeen.Add(nextWord, transformations);
                                forwardQueue.Enqueue(nextWord);
                            }
                        }
                    }
                }

                if (leastTranformations != int.MaxValue)
                    return leastTranformations;

                int backWordsCount = backwardQueue.Count;
                for (int w = 0; w < backWordsCount; ++w)
                {
                    var backWord = backwardQueue.Dequeue();
                    foreach (var wildWord in word2Wildwords[backWord])
                    {
                        foreach (var nextWord in wildwords2Words[wildWord])
                        {
                            if (!backwardSeen.ContainsKey(nextWord))
                            {
                                backwardSeen.Add(nextWord, transformations);
                                backwardQueue.Enqueue(nextWord);
                            }
                        }
                    }
                }
            }

            return 0;
        }

        private static void Preprocess(string word, IDictionary<string, HashSet<string>> word2Wildwords, IDictionary<string, HashSet<string>> wildWords2Word)
        {
            if (!word2Wildwords.ContainsKey(word))
            {
                word2Wildwords.Add(word, new HashSet<string>());
                HashSet<string> normalWords = null;
                foreach (var wildWord in GetWildWords(word))
                {
                    word2Wildwords[word].Add(wildWord);
                    if (!wildWords2Word.TryGetValue(wildWord, out normalWords))
                    {
                        normalWords = new HashSet<string>();
                        wildWords2Word.Add(wildWord, normalWords);
                    }

                    normalWords.Add(word);
                }
            }
        }

        private static IEnumerable<string> GetWildWords(string word)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                var nextWord = string.Join(string.Empty,
                                 i > 0 ? word.Substring(0, i) : string.Empty,
                                 "*",
                                 i < word.Length - 1 ? word.Substring(i + 1) : string.Empty);

                yield return nextWord;
            }
        }

        private static IEnumerable<string> GetNextWords(string curWord)
        {
            // transform cur word one character by one character
            for (int i = 0; i < curWord.Length; ++i)
            {
                for (char j = 'a'; j <= 'z'; ++j)
                {
                    if (curWord[i] != j)
                    {
                        var nextWord = string.Join(string.Empty,
                                         i > 0 ? curWord.Substring(0, i) : string.Empty,
                                         j.ToString(),
                                         i < curWord.Length - 1 ? curWord.Substring(i + 1) : string.Empty);

                        yield return nextWord;
                    }
                }
            }
        }

        public int LadderLengthV1(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> words = new HashSet<string>(wordList.Count);
            foreach (var word in wordList)
            {
                words.Add(word);
            }

            if (!words.Contains(endWord))
                return 0;

            HashSet<string> seen = new HashSet<string>();

            //    curWord, # of transformations so far
            Queue<(string, int)> queue = new Queue<(string, int)>();
            queue.Enqueue((beginWord, 1));
            while (queue.Count != 0)
            {
                var cur = queue.Dequeue();
                if (cur.Item1 == endWord)
                    return cur.Item2;

                // transform cur word one character by one character
                for (int i = 0; i < cur.Item1.Length; ++i)
                {
                    for (char j = 'a'; j <= 'z'; ++j)
                    {
                        if (cur.Item1[i] != j)
                        {
                            var nextWord = string.Join(string.Empty,
                                             i > 0 ? cur.Item1.Substring(0, i) : string.Empty,
                                             j.ToString(),
                                             i < cur.Item1.Length - 1 ? cur.Item1.Substring(i + 1) : string.Empty);

                            if (words.Contains(nextWord) && seen.Add(nextWord))
                                queue.Enqueue((nextWord, cur.Item2 + 1));
                        }
                    }
                }
            }

            return 0;
        }

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

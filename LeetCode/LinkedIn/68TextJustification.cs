using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _68TextJustification
    {
        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            var res = new List<string>();
            int wordsLength = 0;
            LinkedList<string> buffer = new LinkedList<string>();
            foreach (var word in words)
            {
                if (wordsLength + buffer.Count - 1 + word.Length < maxWidth)
                {
                    // put word on the same line
                    buffer.AddLast(word);
                    wordsLength += word.Length;
                }
                else
                {
                    if (buffer.Count > 0)
                    {
                        // generate a new line                       
                        res.Add(Output(maxWidth, wordsLength, buffer, false));
                        wordsLength = 0;
                        buffer.Clear();

                        if (word.Length < maxWidth)
                        {
                            wordsLength = word.Length;
                            buffer.AddLast(word);
                        }
                        else // word.Length == maxWidth
                        {
                            res.Add(word);
                        }
                    }
                }
            }

            if (buffer.Count != 0)
            {
                // last line                    
                res.Add(Output(maxWidth, wordsLength, buffer, true));
            }

            return res;
        }

        private static string Output(int maxWidth, int wordsLength, LinkedList<string> buffer, bool leftJustified)
        {
            StringBuilder sb = new StringBuilder(maxWidth);
            if (buffer.Count != 1)
            {
                int totalSpaces = maxWidth - wordsLength;
                if (!leftJustified)
                {
                    int slots = buffer.Count - 1;
                    int leastStride = totalSpaces / slots;
                    int stride = leastStride + 1;
                    int lastStrideBegins = totalSpaces % slots;
                    int index = 0;
                    foreach (var w in buffer)
                    {
                        sb.Append(w);
                        if (index < lastStrideBegins)
                        {
                            sb.Append(new string(' ', stride));
                        }
                        else if (index < slots)
                        {
                            sb.Append(new string(' ', leastStride));
                        }

                        ++index;
                    }
                }
                else
                {
                    int stride = 1;
                    int lastStride = maxWidth - (wordsLength + buffer.Count - 1);
                    int index = 0;
                    foreach (var w in buffer)
                    {
                        sb.Append(w);
                        if (index++ < buffer.Count - 1)
                        {
                            sb.Append(new string(' ', stride));
                        }
                    }

                    sb.Append(new string(' ', lastStride));
                }
            }
            else
            {
                var onlyWord = buffer.Last();
                sb.Append(onlyWord);
                sb.Append(new string(' ', maxWidth - onlyWord.Length));
            }

            return sb.ToString();
        }
    }
}

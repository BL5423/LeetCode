using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _151ReverseWordsInString
    {
        public string ReverseWords(string s)
        {
            int start = 0, end = s.Length - 1;
            char[] chars = s.ToCharArray();
            Reverse(chars, start, end);

            int index = 0;
            while(index < s.Length)
            {
                if (index < s.Length && chars[index++] == ' ')
                    continue;

                start = end = index - 1;
                while (end < s.Length && chars[end] != ' ') ++end;
                Reverse(chars, start, end - 1);
                index = end;
            }

            StringBuilder result = new StringBuilder();
            index = 0;
            while(index < chars.Length)
            {
                if (chars[index] != ' ')
                    result.Append(chars[index++]);
                else
                {
                    while (index < chars.Length && chars[index] == ' ')
                        ++index;
                    if (index < chars.Length && result.Length > 0)
                        result.Append(' ');
                }
            }

            return result.ToString();
        }

        private void Reverse(char[] chars, int start, int end)
        {
            while(start < end)
            {
                char ch = chars[start];
                chars[start] = chars[end];
                chars[end] = ch;

                ++start;
                --end;
            }
        }
    }
}

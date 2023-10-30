using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _Status
    {
        public int index;
    }

    public class _17LetterCombinationsOfAPhoneNumber
    {
        public IList<string> LetterCombinations(string digits)
        {
            var res = new LinkedList<string>();
            if (!string.IsNullOrEmpty(digits))
            {
                res.AddLast(string.Empty);
                foreach (var num in digits)
                {
                    var chars = this.GetCharactersPerDigit(num);
                    var list = new LinkedList<string>();
                    foreach (var str in res)
                    {
                        foreach (var ch in chars)
                        {
                            list.AddLast(string.Concat(str, ch));
                        }
                    }

                    res = list;
                }
            }

            return res.ToList();
        }

        public IList<string> LetterCombinationsDFS(string digits)
        {
            var res = new List<string>();
            if (!string.IsNullOrEmpty(digits))
            {
                Stack<(int, _Status)> stack = new Stack<(int, _Status)>();
                stack.Push((0, new _Status()));
                var list = new LinkedList<char>();
                while (stack.Count != 0)
                {
                    var item = stack.Peek();
                    if (item.Item1 == digits.Length)
                    {
                        res.Add(string.Join("", list.ToList()));
                        stack.Pop();
                        list.RemoveLast();
                        continue;
                    }

                    var chars = this.GetCharsPerDigit(digits[item.Item1]);
                    if (item.Item2.index < chars.Length)
                    {
                        list.AddLast(chars[item.Item2.index++]);
                        stack.Push((item.Item1 + 1, new _Status()));
                    }
                    else
                    {
                        stack.Pop();
                        if (list.Count != 0)
                            list.RemoveLast(); // remove the last char added by stack.Top since we'll iterate on the next char of it
                    }
                }
            }

            return res;
        }

        private char[] GetCharsPerDigit(char digit)
        {
            switch (digit)
            {
                case '2':
                    return new char[] { 'a', 'b', 'c' };
                case '3':
                    return new char[] { 'd', 'e', 'f' };
                case '4':
                    return new char[] { 'g', 'h', 'i' };
                case '5':
                    return new char[] { 'j', 'k', 'l' };
                case '6':
                    return new char[] { 'm', 'n', 'o' };
                case '7':
                    return new char[] { 'p', 'q', 'r', 's' };
                case '8':
                    return new char[] { 't', 'u', 'v' };
                case '9':
                    return new char[] { 'w', 'x', 'y', 'z' };
            }

            return null;
        }

        public IList<string> LetterCombinationsV2(string digits)
        {
            var res = new List<string>();
            if (!string.IsNullOrEmpty(digits))
            {
                Queue<(string, int)> queue = new Queue<(string, int)>();
                queue.Enqueue((string.Empty, 0));
                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    var str = item.Item1;
                    var index = item.Item2;
                    if (index == digits.Length)
                    {
                        res.Add(str.ToString());
                    }
                    else
                    {
                        foreach (var c in this.GetCharactersPerDigit(digits[index]))
                        {
                            var newStr = new string(string.Concat(str, c));
                            queue.Enqueue((newStr, index + 1));
                        }
                    }
                }
            }

            return res;
        }

        public IList<string> LetterCombinationsV1(string digits)
        {
            var combinations = new List<string>();
            if (digits.Length == 0)
                return combinations;

            combinations.AddRange(GetCharactersPerDigit(digits[0]));

            for (int index = 1; index < digits.Length; ++index)
            {
                combinations = LetterCombinationsRecursively(combinations, GetCharactersPerDigit(digits[index]));
            }

            return combinations;
        }

        private List<string> LetterCombinationsRecursively(IList<string> combinations, string[] characters)
        {
            var newCombinations = new List<string>();
            foreach (string combination in combinations)
            {
                foreach (string ch in characters)
                {
                    var newCombination = combination + ch;
                    newCombinations.Add(newCombination);
                }
            }

            // keep the latest combinations
            return newCombinations;
        }

        private string[] GetCharactersPerDigit(char digit)
        {
            switch (digit)
            {
                case '2':
                    return new string[] { "a", "b", "c" };
                case '3':
                    return new string[] { "d", "e", "f" };
                case '4':
                    return new string[] { "g", "h", "i" };
                case '5':
                    return new string[] { "j", "k", "l" };
                case '6':
                    return new string[] { "m", "n", "o" };
                case '7':
                    return new string[] { "p", "q", "r", "s" };
                case '8':
                    return new string[] { "t", "u", "v" };
                case '9':
                    return new string[] { "w", "x", "y", "z" };
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _17LetterCombinationsOfAPhoneNumber
    {
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

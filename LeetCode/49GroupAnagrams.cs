using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _49GroupAnagrams
    {
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, IList<string>> dic = new Dictionary<string, IList<string>>();
            foreach (string str in strs)
            {
                if (str == null)
                    continue;

                string hash = GetHash(str);
                if (dic.TryGetValue(hash, out IList<string> values))
                {
                    values.Add(str);
                }
                else
                {
                    var newValues = new List<string>();
                    newValues.Add(str);
                    dic.Add(hash, newValues);
                }                    
            }

            List<IList<string>> results = new List<IList<string>>();
            results.AddRange(dic.Values.Select(value => value));
            return results;
        }

        private string GetHash(string input)
        {
            int[] chars = new int[26];
            foreach(char ch in input)
            {
                ++chars[ch - 'a'];
            }

            return string.Join(",", chars);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Amazon
{
    public class _937ReorderDatainLogFiles
    {
        public string[] ReorderLogFiles(string[] logs)
        {
            var letterlogs = new List<string[]>(logs.Length);
            var digitlogs = new List<string>(logs.Length);
            foreach (var log in logs)
            {
                if (Check(log, out string id, out string content))
                {
                    digitlogs.Add(log);
                }
                else
                {
                    letterlogs.Add(new string[3] { id, content, log });
                }
            }

            letterlogs.Sort((a, b) => a[1].CompareTo(b[1]) != 0 ? a[1].CompareTo(b[1]) : a[0].CompareTo(b[0]));
            return letterlogs.Select(a => a[2]).Concat(digitlogs).ToArray();
        }

        private static bool Check(string log, out string id, out string content)
        {
            bool hasDigit = false;
            id = null;
            content = null;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < log.Length; ++i)
            {
                if (log[i] == ' ')
                {
                    if (id == null)
                    {
                        id = sb.ToString();
                        content = log.Substring(i + 1);
                    }
                }
                else if (id != null)
                {
                    hasDigit = log[i] >= '0' && log[i] <= '9';
                    break;
                }
                else
                {
                    sb.Append(log[i]);
                }
            }

            return hasDigit;
        }
    }
}

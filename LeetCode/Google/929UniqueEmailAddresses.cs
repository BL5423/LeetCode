using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _929UniqueEmailAddresses
    {
        public int NumUniqueEmails(string[] emails)
        {
            var finalEmails = new HashSet<string>(emails.Length);
            foreach (var email in emails)
            {
                var sb = new StringBuilder(email.Length);
                for (int i = 0; i < email.Length; ++i)
                {
                    if (email[i] == '@')
                    {
                        sb.Append(email.Substring(i));
                        break;
                    }
                    else if (email[i] == '+')
                    {
                        sb.Append(email.Substring(email.IndexOf('@', i + 1)));
                        break;
                    }
                    else if (email[i] != '.')
                    {
                        sb.Append(email[i]);
                    }
                }

                finalEmails.Add(sb.ToString());
            }

            return finalEmails.Count;
        }
    }
}

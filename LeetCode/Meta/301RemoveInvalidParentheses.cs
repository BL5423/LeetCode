using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _301RemoveInvalidParentheses
    {
        private int minRemovals = int.MaxValue;

        public IList<string> RemoveInvalidParentheses(string s)
        {
            return RemoveParenthesesBFS(s);
        }

        private IList<string> RemoveParenthesesBFS(string s)
        {
            int leftsRemained = 0, rightsRemained = 0;
            foreach (char ch in s)
            {
                if (ch == '(')
                    ++leftsRemained;
                else if (ch == ')')
                {
                    if (leftsRemained > 0)
                        --leftsRemained;
                    else
                        ++rightsRemained;
                }
            }

            var seen = new HashSet<string>();
            var res = new List<string>();
            Queue<(string, int, int)> q = new Queue<(string, int, int)>();
            q.Enqueue((s, leftsRemained, rightsRemained));
            while (q.Count != 0 && res.Count == 0)
            {
                for (int c = q.Count; c > 0; --c)
                {
                    var node = q.Dequeue();
                    var curStr = node.Item1;
                    var lefts = node.Item2;
                    var rights = node.Item3;
                    if (lefts == 0 && rights == 0)
                    {
                        int openLefts = 0;
                        for(int i = 0; i < curStr.Length && openLefts >= 0; ++i)
                        {
                            if (curStr[i] == '(')
                                ++openLefts;
                            else if (curStr[i] == ')')
                                --openLefts;
                        }

                        if (openLefts == 0)
                            res.Add(curStr);
                    }
                    else
                    {
                        for(int i = 0; i < curStr.Length; ++i)
                        {
                            if (curStr[i] == '(' && lefts - 1 >= 0)
                            {
                                var newStr = curStr.Substring(0, i) + curStr.Substring(i + 1);
                                if (seen.Add(newStr))
                                    q.Enqueue((newStr, lefts - 1, rights));
                            }
                            else if (curStr[i] == ')' && rights - 1 >= 0)
                            {
                                var newStr = curStr.Substring(0, i) + curStr.Substring(i + 1);
                                if (seen.Add(newStr))
                                    q.Enqueue((newStr, lefts, rights - 1));
                            }
                        }
                    }
                }
            }

            return res;
        }

        private void RemoveParentheses(string s, int index, bool[] removed, int lefts, int rights, int leftsRemained, int rightsRemained, HashSet<string> res)
        {
            if (leftsRemained < 0 || rightsRemained < 0)
                return;

            if (index == s.Length)
            {
                if (leftsRemained == 0 && rightsRemained == 0)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < s.Length; ++i)
                    {
                        if (removed[i])
                            continue;

                        sb.Append(s[i]);
                    }
                    res.Add(sb.ToString());
                }

                return;
            }

            // do nothing
            int nextlefts = lefts + (s[index] == '(' ? 1 : 0);
            int nextRights = rights + (s[index] == ')' ? 1 : 0);
            if (nextlefts >= nextRights)
            {
                RemoveParentheses(s, index + 1, removed, nextlefts, nextRights, leftsRemained, rightsRemained, res);
            }

            // try to remove
            if (!removed[index] && (s[index] == '(' || s[index] == ')'))
            {
                if (s[index] == '(')
                {
                    --leftsRemained;
                }
                else
                {
                    --rightsRemained;
                }

                removed[index] = true;
                RemoveParentheses(s, index + 1, removed, lefts, rights, leftsRemained, rightsRemained, res);
                removed[index] = false;
            }
        }

        private void RemoveParenthesesV2(string s, int index, bool[] removed, int removals, int lefts, int rights, HashSet<string>[] res)
        {
            if (removals > this.minRemovals)
                return;

            if (index == s.Length)
            {
                if (lefts == rights && removals <= this.minRemovals)
                { 
                    if (res[removals] == null)
                        res[removals] = new HashSet<string>();
                    var sb = new StringBuilder();
                    for (int i = 0; i < s.Length; ++i)
                    {
                        if (removed[i])
                            continue;

                        sb.Append(s[i]);
                    }
                    res[removals].Add(sb.ToString());

                    this.minRemovals = removals;
                }

                return;
            }

            // do nothing
            int nextlefts = lefts + (s[index] == '(' ? 1 : 0);
            int nextRights = rights + (s[index] == ')' ? 1 : 0);
            if (nextlefts >= nextRights)
            {
                RemoveParenthesesV2(s, index + 1, removed, removals, nextlefts, nextRights, res);
            }

            // try to remove
            if (!removed[index] && (s[index] == '(' || s[index] == ')'))
            {
                removed[index] = true;
                RemoveParenthesesV2(s, index + 1, removed, removals + 1, lefts, rights, res);
                removed[index] = false;
            }
        }

        private void RemoveParenthesesV1(string s, int index, bool[] removed, int removals, HashSet<string>[] res)
        {
            if (index == s.Length)
            {
                if (IsValid(s, removed, out string newS))
                {
                    if (res[removals] == null)
                        res[removals] = new HashSet<string>();
                    res[removals].Add(newS);

                    this.minRemovals = Math.Min(this.minRemovals, removals);
                }

                return;
            }

            // do nothing, s[index] might be a letter
            RemoveParenthesesV1(s, index + 1, removed, removals, res);

            if (removals >= this.minRemovals)
                return;

            // try to remove s[i]
            if (!removed[index] && (s[index] == '(' || s[index] == ')'))
            {
                // remove s[index] from s
                removed[index] = true;
                RemoveParenthesesV1(s, index + 1, removed, removals + 1, res);
                removed[index] = false;
            }
        }

        private bool IsValid(string s, bool[] removed, out string newS)
        {
            newS = null;
            var sb = new StringBuilder();
            int openLeft = 0;
            for (int i = 0; i < s.Length; ++i)
            {
                if (removed[i])
                    continue;

                char ch = s[i];
                sb.Append(ch);
                if (ch == '(')
                    ++openLeft;
                else if (ch == ')')
                    --openLeft;

                if (openLeft < 0)
                    return false;
            }

            if (openLeft == 0)
            {
                newS = sb.ToString();
                return true;
            }

            return false;
        }
    }
}

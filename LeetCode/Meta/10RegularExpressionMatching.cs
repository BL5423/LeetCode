using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _10RegularExpressionMatching
    {
        private bool?[,] cache;

        public bool IsMatch(string s, string p)
        {
            return IsMatchDP_BottomUp(s, p);
        }

        private bool IsMatchDP_BottomUp(string s, string p)
        {
            bool[] prevDp = new bool[p.Length + 1];
            bool[] curDp = new bool[p.Length + 1];
            curDp[p.Length] = true;

            for (int indexS = s.Length; indexS >= 0; --indexS)
            {
                for (int indexP = p.Length - 1; indexP >= 0; --indexP)
                {
                    bool match = false;
                    bool firstMatch = indexS < s.Length && IsMatch(s[indexS], p[indexP]);
                    if (indexP + 1 < p.Length && p[indexP + 1] == '*')
                    {
                        // skip x* from p
                        match = curDp[indexP + 2] ||
                            // if first character matched, try to proceed s
                            (firstMatch && prevDp[indexP]);
                    }
                    else
                    {
                        match = firstMatch && prevDp[indexP + 1];
                    }

                    curDp[indexP] = match;
                }

                var temp = curDp;
                curDp = prevDp;
                prevDp = temp;
            }

            return prevDp[0];
        }

        private bool IsMatchDP_BottomUpv1(string s, string p)
        {
            bool[,] dp = new bool[s.Length + 1, p.Length + 1];
            dp[s.Length, p.Length] = true; // base state

            for(int indexS = s.Length; indexS >= 0; --indexS)
            {
                for(int indexP = p.Length - 1; indexP >= 0; --indexP)
                {
                    bool match = false;
                    bool firstMatch = indexS < s.Length && IsMatch(s[indexS], p[indexP]);
                    if (indexP + 1 < p.Length && p[indexP + 1] == '*')
                    {
                        // skip x* from p
                        match = dp[indexS, indexP + 2] ||
                            // if first character matched, try to proceed s
                            (firstMatch && dp[indexS + 1, indexP]);
                    }
                    else
                    {
                        match = firstMatch && dp[indexS + 1, indexP + 1];
                    }

                    dp[indexS, indexP] = match;
                }
            }

            return dp[0, 0];
        }

        private bool IsMatchDP_TopDown(string s, int indexS, string p, int indexP)
        {
            if (cache[indexS, indexP] != null)
                return cache[indexS, indexP].Value;

            if (p.Length == indexP)
            {
                cache[indexS, indexP] = s.Length == indexS;
                return cache[indexS, indexP].Value;
            }

            bool match = false;
            bool firstMatch = indexS < s.Length && IsMatch(s[indexS], p[indexP]);
            if (indexP + 1 < p.Length && p[indexP + 1] == '*')
            {
                // skip x* from p
                match = IsMatchDP_TopDown(s, indexS, p, indexP + 2) ||
                    // if first character matched, try to proceed s
                    (firstMatch && IsMatchDP_TopDown(s, indexS + 1, p, indexP));
            }
            else
            {
                match = firstMatch && IsMatchDP_TopDown(s, indexS + 1, p, indexP + 1);
            }

            cache[indexS, indexP] = match;
            return match;
        }

        private bool IsMatchDPv1(string s, int indexS, string p, int indexP)
        {
            if (this.cache[indexS, indexP] != null)
                return this.cache[indexS, indexP].Value;

            bool match = false;
            if (indexS == s.Length)
            {
                if (indexP == p.Length)
                    match = true;
                else// pIndex != p.Length
                {
                    if (p[indexP] == '*')
                        ++indexP;

                    if (indexP == p.Length)
                        match = true;
                    else if ((p.Length - indexP) % 2 == 0)
                    {
                        for (int i = indexP + 1; i < p.Length; i += 2)
                        {
                            match = true;
                            if (p[i] != '*')
                            {
                                match = false;
                                break;
                            }
                        }
                    }
                }

                return match;
            }
            if (indexP == p.Length)
            {
                if (indexS == s.Length)
                    match = true;

                return match;
            }

            if (IsMatch(s[indexS], p[indexP]))
            {
                match = IsMatchDPv1(s, indexS + 1, p, indexP + 1);
                if (!match &&
                    indexP + 1 < p.Length && p[indexP + 1] == '*')
                {
                    match = IsMatchDPv1(s, indexS, p, indexP + 2);
                }
            }
            else if (p[indexP] == '*')
            {
                if (indexS > 0 && s[indexS - 1] == s[indexS])
                {
                    match = IsMatchDPv1(s, indexS + 1, p, indexP + 1) ||
                            IsMatchDPv1(s, indexS + 1, p, indexP) ||
                            IsMatchDPv1(s, indexS, p, indexP + 1);
                }
                else
                {
                    // ab -> a*
                    if (p[indexP - 1] != '.')
                        match = IsMatchDPv1(s, indexS, p, indexP + 1);
                    else
                    {   // ab -> .*
                        match = IsMatchDPv1(s, indexS + 1, p, indexP + 1) ||
                                IsMatchDPv1(s, indexS + 1, p, indexP) ||
                                IsMatchDPv1(s, indexS, p, indexP + 1);
                    }
                }
            }
            else // ab -> c
            {
                if (indexP + 1 < p.Length && p[indexP + 1] == '*')
                    match = IsMatchDPv1(s, indexS, p, indexP + 2);
            }

            this.cache[indexS, indexP] = match;
            return match;
        }

        public bool IsMatchBFS(string s, string p)
        {
            bool match = false;
            bool[,] seen = new bool[s.Length + 1, p.Length + 1];
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((0, 0));
            while (!match && queue.Count != 0)
            {
                var node = queue.Dequeue();
                int sIndex = node.Item1, pIndex = node.Item2;
                if (seen[sIndex, pIndex])
                    continue;
                seen[sIndex, pIndex] = true;

                if (sIndex == s.Length)
                {
                    if (pIndex == p.Length)
                        match = true;
                    else// pIndex != p.Length
                    {
                        if (p[pIndex] == '*')
                            ++pIndex;

                        if (pIndex == p.Length)
                            match = true;
                        else if ((p.Length - pIndex) % 2 == 0)
                        {
                            for (int i = pIndex + 1; i < p.Length; i += 2)
                            {
                                match = true;
                                if (p[i] != '*')
                                {
                                    match = false;
                                    break;
                                }
                            }
                        }
                    }

                    continue;
                }
                if (pIndex == p.Length)
                {
                    if (sIndex == s.Length)
                        match = true;

                    continue;
                }

                // a -> a or .
                if (IsMatch(s[sIndex], p[pIndex]))
                {
                    queue.Enqueue((sIndex + 1, pIndex + 1));
                    if (pIndex + 1 < p.Length && p[pIndex + 1] == '*')
                    {   // a -> a*
                        // try to skip the current pair from p
                        queue.Enqueue((sIndex, pIndex + 2));
                    }
                }
                else if (p[pIndex] == '*')
                { // a -> *
                    if (sIndex > 0 && s[sIndex - 1] == s[sIndex])
                    { // aa -> a* or .*
                        queue.Enqueue((sIndex + 1, pIndex + 1));
                        queue.Enqueue((sIndex + 1, pIndex));
                        queue.Enqueue((sIndex, pIndex + 1));
                    }
                    else
                    {
                        // ab -> a*
                        if (p[pIndex - 1] != '.')
                            queue.Enqueue((sIndex, pIndex + 1));
                        else
                        {   // ab -> .*
                            queue.Enqueue((sIndex + 1, pIndex + 1));
                            queue.Enqueue((sIndex + 1, pIndex));
                            queue.Enqueue((sIndex, pIndex + 1));
                        }
                    }
                }
                else // ab -> c
                {
                    if (pIndex + 1 < p.Length && p[pIndex + 1] == '*')
                        queue.Enqueue((sIndex, pIndex + 2));
                }
            }

            return match;
        }

        private bool IsMatch(char s, char p)
        {
            return s == p || p == '.';
        }
    }
}

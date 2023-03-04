using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level3
{
    public class _28FindtheIndexoftheFirstOccurrenceinaString
    {
        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
                return -1;

            // All the longest lengths of the proper prefix in all suffixes at each index of needle
            //int[] lps = this.MP(needle);
            int[] lps = this.KMP(needle);

            int i = 0, j = 0;
            while (i < haystack.Length && j < needle.Length)
            {
                if (haystack[i] == needle[j])
                {
                    ++i;
                    ++j;

                    // found a match
                    if (j == needle.Length)
                    {
                        return i - j;
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        // skip the lps[j - 1] characters since they have been matched in i(as suffix) and j(as prefix)
                        // haystack  ...mmmmmsssx...
                        //                      |
                        //                      i
                        //
                        // needle    ...pppmmmmmx...
                        //                      |
                        //                      j
                        //
                        // in above, m means a matched character, s is a m and also part of the suffix of all matched characters in haystack
                        // p is a m too and part of the prefix of all matched characters in needle
                        // the length of s's and p's are same and equals to lsp[j-1], where j is the index of x, where a mismatch is found
                        j = lps[j - 1];
                    }
                    else
                    {
                        // if j == 0, it means there is no match yet, hence move i one step forward
                        ++i;
                    }
                }
            }

            return -1;
        }

        public int[] KMP(string needle)
        {
            int[] lps = new int[needle.Length];
            lps[0] = 0;
            int i = 1, len = lps[0];

            while (i < needle.Length)
            {
                if (needle[i] == needle[len])
                {
                    lps[i++] = ++len;

                    // *After above line*
                    // lps[i - 1] = len
                    // pppppppp      x...ssssssss    y...
                    //        |      |          |    |
                    //      len-1   len        i-1   i
                    // if y is not a match when comparing with other text, then we will move the index backwards to lps[i - 1] which is len
                    // and furthermore, if x(at index len/lps[i - 1]) equals to y, then we know it won't match either, so we can keep skipping in lps by 
                    // resetting lps[i - 1] to the length of a proper prefix of substring ends before len(i.e. lps[len - 1]), similar like a recursion.
                    // in this way, we don't have to waste time on x in the case that y is not a match.
                    if (i < needle.Length && needle[i] == needle[len])
                        lps[i - 1] = lps[len - 1]; // lps[lps[i - 1] - 1]
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i++] = len;
                    }
                }
            }

            return lps;
        }

        public int[] MP(string needle)
        {
            int[] lps = new int[needle.Length];
            lps[0] = 0;
            int i = 1, len = lps[0]; // len is the previous longest length of a proper prefix of needle, so it is initialized as lps[0]

            // iterate all i's to find the longest length of a proper prefix ends at i
            // i starts at 1 since lps[0] has been set as 0 as base status
            while (i < needle.Length)
            {
                //
                // needle:  ...mmmmmsssx
                //                     |
                //                     i
                //
                // needle: ...pppmmmmmx
                //                    |
                //                   len
                // similar as the search process, m is a match character. And if needle[i] = needle[len], then it is another matched character
                // otherwise, move len backwards to a previous longest length of a proper fix by looking at lps[len - 1] if len > 0
                // else, there is no match for i, then set lps[i] to 0 and move i forward

                // Another perspective:
                // pppppppx...sssssssy...
                //        |          |
                //       len         i
                // len = length of the longest proper prefix before x, aka p's.
                // if x == y, which means pppppppx == sssssssy and then pppppppx becomes the proper prefix for substring ends at index i
                // otherwise(i.e. x != y)
                //   if len != 0, then move len backwards to the previous proper prefix, whose length is lps[len - 1]:
                //     PPPppppx...ssssSSSy...
                //       |               |
                //      len'             i
                //     here PPP is the previous proper prefix and SSS is the corresponding suffix.
                //
                //   else if len == 0, which means there was no proper prefix found, hence lps[i] should be 0 and move i one step forward
                if (needle[i] == needle[len])
                {
                    lps[i++] = ++len;
                }
                else
                {
                    if (len != 0)
                    {
                        // or refer to https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/discuss/13160/Detailed-explanation-on-building-up-lps-for-KMP-algorithm                
                        //This is tricky. Consider the example "ababe......ababc", i is index of 'c', len==4. The longest prefix suffix is "abab",
                        //when pat[i]!=pat[len], we get new prefix "ababe" and suffix "ababc", which are not equal. 
                        //This means we can't increment length of lps based on current lps "abab" with len==4.
                        //***We may want to increment it based on
                        //***the longest prefix suffix with length < len==4, which by definition is lps of "abab". So we set len to lps[len-1],
                        //which is 2, now the lps is "ab". Then check pat[i]==pat[len] again due to the while loop, which is also the reason
                        //why we do not increment i here. The iteration of i terminate until len==0 (didn't find lps ends with pat[i]) or found
                        //a lps ends with pat[i].

                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i++] = len;
                    }
                }
            }

            return lps;
        }

        public int StrStrV1(string haystack, string needle)
        {
            if (haystack.Length < needle.Length)
                return -1;
            int[] needleCode = new int['z' - 'a' + 1];
            int[] haystackCode = new int['z' - 'a' + 1];
            for(int i = 0; i < needle.Length; ++i)
            {
                ++needleCode[needle[i] - 'a'];
                ++haystackCode[haystack[i] - 'a'];
            }

            int startIndex = 0, endIndex = needle.Length - 1;
            while (startIndex <= endIndex && endIndex < haystack.Length)
            {
                if (haystackCode.SequenceEqual(needleCode) &&
                    haystack.Substring(startIndex, endIndex - startIndex + 1).Equals(needle))
                {
                    return startIndex;
                }
                else
                {
                    --haystackCode[haystack[startIndex++] - 'a'];
                    if (endIndex < haystack.Length - 1)
                        ++haystackCode[haystack[++endIndex] - 'a'];
                    else
                        break;
                }
            }

            return -1;
        }
    }
}

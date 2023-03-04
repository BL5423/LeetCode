using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _43MultiplyStrings
    {
        public string Multiply(string num1, string num2)
        {
            // make sure num2 is shorter than num1
            if (num1.Length < num2.Length)
                return Multiply(num2, num1);

            var m = MultiplyDigits(num1, num2);
            return Sum(m, num1.Length, num2.Length);
        }

        private IList<int[]> MultiplyDigits(string num1, string num2)
        {
            var m = new List<int[]>(num2.Length);
            for(int j = num2.Length - 1; j >=0; --j)
            { 
                char ch2 = num2[j];
                int[] res = new int[num1.Length + 1];
                int carry = 0;
                for(int i = num1.Length - 1; i >= 0; --i)
                {
                    var t = (num1[i] - '0') * (ch2 - '0') + carry;
                    carry = 0;
                    if (t >= 10)
                    {                        
                        carry = t / 10;
                        t %= 10;
                    }

                    res[i + 1] = t;
                }

                res[0] = carry;
                m.Add(res);
            }

            return m;
        }

        private string Sum(IList<int[]> m, int len1, int len2)
        {
            char[] res = new char[len1 + len2];
            for(int i = 0; i < res.Length; ++i)
            {
                res[i] = '0';
            }

            bool allZeros = true;
            int index = res.Length - 1;
            for(int i = 0; i < m.Count; ++i)
            {
                int start = res.Length - 1 - i;
                int[] mi = m[i];
                int carry = 0;
                for(int j = mi.Length - 1; j >= 0; --j)
                {
                    int sum = res[start] - '0' + mi[j] + carry;
                    carry = 0;
                    if (sum >= 10)
                    {
                        sum -= 10;
                        carry = 1;
                    }
                    if (sum != 0)
                    {
                        allZeros = false;
                    }

                    res[start--] = (char)(sum + '0');
                }
                if (carry == 1)
                {
                    res[start--] = '1';
                }

                index = start;
            }
            
            int left = index + 1;

            while (res[left] == '0' && left < res.Length - 1)
            {
                ++left;
                if (allZeros)
                {
                    left = res.Length - 1;
                    break;
                }
            }

            return new string(res, left, res.Length - left);
        }
    }
}

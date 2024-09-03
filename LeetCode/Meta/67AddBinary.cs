using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _67AddBinary
    {
        public string AddBinary(string aStr, string bStr)
        {
            LinkedList<char> res = new LinkedList<char>();
            int indexA = aStr.Length - 1, indexB = bStr.Length - 1, carry = 0;
            while (indexA >= 0 && indexB >= 0)
            {
                char a = aStr[indexA--];
                char b = bStr[indexB--];
                if (a == '0' && b == '0')
                {
                    res.AddFirst((char)(carry + '0'));
                    carry = 0;
                }
                else if (a == '0' && b == '1')
                {
                    res.AddFirst(carry == 0 ? '1' : '0');
                }
                else if (a == '1' && b == '0')
                {
                    res.AddFirst(carry == 0 ? '1' : '0');
                }
                else // a == 1 && b == 1
                {
                    res.AddFirst((char)(carry + '0'));
                    carry = 1;
                }
            }

            while (indexA >= 0)
            {
                if (carry == 0)
                    res.AddFirst(aStr[indexA--]);
                else
                {
                    if (aStr[indexA--] == '1')
                        res.AddFirst('0');
                    else
                    {
                        res.AddFirst('1');
                        carry = 0;
                    }
                }
            }

            while (indexB >= 0)
            {
                if (carry == 0)
                    res.AddFirst(bStr[indexB--]);
                else
                {
                    if (bStr[indexB--] == '1')
                        res.AddFirst('0');
                    else
                    {
                        res.AddFirst('1');
                        carry = 0;
                    }
                }
            }

            if (carry != 0)
                res.AddFirst('1');

            return string.Join("", res);
        }
    }
}

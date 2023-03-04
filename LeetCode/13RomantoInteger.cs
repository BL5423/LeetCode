using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    public class RomantoInteger
    {
        public int RomanToInt(string s)
        {
            int total = 0;
            int index = 0;
            while (index <= s.Length - 1)
            {
                /* IV = 4, IX = 9, XL = 40, XC = 90, CD = 400, CM = 900                
                    I             1
                    V             5
                    X             10
                    L             50
                    C             100
                    D             500
                    M             1000
                */
                char c = s[index];
                if (c == 'I')
                {
                    if (index + 1 <= s.Length -1)
                    {
                        char next = s[index + 1];
                        if (next == 'V')
                        {
                            // IV
                            total += 4;
                            index += 2;
                            continue;
                        }
                        else if(next == 'X')
                        {
                            // IX
                            total += 9;
                            index += 2;
                            continue;
                        }
                    }

                    // Single I
                    total += 1;
                    ++index;
                }
                else if (c == 'X')
                {
                    if (index + 1 <= s.Length - 1)
                    {
                        char next = s[index + 1];
                        if (next == 'L')
                        {
                            // XL
                            total += 40;
                            index += 2;
                            continue;
                        }
                        else if (next == 'C')
                        {
                            // XC
                            total += 90;
                            index += 2;
                            continue;
                        }
                    }

                    // Single X
                    total += 10;
                    ++index;
                }
                else if (c == 'C')
                {
                    if (index + 1 <= s.Length - 1)
                    {
                        char next = s[index + 1];
                        if (next == 'D')
                        {
                            // CD
                            total += 400;
                            index += 2;
                            continue;
                        }
                        else if (next == 'M')
                        {
                            // CM
                            total += 900;
                            index += 2;
                            continue;
                        }
                    }

                    // Single C
                    total += 100;
                    ++index;
                }
                else if (c == 'V')
                {
                    total += 5;
                    ++index;
                }
                else if (c == 'L')
                {
                    total += 50;
                    ++index;
                }
                else if (c == 'D')
                {
                    total += 500;
                    ++index;
                }
                else if (c == 'M')
                {
                    total += 1000;
                    ++index;
                }
            }


            return total;
        }

        public string IntToRoman(int num)
        {
            /*  I             1
                IV            4
                V             5
                IX            9
                X             10
                XL            40
                L             50
                XC            90
                C             100
                CD            400
                D             500
                CM            900 
                M             1000
            */
            StringBuilder sb = new StringBuilder(30);

            int value = num;

            int m = value / 1000;
            if (m >= 1)
            {
                sb.Append(new string('M', m));
                value %= 1000;
            }

            if (value >= 900)
            {
                sb.Append("CM");
                value -= 900;
            }

            int d = value / 500;
            if (d >= 1)
            {
                sb.Append(new string('D', d));
                value %= 500;
            }

            if (value >= 400)
            {
                sb.Append("CD");
                value -= 400;
            }

            int c = value / 100;
            if (c >= 1)
            {
                sb.Append(new string('C', c));
                value %= 100;
            }

            if (value >= 90)
            {
                sb.Append("XC");
                value -= 90;
            }

            int l = value / 50;
            if (l >= 1)
            {
                sb.Append(new string('L', l));
                value %= 50;
            }

            if (value >= 40)
            {
                sb.Append("XL");
                value -= 40;
            }

            int x = value / 10;
            if (x >= 1)
            {
                sb.Append(new string('X', x));
                value %= 10;
            }

            if (value >= 9)
            {
                sb.Append("IX");
                value -= 9;
            }

            int v = value / 5;
            if (v >= 1)
            {
                sb.Append(new string('V', v));
                value %= 5;
            }

            if (value >= 4)
            {
                sb.Append("IV");
                value -= 4;
            }

            int i = value;
            if (i >= 1)
            {
                sb.Append(new string('I', i));
            }

            return sb.ToString();
        }
    }
}

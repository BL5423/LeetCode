using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Meta
{
    public class _468ValidateIPAddress
    {
        private const int BASE = 16;

        private const int MaxV4 = 2 * BASE * BASE + 5 * BASE + 6;

        public string ValidIPAddress(string queryIP)
        {
            bool valid = true;
            int digits = 0, ipType = 0, value = 0, delimiters = 0;
            char firstDigit = (char)255;
            foreach (var c in queryIP)
            {
                if (c == '.')
                {
                    ++delimiters;

                    // v4
                    if (ipType != 2 && digits > 0 && digits < 4 &&
                       !(digits > 1 && firstDigit == '0') && value < MaxV4)
                    {
                        ipType = 1;
                        digits = 0;
                        value = 0;
                        firstDigit = (char)255;
                        continue;
                    }

                    valid = false;
                    break;
                }
                else if (c == ':')
                {
                    ++delimiters;

                    // v6
                    if (ipType != 1 && digits > 0 && digits <= 4)
                    {
                        ipType = 2;
                        digits = 0;
                        value = 0;
                        firstDigit = (char)255;
                        continue;
                    }

                    valid = false;
                    break;
                }
                else if (c >= '0' && c <= '9')
                {
                    ++digits;
                    if (digits > 4)
                    {
                        valid = false;
                        break;
                    }
                    else if (digits == 1)
                    {
                        firstDigit = c;
                    }

                    value = value * BASE + (c - '0');
                }
                else if (c >= 'a' && c <= 'f' || c >= 'A' && c <= 'F')
                {
                    if (ipType == 0)
                        ipType = 2;
                    else if (ipType == 1)
                    {
                        valid = false;
                        break;
                    }

                    ++digits;
                    if (digits > 4)
                    {
                        valid = false;
                        break;
                    }

                    value = value * BASE + (10 + (c >= 'a' ? c - 'a' : c - 'A'));
                }
                else
                {
                    valid = false;
                    break;
                }
            }

            if (!valid)
                return "Neither";

            if (ipType == 1 && delimiters == 3)
            {
                if (ipType != 2 && digits > 0 && digits < 4 &&
                    !(digits > 1 && firstDigit == '0') && value < MaxV4)
                {
                    return "IPv4";
                }
            }

            if (ipType == 2 && delimiters == 7)
            {
                if (ipType != 1 && digits > 0 && digits <= 4)
                {
                    return "IPv6";
                }
            }

            return "Neither";
        }
    }
}

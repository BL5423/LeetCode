using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class MajorityNumber
    {
        public int MajorityElement(int[] nums)
        {
            int count = 0;
            int m = 0;
            foreach (int num in nums)
            {
                if (count == 0 || num == m)
                {
                    m = num;
                    ++count;
                }
                else if (count > 0) 
                {                    
                    --count;
                }
            }

            if (count > 0)
            {
                return m;
            }

            throw new ArgumentException("There is no majority number.");
        }

        public IList<int> MajorityElement2(int[] nums)
        {
            int countA = 0, countB = 0, countC = 0;
            int a = 0, b = 0, c = 0;

            foreach (int num in nums)
            {
                if ((countA == 0 || num == a) && num != b && num != c)
                {
                    a = num;
                    ++countA;
                }
                else if ((countB == 0 || num == b) && num != c)
                {
                    b = num;
                    ++countB;
                }
                else if (countC == 0 || num == c)
                {
                    c = num;
                    ++countC;
                }
                else
                {
                    if (countA > 0)
                        --countA;

                    if (countB > 0)
                        --countB;

                    if (countC > 0)
                        --countC;              
                }
            }

            var result = new List<int>(3);

            if (countA > 0 && nums.Count(x => x == a) > nums.Length / 3)
            {
                result.Add(a);
            }

            if (countB > 0 && nums.Count(x => x == b) > nums.Length / 3)
            {
                result.Add(b);
            }

            if (countC > 0 && nums.Count(x => x == c) > nums.Length / 3)
            {
                result.Add(c);
            }

            return result;
        }
    }
}

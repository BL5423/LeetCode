using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class CountPrime
    {
        public int CountPrimes(int num)
        {
            int totalOfPrimes = num > 2 ? num - 2 : 0;
            HashSet<int> nonPrimes = new HashSet<int>();
            int prime = 2;
            while (prime * prime < num)
            {
                if (!nonPrimes.Contains(prime))
                {
                    int p = prime * prime;
                    while (num > p)
                    {
                        nonPrimes.Add(p);
                        p += prime;
                    }
                }

                ++prime;
            }

            return totalOfPrimes - nonPrimes.Count;

            //int primes = 0;
            //for (int p = 2; p < num; ++p)
            //{
            //    if (!nonPrimes.Contains(p))
            //    {
            //        ++primes;
            //    }
            //}

            //return primes;
        }

        public int CountPrimesV1(int n)
        {
            bool[] isPrime = new bool[n];
            for(int index = 0; index < isPrime.Length; ++index)
            {
                isPrime[index] = true;
            }

            for(int p = 2; p * p < n; ++p)
            {
                if (!isPrime[p])
                    continue;

                int next = p * p;
                while (next < n)
                {
                    isPrime[next] = false;
                    next += p;
                }
            }

            int count = 0;
            for (int i = n - 1; i >= 2; --i)
            {
                if (isPrime[i])
                {
                    ++count;
                }
            }

            return count;
        }
    }
}

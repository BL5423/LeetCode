using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LC.Graph
{
    public class _952LargestComponentSizebyCommonFactor
    {
        public int LargestComponentSize(int[] nums)
        {
            int maxNum = nums.Max();
            var uf = new DisjointUnion(maxNum + 1);
            var num2Prime = new Dictionary<int, int>(nums.Length);
            for (int i = 0; i < nums.Length; ++i)
            {
                if (nums[i] > 1)
                {
                    var primeFactors = this.PrimeDecompose(nums[i]);
                    foreach (var prime in primeFactors)
                    {
                        if (!num2Prime.TryGetValue(nums[i], out int firstPrime))
                        {
                            firstPrime = prime;
                            num2Prime.Add(nums[i], prime);
                        }
                        else
                        {
                            uf.Union(firstPrime, prime);
                        }
                    }
                }
            }

            int maxSize = 0;
            var groupSizes = new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if (num2Prime.TryGetValue(num, out int prime))
                {
                    int parent = uf.GetParent(prime);
                    if (!groupSizes.TryGetValue(parent, out int size))
                    {
                        groupSizes.Add(parent, 0);
                    }

                    if (maxSize < ++groupSizes[parent])
                    {
                        maxSize = groupSizes[parent];
                    }
                }
            }

            return maxSize;
        }

        LinkedList<int> PrimeDecompose(int num)
        {
            LinkedList<int> primeFactors = new LinkedList<int>();
            int factor = 2;

            while (num >= factor * factor)
            {
                if (num % factor == 0)
                {
                    primeFactors.AddLast(factor);
                    while (num % factor == 0)
                        num = num / factor;
                }
                else
                {
                    factor += 1;
                }
            }
            if (num > 1)
                primeFactors.AddLast(num);

            return primeFactors;
        }

        LinkedList<int> PrimeDecomposeV1(int num)
        {
            LinkedList<int> primeFactors = new LinkedList<int>();
            int factor = 2;

            while (num >= factor * factor)
            {
                if (num % factor == 0)
                {
                    if (primeFactors.LastOrDefault() != factor)
                        primeFactors.AddLast(factor);
                    num = num / factor;
                }
                else
                {
                    factor += 1;
                }
            }
            primeFactors.AddLast(num);
            return primeFactors;
        }

        public int LargestComponentSizeV3(int[] nums)
        {
            int maxNum = nums.Max();
            var uf = new DisjointUnion(maxNum + 1);
            var primes = this.GetPrimeFactors(maxNum);
            var num2Prime = new Dictionary<int, int>(nums.Length);
            for (int i = 0; i < nums.Length; ++i)
            {
                for(int j = 0; j < primes.Count && primes[j] <= nums[i]; ++j)
                {
                    int prime = primes[j];
                    if (nums[i] % prime == 0)
                    {
                        if (!num2Prime.TryGetValue(nums[i], out int firstPrime))
                        {
                            firstPrime = prime;
                            num2Prime.Add(nums[i], prime);
                        }
                        else
                        {
                            uf.Union(firstPrime, prime);
                        }
                    }
                }
            }

            int maxSize = 0;
            var groupSizes = new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if (num2Prime.TryGetValue(num, out int prime))
                {
                    int parent = uf.GetParent(prime);
                    if (!groupSizes.TryGetValue(parent, out int size))
                    {
                        groupSizes.Add(parent, 0);
                    }

                    if (maxSize < ++groupSizes[parent])
                    {
                        maxSize = groupSizes[parent];
                    }
                }
            }

            return maxSize;
        }

        private List<int> GetPrimeFactors(int num)
        {
            HashSet<int> primes = new HashSet<int>();
            for(int i = 2; i <= num; ++i)
            {
                primes.Add(i);
            }

            int prime = 2;
            while (prime * prime <= num)
            {
                if (primes.Contains(prime))
                {
                    int p = prime * prime;
                    while (num >= p)
                    {
                        primes.Remove(p);
                        p += prime;
                    }
                }

                ++prime;
            }

            return new List<int>(primes);
        }

        public int LargestComponentSizeV2(int[] nums)
        {
            int maxNum = nums.Max();
            var uf = new DisjointUnion(maxNum + 1);
            for (int i = 0; i < nums.Length; ++i)
            {
                for (int factor = 2; factor < (int)Math.Sqrt(nums[i]) + 1; ++factor)
                {
                    if (nums[i] % factor == 0)
                    {
                        uf.Union(nums[i], factor);
                        uf.Union(nums[i], nums[i] / factor);
                    }
                }
            }

            int maxSize = 0;
            var groupSizes = new Dictionary<int, int>();
            foreach(var num in nums)
            {
                int parent = uf.GetParent(num);
                if (!groupSizes.TryGetValue(parent, out int size))
                {
                    groupSizes.Add(parent, 0);
                }

                if (maxSize < ++groupSizes[parent])
                {
                    maxSize = groupSizes[parent];
                }
            }

            return maxSize;
        }

        public int LargestComponentSizeV1(int[] nums)
        {
            // TLE
            var uf = new DisjointUnion(nums.Length);
            for(int i = 0; i < nums.Length; ++i)
            {
                for(int j = i + 1; j < nums.Length; ++j)
                {
                    int parent1 = uf.GetParent(i);
                    int parent2 = uf.GetParent(j);
                    if (parent1 != parent2 && this.ShareCommonFactor(nums[i], nums[j]))
                    {
                        uf.Union(i, j);
                    }
                }
            }

            int[] sizes = new int[nums.Length];
            for(int i = 0; i < nums.Length; ++i)
            {
                ++sizes[uf.GetParent(i)];
            }

            return sizes.Max();
        }

        private bool ShareCommonFactor(int num1, int num2)
        {
            while (true)
            {
                if (num1 == 1 || num2 == 1)
                    return false;

                if (num1 < num2)
                {
                    var n = num2;
                    num2 = num1;
                    num1 = n;
                }

                num1 = num1 % num2;
                if (num1 == 0 || (num1 % 2 == 0 && num2 % 2 == 0))
                    return true;
            }
        }
    }
}

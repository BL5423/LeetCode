using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class RandomizedSet
    {
        private const int SIZE = 200000 + 1;

        private Dictionary<int, int> dictionary;

        private int[] numbers = new int[SIZE];

        public RandomizedSet()
        {
            this.dictionary = new Dictionary<int, int>();
        }

        public bool Insert(int val)
        {
            if (!this.dictionary.TryGetValue(val, out int index))
            {
                index = this.dictionary.Count;
                this.dictionary.Add(val, index);
                this.numbers[index] = val;
                return true;
            }

            return false;
        }

        public bool Remove(int val)
        {
            if (this.dictionary.TryGetValue(val, out int index))
            {
                int count = this.dictionary.Count;
                if (count > 1)
                {
                    // use the last number to overwrite val, which is being deleted
                    this.numbers[index] = this.numbers[count - 1];
                    this.dictionary[this.numbers[count - 1]] = index;
                }

                this.dictionary.Remove(val);
                return true;
            }

            return false;
        }

        public int GetRandom()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int index = random.Next(0, this.dictionary.Count);
            return this.numbers[index];
        }
    }
}

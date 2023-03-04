using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class MyHashSet
    {
        private List<int> list;

        public MyHashSet()
        {
            this.list = new List<int>();
        }

        public void Add(int key)
        {
            int pos = this.list.BinarySearch(key);
            if (pos < 0)
            {
                pos = ~pos;
                this.list.Insert(pos, key);
            }
        }

        public void Remove(int key)
        {
            if (this.Contains(key))
            {
                this.list.Remove(key);
            }
        }

        public bool Contains(int key)
        {
            int pos = this.list.BinarySearch(key);
            return pos >= 0;
        }
    }
}

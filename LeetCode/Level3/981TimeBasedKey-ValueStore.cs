using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ConsoleApp2.Level3
{
    public class _981TimeBasedKey_ValueStore
    {
        public class TimeMap
        {
            const int BucketSize = 100;

            private Dictionary<string, List<(int, string[])>> key2Values;

            private MyComparer comparer = new MyComparer();

            public TimeMap()
            {
                this.key2Values = new Dictionary<string, List<(int, string[])>>();
            }

            public void Set(string key, string value, int timestamp)
            {
                int bucketId = timestamp / BucketSize;
                int index = timestamp % BucketSize;
                if (!this.key2Values.TryGetValue(key, out List<(int, string[])> buckets))
                {
                    buckets = new List<(int, string[])>();
                    this.key2Values.Add(key, buckets);
                }

                string[] values = null;
                var pos = buckets.BinarySearch((bucketId, null), this.comparer);
                if (pos < 0)
                {
                    pos = ~pos;
                    values = new string[BucketSize];
                    values[index] = value;
                    buckets.Insert(pos, (bucketId, values));
                }
                else
                {
                    values = buckets[pos].Item2;
                    values[index] = value;
                }
            }

            public string Get(string key, int timestamp)
            {
                if (!this.key2Values.TryGetValue(key, out List<(int, string[])> buckets))
                {
                    return string.Empty;
                }

                int bucketId = timestamp / BucketSize;
                int index = timestamp % BucketSize;
                var pos = buckets.BinarySearch((bucketId, null), this.comparer);
                if (pos < 0)
                {
                    if (bucketId == 0)
                        return string.Empty;

                    pos = ~pos;
                    return buckets[pos - 1].Item2.Last(val => !string.IsNullOrEmpty(val));
                }
                else
                {
                    var values = buckets[pos].Item2;
                    for (int i = index; i >= 0; --i)
                    {
                        if (values[i] != null)
                            return values[i];
                    }

                    return string.Empty;
                }
            }
        }

        private class MyComparer : IComparer<(int, string[])>
        {
            public int Compare((int, string[]) x, (int, string[]) y)
            {
                return x.Item1 - y.Item1;
            }
        }
    }
}

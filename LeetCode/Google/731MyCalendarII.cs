using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _731MyCalendarII
    {
        private LinkedList<(int, int)> singleBooked, doubleBooked;

        private SortedDictionary<int, int> calendar;

        public _731MyCalendarII()
        {
            this.calendar = new SortedDictionary<int, int>();
        }

        public bool Book(int start, int end)
        {
            if (!this.calendar.TryGetValue(start, out int startCount))
            {
                this.calendar.Add(start, 0);
            }
            ++this.calendar[start];

            if (!this.calendar.TryGetValue(end, out int endCount))
            {
                this.calendar.Add(end, 0);
            }
            --this.calendar[end];

            int occurs = 0;
            foreach (var kv in this.calendar)
            {
                occurs += kv.Value;
                if (occurs >= 3)
                {
                    if (--this.calendar[start] == 0)
                    {
                        this.calendar.Remove(start);
                    }

                    if (++this.calendar[end] == 0)
                    {
                        this.calendar.Remove(end);
                    }

                    return false;
                }
            }

            return true;
        }

        public bool BookV1(int start, int end)
        {
            foreach (var kv in this.doubleBooked)
            {
                if (HasIntersection(start, end, kv.Item1, kv.Item2))
                    return false;
            }

            foreach (var kv in this.singleBooked)
            {
                if (HasIntersection(start, end, kv.Item1, kv.Item2, out int newStart, out int newEnd))
                {
                    this.doubleBooked.AddLast((newStart, newEnd));
                }
            }

            this.singleBooked.AddLast((start, end));
            return true;
        }

        private static bool HasIntersection(int start1, int end1, int start2, int end2)
        {
            return !(start1 >= end2 || start2 >= end1);
        }

        private static bool HasIntersection(int start1, int end1, int start2, int end2, out int start, out int end)
        {
            start = -1;
            end = -1;
            if (HasIntersection(start1, end1, start2, end2))
            {
                start = Math.Max(start1, start2);
                end = Math.Min(end1, end2);
                return true;
            }

            return false;
        }
    }
}

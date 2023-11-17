using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    // https://leetcode.com/problems/merge-intervals/discuss/355318/Fully-Explained-and-Clean-Interval-Tree-for-Facebook-Follow-Up-No-Sorting
    public class IntervalNode
    {
        public IntervalNode left, right;

        public int start, end, middle;

        public IntervalNode(int start, int end)
        {
            this.start = start;
            this.end = end;
            this.middle = start + ((end - start) >> 1);
        }

        public static int[][] Merge(int[][] intervals)
        {
            if (intervals != null && intervals.Length > 0)
            {
                IntervalNode root = new IntervalNode(intervals[0][0], intervals[0][1]);
                for (int index = 1; index < intervals.Length; ++index)
                {
                    root.Add(intervals[index]);
                }

                var res = root.Query();
                return res.ToArray();
            }

            return null;
        }

        private void Add(int[] interval)
        {
            if (interval[0] > this.middle)
            {
                // add the right
                if (this.right != null)
                {
                    this.right.Add(interval);
                }
                else
                {
                    this.right = new IntervalNode(interval[0], interval[1]);
                }
            }
            else if (interval[1] < this.middle)
            {
                // add to left
                if (this.left != null)
                {
                    this.left.Add(interval);
                }
                else
                {
                    this.left = new IntervalNode(interval[0], interval[1]);
                }
            }
            else
            {
                // expand current node but do not update middle since it should be the consistent critiera for ALL intervals
                this.start = Math.Min(this.start, interval[0]);
                this.end = Math.Max(this.end, interval[1]);
            }
        }

        public IList<int[]> Query()
        {
            bool mergedCurNode = false;
            IList<int[]> res = new List<int[]>();
            if (this.left != null)
            {
                var leftIntervals = this.left.Query();
                foreach(var leftInterval in leftIntervals)
                {
                    if (leftInterval[1] < this.start)
                    {
                        res.Add(leftInterval);
                    }
                    else
                    {
                        res.Add(new int[] { Math.Min(leftInterval[0], this.start), this.end });
                        mergedCurNode = true;
                        break;
                    }
                }
            }

            if (!mergedCurNode)
            {
                res.Add(new int[] { this.start, this.end });
            }

            if (this.right != null)
            {
                var rightIntervals = this.right.Query();
                foreach(var rightInterval in rightIntervals)
                {
                    if (rightInterval[0] > this.end)
                    {
                        res.Add(rightInterval);
                    }
                    else
                    {
                        this.end = res.Last()[1] = Math.Max(rightInterval[1], this.end);
                    }
                }
            }

            return res;
        }
    }

    public class IntervalNodeV3
    {
        private int start, end, middle;

        private IntervalNodeV3 left, right;

        public IntervalNodeV3(int[] intervals)
        {
            this.start = intervals[0];
            this.end = intervals[1];
            this.middle = this.start + ((this.end - this.start) >> 1);
        }

        public static IntervalNodeV3 Merge(int[][] intervals)
        {
            IntervalNodeV3 node = null;
            if (intervals != null && intervals.Length > 0)
            {
                node = new IntervalNodeV3(intervals[0]);
                for (int i = 1; i < intervals.Length; ++i)
                {
                    if (!node.Insert(intervals[i]))
                        return null;
                }
            }

            return node;
        }

        private bool Insert(int[] interval)
        {
            if (interval != null)
            {
                if (interval[0] >= this.middle)
                {
                    if (this.right != null)
                    {
                        return this.right.Insert(interval);
                    }
                    else
                    {
                        this.right = new IntervalNodeV3(interval);
                    }
                }
                else if (interval[1] <= this.middle)
                {
                    if (this.left != null)
                    {
                        return this.left.Insert(interval);
                    }
                    else
                    {
                        this.left = new IntervalNodeV3(interval);
                    }
                }
                else
                {
                    this.start = Math.Min(this.start, interval[0]);
                    this.end = Math.Max(this.end, interval[1]);
                    return false;
                }
            }

            return true;
        }

        public List<int[]> Query()
        {
            bool insertNode = false;
            var res = new List<int[]>();

            if (this.left != null)
            {
                var leftIntervals = this.left.Query();
                foreach (var leftInterval in leftIntervals)
                {
                    if (leftInterval[1] <= this.start)
                    {
                        res.Add(leftInterval);
                    }
                    else
                    {
                        insertNode = true;
                        res.Add(new int[] { Math.Min(leftInterval[0], this.start), this.end });
                        break;
                    }
                }
            }

            if (!insertNode)
            {
                res.Add(new int[] { this.start, this.end });
            }

            if (this.right != null)
            {
                var rightIntervals = this.right.Query();
                foreach (var rightInterval in rightIntervals)
                {
                    if (rightInterval[0] >= this.end)
                    {
                        res.Add(rightInterval);
                    }
                    else
                    {
                        this.end = res.Last()[1] = Math.Max(this.end, rightInterval[1]);
                    }
                }
            }

            return res;
        }
    }


    public class MergeIntervals
    {

        public int[][] Merge(int[][] intervals)
        {
            int firstIndex = int.MaxValue, lastIndex = int.MinValue;
            int[] range = new int[10002];
            foreach (var interval in intervals)
            {
                range[interval[0] + 1] = Math.Max(range[interval[0] + 1], interval[1] + 1);

                if (interval[0] + 1 < firstIndex)
                    firstIndex = interval[0] + 1;
                if (interval[1] + 1 > lastIndex)
                    lastIndex = interval[1] + 1;
            }

            int index = firstIndex;
            var list = new List<int[]>();
            while (index <= lastIndex)
            {
                if (range[index] == 0)
                {
                    ++index;
                    continue;
                }

                int nextIndex = range[index];
                for (int i = index; i <= nextIndex; ++i)
                {
                    if (range[i] > nextIndex)
                        nextIndex = range[i];
                }

                list.Add(new int[] { index - 1, nextIndex - 1 });
                index = nextIndex + 1;
            }

            return list.ToArray();
        }

        public int[][] MergeV2(int[][] intervals)
        {
            return IntervalNodeV2.Merge(intervals);
        }

        public bool CanAttendMeetings(int[][] intervals)
        {
            if (intervals == null || intervals.Length <= 1)
                return true;

            var intervalNode = IntervalNodeV3.Merge(intervals);
            if (intervalNode == null)
                return false;
            var mergedIntervals = intervalNode.Query();
            return mergedIntervals != null && mergedIntervals.Count == intervals.Length;
        }

        public int[][] Merge_Sort(int[][] intervals)
        {
            var res = new List<int[]>();
            if (intervals != null && intervals.Length >= 1)
            {
                Array.Sort(intervals, (interval1, interval2) => interval1[0] - interval2[0]);
                var prevInterval = intervals[0];
                for(int index = 1; index < intervals.Length; ++index)
                {
                    var curInterval = intervals[index];
                    if (prevInterval[1] < curInterval[0])
                    {
                        res.Add(prevInterval);
                        prevInterval = curInterval;
                    }
                    else
                    {
                        prevInterval[1] = Math.Max(prevInterval[1], curInterval[1]);
                    }
                }

                res.Add(prevInterval);
            }

            return res.ToArray();
        }

        public int[][] MergeV1(int[][] intervals)
        {
            int maxEnd = 0, minStart = 10000;
            int[] linearIntervals = Enumerable.Repeat(-1, minStart).ToArray();
            foreach (var interval in intervals)
            {
                int start = interval[0];
                if (start < minStart)
                {
                    minStart = start;
                }
                int end = interval[1];
                if (end > maxEnd)
                {
                    maxEnd = end;
                }

                if (linearIntervals[start] == -1)
                {
                    linearIntervals[start] = end;
                }
                else if (linearIntervals[start] < end)
                {
                    // keep the longer interval
                    linearIntervals[start] = end;
                }
            }

            List<int[]> finalIntervals = new List<int[]>();
            int currentStart = -1, currentEnd = -1;
            int index = minStart;
            while (index <= maxEnd)
            {
                int start = index;
                int end = linearIntervals[index];
                if (end != -1)
                {
                    if (currentStart == -1)
                    {
                        currentStart = start;
                        currentEnd = end;
                    }

                    // no overlap
                    else if (currentEnd < start)
                    {
                        // output current interval as an independent interval
                        finalIntervals.Add(new int[] { currentStart, currentEnd });

                        // reset
                        currentStart = start;
                        currentEnd = end;
                    }
                    // otherwise with overlap
                    else
                    {
                        // merge
                        currentEnd = Math.Max(currentEnd, end);
                    }
                }

                ++index;
            }

            if (currentStart != -1)
            {
                finalIntervals.Add(new int[] { currentStart, currentEnd });
            }

            return finalIntervals.ToArray();
        }
    }

    public class IntervalNodeV2
    {
        public IntervalNodeV2 left, right;

        public int start, end;

        public IntervalNodeV2(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public static int[][] Merge(int[][] intervals)
        {
            if (intervals != null && intervals.Length > 0)
            {
                IntervalNodeV2 root = new IntervalNodeV2(intervals[0][0], intervals[0][1]);
                for (int index = 1; index < intervals.Length; ++index)
                {
                    root.Add(intervals[index]);
                }

                var res = root.Query();
                return res.ToArray();
            }

            return null;
        }

        private void Add(int[] interval)
        {
            if (interval[0] > this.end)
            {
                // add the right
                if (this.right != null)
                {
                    this.right.Add(interval);
                }
                else
                {
                    this.right = new IntervalNodeV2(interval[0], interval[1]);
                }
            }
            else if (interval[1] < this.start)
            {
                // add to left
                if (this.left != null)
                {
                    this.left.Add(interval);
                }
                else
                {
                    this.left = new IntervalNodeV2(interval[0], interval[1]);
                }
            }
            else
            {
                // expand current node
                this.start = Math.Min(this.start, interval[0]);
                this.end = Math.Max(this.end, interval[1]);
            }
        }

        public IList<int[]> Query()
        {
            bool mergedCurNode = false;
            IList<int[]> res = new List<int[]>();
            if (this.left != null)
            {
                var leftIntervals = this.left.Query();
                foreach (var leftInterval in leftIntervals)
                {
                    if (leftInterval[1] < this.start)
                    {
                        res.Add(leftInterval);
                    }
                    else
                    {
                        res.Add(new int[] { Math.Min(leftInterval[0], this.start), this.end });
                        mergedCurNode = true;
                        break;
                    }
                }
            }

            if (!mergedCurNode)
            {
                res.Add(new int[] { this.start, this.end });
            }

            if (this.right != null)
            {
                var rightIntervals = this.right.Query();
                foreach (var rightInterval in rightIntervals)
                {
                    if (rightInterval[0] > this.end)
                    {
                        res.Add(rightInterval);
                    }
                    else
                    {
                        this.end = res.Last()[1] = Math.Max(rightInterval[1], this.end);
                    }
                }
            }

            return res;
        }
    }
}

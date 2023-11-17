using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Top100Medium
{
    public class _253MeetingRoomsII
    {
        public int MinMeetingRooms(int[][] intervals)
        {
            int[] startTimes = new int[intervals.Length];
            int[] endTimes = new int[intervals.Length];
            for(int i =0; i < intervals.Length; ++i)
            {
                startTimes[i] = intervals[i][0];
                endTimes[i] = intervals[i][1];
            }
            Array.Sort(startTimes);
            Array.Sort(endTimes);

            int startIndex = 0, endIndex = 0, rooms = 0;
            while (startIndex < intervals.Length)
            {
                if (startTimes[startIndex] >= endTimes[endIndex])
                {
                    // we can reused a room where a meeting has just ended
                    --rooms;
                }

                // no matter what, let's assume we need a meeting room
                ++rooms;
                ++startIndex;
            }

            return rooms;
        }

        public int MinMeetingRoomsV2(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[0] - b[0]);
            var endTimes = new PriorityQueue<int, int>();
            int rooms = 1;
            foreach(var interval in intervals)
            {
                // if the room with a meeting that ends at earilest time can not fit the pending meeting then we need an extra room
                if (endTimes.Count > 0)
                {
                    if (endTimes.Peek() > interval[0])
                    {
                        ++rooms;
                    }
                    else
                    {
                        endTimes.Dequeue();
                    }
                }

                endTimes.Enqueue(interval[1], interval[1]);
            }

            return rooms;
        }

        public int MinMeetingRoomsV1(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[0] - b[0]);
            LinkedList<int[]> rooms = new LinkedList<int[]>();
            rooms.AddLast(intervals[0]);
            for (int i = 1; i < intervals.Length; ++i)
            {
                bool booked = false;
                foreach (var room in rooms)
                {
                    if (room[1] <= intervals[i][0] || room[0] >= intervals[i][1])
                    {
                        // no overlap then book
                        room[0] = intervals[i][0];
                        room[1] = intervals[i][1];
                        booked = true;
                        break;
                    }
                }

                if (!booked)
                {
                    rooms.AddLast(intervals[i]);
                }
            }

            return rooms.Count;
        }
    }
}

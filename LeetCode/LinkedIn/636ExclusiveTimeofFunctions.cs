using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.LinkedIn
{
    public class _636ExclusiveTimeofFunctions
    {
        public int[] ExclusiveTime(int n, IList<string> logs)
        {
            int[] times = new int[n];
            Stack<RunningFunction> stack = new Stack<RunningFunction>();
            foreach (var log in logs)
            {
                var splits = log.Split(":");
                int id = int.Parse(splits[0]);
                int timestamp = int.Parse(splits[2]);
                if (splits[1] == "start")
                {
                    stack.Push(new RunningFunction(id, timestamp));
                }
                else // "end"
                {
                    var currentFunc = stack.Pop();
                    times[currentFunc.id] += timestamp - currentFunc.timestamp + 1;

                    if (stack.Count != 0)
                    {
                        var prevFunc = stack.Peek();
                        times[prevFunc.id] += currentFunc.starttime - prevFunc.timestamp;
                        prevFunc.timestamp = timestamp + 1; // resume
                    }
                }
            }

            return times;
        }

        public int[] ExclusiveTimeV1(int n, IList<string> logs)
        {
            int[] times = new int[n];
            Stack<RunningFunction> stack = new Stack<RunningFunction>();
            foreach (var log in logs)
            {
                var splits = log.Split(":");
                int id = int.Parse(splits[0]);
                int timestamp = int.Parse(splits[2]);
                if (splits[1] == "start")
                {
                    if (stack.Count != 0)
                    {
                        var currentFunc = stack.Peek();
                        times[currentFunc.id] += timestamp - currentFunc.timestamp;
                    }

                    stack.Push(new RunningFunction(id, timestamp));
                }
                else // "end"
                {
                    var currentFunc = stack.Pop();
                    times[currentFunc.id] += timestamp - currentFunc.timestamp + 1;

                    if (stack.Count != 0)
                    {
                        var prevFunc = stack.Peek();
                        prevFunc.timestamp = timestamp + 1; // resume
                    }
                }
            }

            return times;
        }
    }

    public class RunningFunction
    {
        public int id, timestamp, starttime;

        public RunningFunction(int id, int timestamp)
        {
            this.id = id;
            this.timestamp = timestamp;
            this.starttime = timestamp;
        }
    }
}

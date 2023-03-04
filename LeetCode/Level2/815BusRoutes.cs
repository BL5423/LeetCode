using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _815BusRoutes
    {
        public int NumBusesToDestination(int[][] routes, int sourceStop, int targetStop)
        {
            if (sourceStop == targetStop)
                return 0;

            Dictionary<int, HashSet<int>> stop2Buses = new Dictionary<int, HashSet<int>>(routes.Length);
            for(int bus = 0; bus < routes.Length; ++bus)
            {
                foreach (var stop in routes[bus])
                {
                    if (!stop2Buses.TryGetValue(stop, out HashSet<int> buses))
                    {
                        buses = new HashSet<int>();
                        stop2Buses.Add(stop, buses);
                    }

                    buses.Add(bus);
                }
            }

            Dictionary<int, HashSet<int>> bus2Buses = new Dictionary<int, HashSet<int>>(routes.Length);
            for (int bus = 0; bus < routes.Length; ++bus)
            {
                var adjBuses = new HashSet<int>();
                bus2Buses.Add(bus, adjBuses);
                foreach (var stop in routes[bus])
                {
                    if (stop2Buses.TryGetValue(stop, out HashSet<int> buses))
                    {
                        foreach(var adjBus in buses)
                        {
                            if (adjBus != bus)
                            {
                                adjBuses.Add(adjBus);
                            }
                        }
                    }
                }
            }

            if (!stop2Buses.TryGetValue(sourceStop, out HashSet<int> startBuses))
            {
                return -1;
            }
            if (!stop2Buses.TryGetValue(targetStop, out HashSet<int> endBuses))
            {
                return -1;
            }
            Queue<int> queue = new Queue<int>();
            bool[] visitedBuses = new bool[routes.Length];
            foreach(var startBus in startBuses)
            {
                queue.Enqueue(startBus);
                visitedBuses[startBus] = true;
            }

            int exchanges = 0;
            while (queue.Count != 0)
            {
                ++exchanges;
                int count = queue.Count;
                for (int c = 0; c < count; ++c)
                {
                    var bus = queue.Dequeue();
                    if (endBuses.Contains(bus))
                        return exchanges;

                    if (bus2Buses.TryGetValue(bus, out HashSet<int> adjBuses))
                    {
                        foreach (var adjBus in adjBuses)
                        {
                            if (!visitedBuses[adjBus])
                            {
                                visitedBuses[adjBus] = true;
                                queue.Enqueue(adjBus);
                            }
                        }
                    }
                }
            }

            return -1;
        }

        public int NumBusesToDestinationV1(int[][] routes, int source, int target)
        {
            if (source == target)
                return 0;

            Dictionary<int, IList<int>> stopToBuses = new Dictionary<int, IList<int>>();
            for(int bus = 0; bus < routes.Length; ++bus)
            {
                int[] route = routes[bus];
                foreach(var stop in route)
                {
                    if (!stopToBuses.TryGetValue(stop, out IList<int> buses))
                    {
                        buses = new List<int>();
                        stopToBuses.Add(stop, buses);
                    }
                    buses.Add(bus);
                }
            }

            int exchanges = int.MaxValue;
            IList<int> startBuses = stopToBuses[source];
            foreach(var bus in startBuses)
            {
                exchanges = Math.Min(exchanges, BFS(bus, stopToBuses, routes, target));
            }

            return exchanges != int.MaxValue ? exchanges : -1;
        }

        private int BFS(int busToStart, IDictionary<int, IList<int>> stopToBuses, int[][] busToStops, int target)
        {
            int exchanges = 0;
            bool found = false;
            var visitedBuses = new HashSet<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(busToStart);
            visitedBuses.Add(busToStart);
            int exchange = 0;
            while (queue.Count > 0 && !found)
            {
                ++exchange;
                int count = queue.Count;
                for (int c = 0; c < count; ++c)
                {
                    int bus = queue.Dequeue();
                    int[] stops = busToStops[bus];
                    if (stops.Contains(target))
                    {
                        // found target
                        found = true;
                        exchanges = exchange;
                        break;
                    }
                    else
                    {
                        foreach (var stop in stops)
                        {
                            if (stopToBuses.TryGetValue(stop, out IList<int> nextBuses))
                            {
                                foreach (var nextBus in nextBuses)
                                {
                                    if (!visitedBuses.Contains(nextBus))
                                    {
                                        queue.Enqueue(nextBus);
                                        visitedBuses.Add(nextBus);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return found ? exchanges : int.MaxValue;
        }

        private int BFSv1(int busToStart, IDictionary<int, IList<int>> stopToBuses, int[][] busToStops, int target)
        {
            int exchanges = 0;
            bool found = false;
            var visitedBuses = new HashSet<int>();
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((busToStart, 1));
            visitedBuses.Add(busToStart);
            while (queue.Count > 0)
            {
                var head = queue.Dequeue();
                int bus = head.Item1;
                int exchange = head.Item2;
                int[] stops = busToStops[bus];
                if (stops.Contains(target))
                {
                    // found target
                    found = true;
                    exchanges = exchange;
                    break;
                }
                else
                {
                    foreach(var stop in stops)
                    {
                        if (stopToBuses.TryGetValue(stop, out IList<int> nextBuses))
                        {
                            foreach(var nextBus in nextBuses)
                            {
                                if (!visitedBuses.Contains(nextBus))
                                {
                                    queue.Enqueue((nextBus, exchange + 1));
                                    visitedBuses.Add(nextBus);
                                }
                            }
                        }
                    }
                }
            }

            return found ? exchanges : int.MaxValue;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class _787CheapestFlightsWithinKStops
    {
        const int MaxPrice = int.MaxValue;

        public int FindCheapestPrice_BellMan_FordV2(int n, int[][] flights, int src, int dst, int k)
        {
            var edges = new Dictionary<int, LinkedList<(int, int)>>(flights.Length);
            foreach (var flight in flights)
            {
                if (!edges.TryGetValue(flight[0], out LinkedList<(int, int)> dsts))
                {
                    dsts = new LinkedList<(int, int)>();
                    edges.Add(flight[0], dsts);
                }
                dsts.AddLast((flight[1], flight[2]));
            }

            int[] prevCost = new int[n];
            int[] curCost = new int[n];
            for (int i = 0; i < n; ++i)
            {
                prevCost[i] = MaxPrice;
                curCost[i] = MaxPrice;
            }
            prevCost[src] = 0; // no cost from src to src
            curCost[src] = 0;

            for (int t = 0; t <= k; ++t)
            {
                for (int i = 0; i < n; ++i)
                {
                    if (edges.TryGetValue(i, out LinkedList<(int, int)> dsts))
                    {
                        foreach (var edge in dsts)
                        {
                            int dest = edge.Item1;
                            int cost = edge.Item2;// cost from i to dest

                            if (prevCost[i] != MaxPrice)
                            {
                                curCost[dest] = Math.Min(curCost[dest], prevCost[i] + cost);
                            }
                        }
                    }
                }

                var temp = prevCost;
                prevCost = curCost;
                curCost = temp;
                for (int i = 0; i < n; ++i)
                    curCost[i] = prevCost[i];
            }

            return prevCost[dst] != MaxPrice ? prevCost[dst] : -1;
        }

        public int FindCheapestPrice_BellMan_FordV1(int n, int[][] flights, int src, int dst, int k)
        {
            var edges = new Dictionary<int, LinkedList<(int, int)>>(flights.Length);
            foreach(var flight in flights)
            {
                if (!edges.TryGetValue(flight[0], out LinkedList<(int, int)> dsts))
                {
                    dsts = new LinkedList<(int, int)>();
                    edges.Add(flight[0], dsts);
                }
                dsts.AddLast((flight[1], flight[2]));
            }

            int[] prevCost = new int[n];
            int[] curCost = new int[n];
            for(int i = 0; i < n; ++i)
            {
                prevCost[i] = MaxPrice;
                curCost[i] = MaxPrice;
            }
            prevCost[src] = 0; // no cost from src to src
            curCost[src] = 0;

            for(int t = 0; t<= k; ++t)
            {
                for(int i = 0; i < n; ++i)
                {
                    if (edges.TryGetValue(i, out LinkedList<(int, int)> dsts))
                    {
                        foreach (var edge in dsts)
                        {
                            int dest = edge.Item1;
                            int cost = edge.Item2;// cost from i to dest

                            int potenialBetterCost = MaxPrice;
                            if (prevCost[i] != MaxPrice)
                            {
                                potenialBetterCost = Math.Min(prevCost[dest], prevCost[i] + cost);
                            }

                            curCost[dest] = Math.Min(curCost[dest], potenialBetterCost);                            
                        }
                    }
                }

                var temp = prevCost;
                prevCost = curCost;
                curCost = temp;
            }

            return prevCost[dst] != MaxPrice ? prevCost[dst] : -1;
        }

        public int FindCheapestPrice_BFS(int n, int[][] flights, int src, int dst, int k)
        {
            var edges = new Dictionary<int, LinkedList<(int, int)>>(flights.Length);
            foreach (var flight in flights)
            {
                if (!edges.TryGetValue(flight[0], out LinkedList<(int, int)> dsts))
                {
                    dsts = new LinkedList<(int, int)>();
                    edges.Add(flight[0], dsts);
                }
                dsts.AddLast((flight[1], flight[2]));
            }

            int minCost = int.MaxValue;
            //     city, cost, stops
            Queue<(int, int, int)> queue = new Queue<(int, int, int)>(n);
            queue.Enqueue((src, 0, 0));
            int[] curCosts = new int[n];
            for (int i = 0; i < n; ++i)
                curCosts[i] = int.MaxValue;
            curCosts[src] = 0;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                int city = node.Item1;
                int curCost = node.Item2;
                int stops = node.Item3;
                if (city == dst)
                {
                    if (curCost < minCost)
                        minCost = curCost;

                    continue;
                }                

                if (edges.TryGetValue(city, out LinkedList<(int, int)> dsts))
                {
                    foreach(var flight in dsts)
                    {
                        int dest = flight.Item1;
                        int cost = flight.Item2;
                        int nextCost = cost + curCost;
                        if (nextCost < minCost && stops < k + 1 && curCosts[dest] > nextCost)
                        {
                            queue.Enqueue((dest, nextCost, stops + 1));
                            curCosts[dest] = nextCost;
                        }
                    }
                }
            }

            return minCost != int.MaxValue ? minCost : -1;
        }

        public int FindCheapestPrice_BFSV2(int n, int[][] flights, int src, int dst, int k)
        {
            var edges = new Dictionary<int, LinkedList<(int, int)>>(flights.Length);
            foreach (var flight in flights)
            {
                if (!edges.TryGetValue(flight[0], out LinkedList<(int, int)> dsts))
                {
                    dsts = new LinkedList<(int, int)>();
                    edges.Add(flight[0], dsts);
                }
                dsts.AddLast((flight[1], flight[2]));
            }

            int minCost = int.MaxValue;
            //     city,cost
            Queue<(int, int)> queue = new Queue<(int, int)>(n);
            queue.Enqueue((src, 0));
            int[] curCosts = new int[n];
            for (int i = 0; i < n; ++i)
                curCosts[i] = int.MaxValue;
            curCosts[src] = 0;

            int stops = -1;
            while (queue.Count > 0)
            {
                int count = queue.Count;
                for (int i = 0; i < count; ++i)
                {
                    var node = queue.Dequeue();
                    int city = node.Item1;
                    int curCost = node.Item2;
                    if (city == dst)
                    {
                        if (curCost < minCost)
                            minCost = curCost;

                        continue;
                    }

                    if (edges.TryGetValue(city, out LinkedList<(int, int)> dsts))
                    {
                        foreach (var flight in dsts)
                        {
                            int dest = flight.Item1;
                            int cost = flight.Item2;
                            int nextCost = cost + curCost;
                            if (nextCost < minCost && curCosts[dest] > nextCost)
                            {
                                queue.Enqueue((dest, nextCost));
                                curCosts[dest] = nextCost;
                            }
                        }
                    }
                }

                if (++stops >= k + 1)
                    break;
            }

            return minCost != int.MaxValue ? minCost : -1;
        }

        public int FindCheapestPrice_Dijkstra(int n, int[][] flights, int src, int dst, int k)
        {
            var edges = new Dictionary<int, LinkedList<(int, int)>>(flights.Length);
            foreach (var flight in flights)
            {
                if (!edges.TryGetValue(flight[0], out LinkedList<(int, int)> dsts))
                {
                    dsts = new LinkedList<(int, int)>();
                    edges.Add(flight[0], dsts);
                }
                dsts.AddLast((flight[1], flight[2]));
            }

            int[] curStops = new int[n];
            for(int i = 0; i < n; ++i)
            {
                curStops[i] = int.MaxValue;
            }
            curStops[src] = 0;

            MinHeap<Stop> minHeap = new MinHeap<Stop>(n * n);
            minHeap.Push(new Stop(src, 0, 0));
            while (minHeap.Size() > 0)
            {
                var city = minHeap.Pop();

                // dest is the next closest node to src
                int dest = city.dest;
                int price = city.price;
                int stops = city.stops;

                if (dest == dst)
                    return price;

                // There would be multiple paths from src to dest, the fist one would be the shortest
                // and also least expensive(since Dijkstra is greedy algothrim) path
                // So, if dest was reached before or the current path exceeds k stops, then skip dest this time
                if (stops > curStops[dest] || stops > k + 1)
                    continue;
                curStops[dest] = stops;

                if (edges.TryGetValue(dest, out LinkedList<(int, int)> dsts))
                {
                    foreach(var edge in dsts)
                    {
                        int nextCity = edge.Item1;
                        int cost = edge.Item2;
                        // do not proceed if the # of stops(of next dest) exceeds k
                        if (stops + 1 <= k + 1)
                        {
                            minHeap.Push(new Stop(nextCity, price + cost, stops + 1));
                        }
                    }
                }
            }

            return -1;
        }
    }

    public class Stop : IComparable<Stop>
    {
        public int dest, price, stops;

        public Stop(int dest, int price, int stops)
        {
            this.dest = dest;
            this.price = price;
            this.stops = stops;
        }

        public int CompareTo(Stop other)
        {
            return this.price - other.price;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp2.Graph
{
    public class _332ReconstructItinerary
    {
        public IList<string> FindItinerary_PostOrder(IList<IList<string>> tickets)
        {
            Dictionary<string, List<string>> v2e = new Dictionary<string, List<string>>(tickets.Count);
            foreach (var ticket in tickets)
            {
                var from = ticket[0];
                var to = ticket[1];

                if (!v2e.TryGetValue(from, out List<string> edges))
                {
                    edges = new List<string>();
                    v2e.Add(from, edges);
                }

                edges.Add(to);
            }

            foreach (var pair in v2e)
            {
                pair.Value.Sort();
            }

            string source = "JFK";
            var path = new LinkedList<string>();
            this.DFS_Iteratively(source, path, v2e);

            return path.ToList();
        }

        private void DFS_Iteratively(string source, LinkedList<string> path, Dictionary<string, List<string>> v2e)
        {
            Stack<string> stack = new Stack<string>();
            stack.Push(source);
            while (stack.Count != 0)
            {
                var city = stack.Peek();
                if (v2e.TryGetValue(city, out List<string> adjCities) && adjCities.Count != 0)
                {
                    string adjCity = adjCities.First();
                    adjCities.RemoveAt(0);
                    stack.Push(adjCity);
                }
                else
                {
                    path.AddFirst(city);
                    stack.Pop();
                }
            }
        }

        private void DFS_Recursively(string source, LinkedList<string> path, Dictionary<string, List<string>> v2e)
        {
            if (v2e.TryGetValue(source, out List<string> adjCities))
            {
                while (adjCities.Count != 0)
                {
                    var adjCity = adjCities.First();
                    adjCities.RemoveAt(0);
                    this.DFS_Recursively(adjCity, path, v2e);
                }
            }

            path.AddFirst(source);
        }

        public IList<string> FindItineraryV1(IList<IList<string>> tickets)
        {
            Dictionary<string, List<string>> v2e = new Dictionary<string, List<string>>(tickets.Count);
            foreach(var ticket in tickets)
            {
                var from = ticket[0];
                var to = ticket[1];

                if (!v2e.TryGetValue(from, out List<string> edges))
                {
                    edges = new List<string>();
                    v2e.Add(from, edges);
                }

                edges.Add(to);

                if (!v2e.TryGetValue(to, out List<string> edges2))
                {
                    edges2 = new List<string>();
                    v2e.Add(to, edges2);
                }
            }

            foreach (var pair in v2e)
            {
                //   pair.Value.Sort();
                pair.Value.Sort((a, b) =>
                {
                    // if a is the destination, then it should be the end of the list
                    if (v2e[a].Count == 0)
                        return 1;

                    // if b is the destination, then it should be the end of the list
                    if (v2e[b].Count == 0)
                        return -1;

                    return a.CompareTo(b);
                });
            }

            HashSet<string> visitedFlights = new HashSet<string>(tickets.Count);
            string source = "JFK";
            Stack<Stop> stack = new Stack<Stop>(tickets.Count);
            stack.Push(new Stop(source));

            while (stack.Count != 0)
            {
                if (visitedFlights.Count == tickets.Count)
                {
                    var res = new List<string>(stack.Select(item => item.city));
                    res.Reverse();
                    return res;
                }

                var stop = stack.Peek();
                var adjIndex = stop.adjIndex;
                if (stop.adjIndex < v2e[stop.city].Count)
                {
                    var nextCity = v2e[stop.city][stop.adjIndex];
                    var flight = string.Concat(stop.city, nextCity, stop.adjIndex++);
                    if (visitedFlights.Add(flight))
                    {
                        var nextStop = new Stop(nextCity);
                        nextStop.flightsTaken.AddLast(flight);
                        stack.Push(nextStop);
                    }
                }
                else
                {
                    stack.Pop();
                    foreach(var flightTaken in stop.flightsTaken)
                    {
                        visitedFlights.Remove(flightTaken);
                    }
                }
            }

            return null;
        }
    }

    public class Stop
    {
        public string city;

        public int adjIndex;

        public LinkedList<string> flightsTaken;

        public Stop(string city)
        {
            this.city = city;
            this.adjIndex = 0;
            this.flightsTaken = new LinkedList<string>();
        }
    }
}

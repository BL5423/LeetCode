using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _797AllPathsFromSourcetoTarget
    {
        public IList<IList<int>> AllPathsSourceTarget_BFS(int[][] graph)
        {
            int source = 0, target = graph.Length - 1;
            Queue<LinkedList<int>> queue = new Queue<LinkedList<int>>(graph.Length);
            var path = new LinkedList<int>();
            path.AddLast(source);
            queue.Enqueue(path);

            var res = new List<IList<int>>();
            while (queue.Count != 0)
            {
                var curPath = queue.Dequeue();
                var node = curPath.Last();
                if (node == target)
                {
                    res.Add(curPath.ToList());
                }
                else
                {
                    foreach (var adjNode in graph[node])
                    {
                        var newPath = new LinkedList<int>(curPath);
                        newPath.AddLast(adjNode);
                        queue.Enqueue(newPath);
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> AllPathsSourceTarget_DFS_Iteratively(int[][] graph)
        {
            var res = new List<IList<int>>();
            bool[] visited = new bool[graph.Length];
            Stack<(int, IEnumerator)> stack = new Stack<(int, IEnumerator)>(graph.Length);
            int source = 0, target = graph.Length - 1;
            stack.Push((source, graph[source].GetEnumerator()));
            visited[source] = true;

            while (stack.Count != 0)
            {
                var top = stack.Peek();
                var node = top.Item1;

                if (node == target)
                {
                    var path = new List<int>(stack.Select(i => i.Item1));
                    path.Reverse();
                    res.Add(path);

                    stack.Pop();
                    visited[node] = false;
                }
                else
                {
                    var enumerator = top.Item2;
                    if (enumerator.MoveNext())
                    {
                        var adjNode = (int)enumerator.Current;
                        if (visited[adjNode] == false)
                        {
                            stack.Push((adjNode, graph[adjNode].GetEnumerator()));
                            visited[adjNode] = true;
                        }
                    }
                    else
                    {
                        stack.Pop();
                        visited[node] = false;
                    }
                }
            }

            return res;
        }

        public IList<IList<int>> AllPathsSourceTarget_DFS_Recursively(int[][] graph)
        {
            var res = new List<IList<int>>();
            bool[] visited = new bool[graph.Length];
            this.DFS(0, graph.Length - 1, graph, res, new LinkedList<int>(), visited);

            return res;
        }

        private void DFS(int source, int target, int[][] graph, IList<IList<int>> res, LinkedList<int> path, bool[] visited)
        {
            path.AddLast(source);

            if (source == target)
            {
                res.Add(path.ToList());
            }
            else
            {
                visited[source] = true;
                
                foreach(var next in graph[source])
                {
                    if (visited[next] == false)
                    {
                        this.DFS(next, target, graph, res, path, visited);
                    }
                }

                visited[source] = false;
            }

            path.RemoveLast();
        }
    }
}

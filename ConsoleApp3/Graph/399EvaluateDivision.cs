using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Graph
{
    public class _399EvaluateDivision
    {
        public double[] CalcEquation_BFS(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            var v2e = new Dictionary<string, LinkedList<(string, double)>>();
            for(int i = 0; i < equations.Count; i++) 
            {
                var num1 = equations[i][0];
                var num2 = equations[i][1];
                var value = values[i];

                if (!v2e.TryGetValue(num1, out var edges1)) 
                {
                    edges1 = new LinkedList<(string, double)>();
                    v2e.Add(num1, edges1);
                }
                edges1.AddLast((num2, value));

                if (!v2e.TryGetValue(num2, out var edges2))
                {
                    edges2 = new LinkedList<(string, double)>();
                    v2e.Add(num2, edges2);
                }
                edges2.AddLast((num1, 1.0 / value));
            }

            double[] res = new double[queries.Count];
            Queue<(string, double)> queue = new Queue<(string, double)>();
            HashSet<string> seen = new HashSet<string>();
            for(int j = 0; j < queries.Count; ++j)
            {
                var num1 = queries[j][0];
                var num2 = queries[j][1];
                if (!v2e.ContainsKey(num1) || !v2e.ContainsKey(num2))
                    res[j] = -1;
                else if (num1 == num2)
                    res[j] = 1;
                else
                {
                    res[j] = -1;
                    queue.Enqueue((num1, 1));
                    while (queue.Count != 0)
                    {
                        var item = queue.Dequeue();
                        var num = item.Item1;
                        var val = item.Item2;
                        if (seen.Contains(num))
                            continue;

                        seen.Add(num);
                        if (num == num2)
                        {
                            res[j] = val;
                            break;
                        }
                        else
                        {
                            if (v2e.TryGetValue(num, out LinkedList<(string, double)> adjNums))
                            {
                                foreach (var adjNum in adjNums)
                                {
                                    if (!seen.Contains(adjNum.Item1))
                                    {
                                        queue.Enqueue((adjNum.Item1, val * adjNum.Item2));
                                    }
                                }
                            }
                        }
                    }

                    queue.Clear();
                    seen.Clear();
                }
            }

            return res;
        }

        public double[] CalcEquation_UF(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            var uf = new UnionFindWithWeight();
            for(int i = 0; i < equations.Count; ++i)
            {
                var equation = equations[i];
                uf.Union(equation[0], equation[1], values[i]);
            }

            var res = new double[queries.Count];
            for (int j = 0; j < queries.Count; ++j)
            {
                var query = queries[j];
                if (!uf.HasWeight(query[0]) || !uf.HasWeight(query[1]))
                    res[j] = -1;
                else
                {
                    var weight1 = uf.GetWeight(query[0]);
                    var weight2 = uf.GetWeight(query[1]);
                    if (weight1.Item1 == weight2.Item1)
                        res[j] = weight1.Item2 / weight2.Item2;
                    else
                        res[j] = -1;
                }
            }

            return res;
        }
    }

    public class UnionFindWithWeight
    {
        //                         (group root Id, weight to root)
        private Dictionary<string, (string, double)> weights;

        public UnionFindWithWeight()
        {
            this.weights = new Dictionary<string, (string, double)>();
        }

        public bool HasWeight(string key)
        {
            return this.weights.ContainsKey(key);
        }

        public (string, double) GetWeight(string key)
        {
            if (!this.weights.TryGetValue(key, out (string, double) weight))
            {
                weight = (key, 1.0);
                this.weights.Add(key, weight);
            }

            // chain update
            if (key != weight.Item1)
            {
                var rootWeigth = this.GetWeight(weight.Item1);
                this.weights[key] = (rootWeigth.Item1, weight.Item2 * rootWeigth.Item2);
            }

            return this.weights[key];
        }

        public void Union(string key1, string key2, double value)
        {
            var weight1 = this.GetWeight(key1);
            var weight2 = this.GetWeight(key2);

            if (weight1.Item1 != weight2.Item1)
            {
                // value = key1 / key2
                // update key1's group root to key2's group root
                this.weights[weight1.Item1] = (weight2.Item1, weight2.Item2 * value / weight1.Item2);
            }
        }
    }
}

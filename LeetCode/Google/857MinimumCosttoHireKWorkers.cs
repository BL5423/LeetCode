using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Google
{
    public class _857MinimumCosttoHireKWorkers
    {
        public double MincostToHireWorkers(int[] quality, int[] wage, int k)
        {
            double[][] ratios = new double[quality.Length][];
            for (int i = 0; i < quality.Length; ++i)
            {
                // ratios[i] is the min ratio of the worker i
                ratios[i] = new double[2] { wage[i] / (double)quality[i], quality[i] };
            }

            // sort ratios in increasing order
            Array.Sort(ratios, (a, b) => a[0].CompareTo(b[0]));

            double minWage = int.MaxValue, totalQuality = 0;
            var maxHeap = new PriorityQueue<double, double>(k); // max heap for quality
            for (int i = 0; i < quality.Length; ++i)
            {
                maxHeap.Enqueue(ratios[i][1], -ratios[i][1]);
                totalQuality += ratios[i][1];
                if (maxHeap.Count > k)
                {
                    totalQuality -= maxHeap.Dequeue();
                }

                if (maxHeap.Count == k)
                {
                    // ratios[i][0] is the global max ratio so far
                    minWage = Math.Min(minWage, ratios[i][0] * totalQuality);
                }
            }

            return minWage;
        }
    }
}

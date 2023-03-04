using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC
{
    public class _774MinimizeMaxDistancetoGasStation
    {
        public double MinmaxGasDistV2(int[] stations, int k)
        {
            double minDis = 0, maxDis = 0;
            for(int i = 0; i < stations.Length - 1; i++)
            {
                int dis = stations[i + 1] - stations[i];
                if (dis > maxDis)
                    maxDis = dis;
            }

            while (minDis < maxDis - 0.000001)
            {
                double mid = minDis + (maxDis - minDis) / 2.0;
                int newStationsLeft = k;
                for (int i = 0; i < stations.Length - 1 && newStationsLeft >= 0; ++i)
                {
                    int dis = stations[i + 1] - stations[i];
                    if (dis <= mid)
                        continue;

                    int newStations = (int)Math.Floor((dis - 0.000001) / mid);
                    newStationsLeft -= newStations;
                }

                if (newStationsLeft >= 0)
                {
                    // have something left then try shorter distance
                    maxDis = mid;
                }
                else
                {
                    // not able to reduce the max adjacent dis under mid with k new stations
                    // try longer dis
                    minDis = mid;
                }
            }

            return maxDis;
        }

        public double MinmaxGasDistV1(int[] stations, int k)
        {
            int[] counts = new int[stations.Length - 1];
            PriorityQueue<int, double> queue = new PriorityQueue<int, double>(stations.Length + k);
            for(int i = 0; i < stations.Length - 1; i++)
            {
                counts[i] = 1;
                queue.Enqueue(i, (stations[i] - stations[i + 1]) / (double)counts[i]);
            }

            for (int i = 0; i < k; ++i)
            {
                if (queue.Count != 0)
                {
                    // keep dividing the longest dis between station leftStationIndex and leftStationIndex + 1
                    var leftStationIndex = queue.Dequeue();
                    ++counts[leftStationIndex];

                    queue.Enqueue(leftStationIndex, (stations[leftStationIndex] - stations[leftStationIndex + 1]) / (double)counts[leftStationIndex]);
                }
            }

            var index = queue.Peek();
            return (stations[index + 1] - stations[index]) / (double)counts[index];
        }
    }
}

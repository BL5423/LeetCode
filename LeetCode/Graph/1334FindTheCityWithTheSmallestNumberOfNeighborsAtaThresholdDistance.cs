using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2.Graph
{
    public class _1334FindTheCityWithTheSmallestNumberOfNeighborsAtaThresholdDistance
    {
        const int MaxDis = 10001;

        public int FindTheCity(int n, int[][] edges, int distanceThreshold)
        {
            int[,] dis = new int[n, n];
            for(int i = 0; i < n; ++i)
            {
                for(int j = 0; j < n; ++j)
                {
                    if (i != j)
                        dis[i, j] = MaxDis;
                    else
                        dis[i, j] = 0;
                }
            }

            foreach(var edge in edges)
            {
                dis[edge[0], edge[1]] = edge[2];
                dis[edge[1], edge[0]] = edge[2];
            }

            // k as an intermedia city between i and j
            for(int k = 0; k < n; ++k)
            {
                for(int i = 0; i < n; ++i)
                {
                    for(int j = 0; j < n; ++j)
                    {
                        dis[i, j] = Math.Min(dis[i, j], dis[i, k] + dis[k ,j]);
                    }
                }
            }

            int city = 0, minAcc = n + 1;
            for (int i = 0; i < n; ++i)
            {
                int countForI = 0;
                for (int j = 0; j < n; ++j)
                {
                    if (dis[i, j] <= distanceThreshold)
                    {
                        ++countForI;
                    }
                }

                if (countForI <= minAcc)
                {
                    minAcc = countForI;
                    city = i;
                }
            }

            return city;
        }
    }
}

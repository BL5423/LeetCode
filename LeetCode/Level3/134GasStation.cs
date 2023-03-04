using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Level3
{
    public class _134GasStation
    {
        public int CanCompleteCircuitV2(int[] gas, int[] cost)
        {
            int startingPoint = 0, nextStation = startingPoint, gasSoFar = 0;
            do
            {
                gasSoFar += (gas[nextStation] - cost[nextStation]);

                if (gasSoFar < 0)
                {
                    while (gasSoFar < 0 && MoveBackward(startingPoint, gas.Length) != nextStation)
                    {
                        startingPoint = MoveBackward(startingPoint, gas.Length);
                        gasSoFar += (gas[startingPoint] - cost[startingPoint]);
                    }
                }

                if (gasSoFar < 0)
                    return -1;

                nextStation = MoveForward(nextStation, gas.Length);
            }
            while (nextStation != startingPoint);

            return gasSoFar >= 0 ? startingPoint : -1;
        }

        public int CanCompleteCircuitV1(int[] gas, int[] cost)
        {
            int totalGas = 0, maxGas = int.MinValue, maxGasIndex = -1;
            int[] fuelLeft = new int[gas.Length];
            for(int i = 0; i < gas.Length; i++) 
            {
                fuelLeft[i] = gas[i] - cost[i];   
                totalGas += fuelLeft[i];
                if (fuelLeft[i] > maxGas)
                {
                    maxGas = fuelLeft[i];
                    maxGasIndex = i;
                }
            }

            if (totalGas < 0)
                return -1;

            int startingPoint = maxGasIndex, gasSoFar = fuelLeft[startingPoint];
            int nextStation = MoveForward(startingPoint, gas.Length);
            while (nextStation != startingPoint)
            {
                gasSoFar += fuelLeft[nextStation];

                while (gasSoFar < 0 && startingPoint != nextStation)
                {
                    startingPoint = MoveBackward(startingPoint, gas.Length);
                    gasSoFar += fuelLeft[startingPoint];
                }

                if (gasSoFar < 0)
                    return -1;

                nextStation = MoveForward(nextStation, gas.Length);
            }

            return startingPoint;
        }

        private static int MoveForward(int pos, int length)
        {
            return (pos + 1) % length;
        }

        private static int MoveBackward(int pos, int length)
        {
            if (pos - 1 >= 0)
                return pos - 1;
            else
                return length - 1;
        }
    }
}

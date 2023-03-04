using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Level2
{
    public class _198HouseRobber
    {
        public int Rob(int[] nums)
        {
            if (nums.Length < 2)
                return nums[0];

            int moneyFromPrevious = nums[0];
            int moneyFromPreviousMinus1 = 0;
            int currentHouse = 1;
            int moneyFromCurrent = 0;
            while (currentHouse < nums.Length)
            {
                moneyFromCurrent = Math.Max(
                    nums[currentHouse] + moneyFromPreviousMinus1, // rob current house, then the total amount of money we will get is money in current house + all the moneys from the house of previous - 1 and before
                    moneyFromPrevious);                           // don't rob current house, then the total amount of money we will get is money robbed from the previous house and before.
                ++currentHouse;

                moneyFromPreviousMinus1 = moneyFromPrevious;
                moneyFromPrevious = moneyFromCurrent;
            }

            return moneyFromCurrent;
        }

        public int RobV3(int[] nums)
        {
            if (nums.Length < 2)
                return nums[0];

            int moneyFromNext = nums[nums.Length - 1];
            int moneyFromNextPlus1 = 0;
            int currentHouse = nums.Length - 2;
            int moneyFromCurrent = 0;
            while (currentHouse >= 0)
            {
                moneyFromCurrent = Math.Max(                    
                    nums[currentHouse] + moneyFromNextPlus1, // rob current house, then the total amount of money we will get is money in current house + all the moneys from the house of next + 1
                    moneyFromNext);                          // don't rob current house, then the total amount of money we will get is money robbed from the next house.
                --currentHouse;

                moneyFromNextPlus1 = moneyFromNext;
                moneyFromNext = moneyFromCurrent;
            }

            return moneyFromCurrent;
        }

        public int RobV2(int[] nums)
        {
            if (nums.Length < 2)
                return nums[0];

            int houseMoney0 = 0, houseMoney1 = 0, houseMoney2 = 0;
            int currentHouse1 = nums.Length - 2;
            int currentHouse2 = nums.Length - 1;
            while (currentHouse2 >= 0 || currentHouse1 >= 0)
            {
                int currentHouse2Money = 0;
                if (currentHouse2 >= 0)
                {
                    int money1 = nums[currentHouse2] + houseMoney1;
                    int money2 = nums[currentHouse2] + houseMoney2;
                    currentHouse2Money = Math.Max(money1, money2);
                    currentHouse2 -= 2;
                }

                int currentHouse1Money = 0;
                if (currentHouse1 >= 0)
                {
                    int money1 = nums[currentHouse1] + houseMoney0;
                    int money2 = nums[currentHouse1] + houseMoney1;
                    currentHouse1Money = Math.Max(money1, money2);
                    currentHouse1 -= 2;
                }

                houseMoney2 = houseMoney0;
                houseMoney1 = currentHouse2Money;
                houseMoney0 = currentHouse1Money;
            }

            return Math.Max(Math.Max(houseMoney0, houseMoney1), Math.Max(houseMoney1, houseMoney2));
        }

        public int RobV1(int[] nums)
        {
            if (nums.Length < 2)
                return nums[0];

            // money[i] is the max amount of money we get if rob house i
            int[] money = new int[nums.Length + 3];
            int currentHouse = nums.Length - 1;
            while (currentHouse >= 0)
            {
                int house1 = currentHouse + 2;
                int house2 = currentHouse + 3;
                int money1 = nums[currentHouse] + money[house1];
                int money2 = nums[currentHouse] + money[house2];
                money[currentHouse] = Math.Max(money1, money2);
                --currentHouse;
            }

            return Math.Max(money[0], money[1]);
        }
    }
}

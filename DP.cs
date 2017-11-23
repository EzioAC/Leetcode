using System;
using System.Collections.Generic;
using System.Linq;

namespace Leetcode
{
    class DP
    {
        //比较正直的DP算法
        public int IntegerBreak(int n)
        {
            int[] dp = new int[n+1];
            dp[1] = 1;
            for(int i=2;i<n+1;i++)
            {
                for(int j = 1;2*j<=i;j++)
                {
                    dp[i] = Math.Max(dp[i],Math.Max(i-j,dp[i-j])*Math.Max(dp[j],j));
                }
            }
            return dp[n];
        }
        //科学的数学方法
        public int IntegerBreak_Math(int n)
        {
            if (n == 2) return 1;
            if (n == 3) return 2;
            int product = 1;
            while (n > 4)
            {
                product *= 3;
                n -= 3;
            }
            product *= n;

            return product;
        }

        public int CountNumbersWithUniqueDigits(int n)
        {
            if (n == 0)
                return 1;
            if (n == 1)
                return 10;
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 10;
            int multi = 9;
            for (int i = 2; i < n + 1; i++)
            {
                //每次只考虑n位(9*9*8*7*6*...)的情况,在加上小于n位的勤快(dp[n-1])
                dp[i] = (dp[i - 1] - dp[i - 2]) * (multi--) + dp[i - 1];
            }
            return dp[n];
        }


        //Time O(N^2) Space O(N^2)
        //空间负责都可以位O(N)毕竟只是使用上一层的信息.
        public bool PredictTheWinner(int[] nums)
        {
            int[,] dp = new int[nums.Length,nums.Length];
            for(int i=0;i<nums.Length;i++)
            {
                dp[i,i] = nums[i];
            }
            for(int i = 1;i<nums.Length;i++)
            {
                for(int y=0;y+i<nums.Length;y++)
                {
                    dp[y,y+i] = Math.Max(nums[y+i]-dp[y,y+i-1],nums[y]-dp[y+1,y+i]);
                }
            }
            return dp[0,nums.Length-1]>=0;
        }
        
        //Space O(n)版
        public bool PredictTheWinner_2(int[] nums)
        {
            int[] dp = new int[nums.Length];
            for(int i =0;i<nums.Length;i++)
            {
                dp[i] = nums[i];
            }
            for(int s = 1;s<nums.Length;s++)
            {
                for(int e =nums.Length-1;e>=s;e--)
                {
                    dp[e] = Math.Max(nums[e]-dp[e-1],nums[e-1]-dp[e]);
                }
            }
            return dp[nums.Length-1]>=0;
        }

        public int MaxProfit(int[] prices)
        {
            int sold = 0, hold = int.MinValue, rest = 0;
            for (int i = 0; i < prices.Length; ++i)
            {
                int prvSold = sold;
                sold = hold + prices[i];
                hold = Math.Max(hold, rest - prices[i]);
                rest = Math.Max(rest, prvSold);
            }
            return Math.Max(sold, rest);
        }

    }
}   
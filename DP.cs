using System;
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
    }
}   
using System.Linq;

namespace Leetcode
{
    public class NumArray
    {

        private int[] nums;
        public NumArray(int[] nums)
        {
            for (int i = 1; i < nums.Length; i++)
                nums[i] += nums[i - 1];
            this.nums = nums;
        }

        public int SumRange(int i, int j)
        {
            if (i == 0)
                return nums[j];

            return nums[j] - nums[i - 1];
        }
    }

    public class NumMatrix
    {
        private int[,] dp;

        public NumMatrix(int[,] matrix)
        {
            dp = new int[matrix.GetLength(0) + 1, matrix.GetLength(1) + 1];
            for (int y = 1; y < matrix.GetLength(0) + 1; y++)
            {
                for (int x = 1; x < matrix.GetLength(1) + 1; x++)
                {
                    dp[y, x] = dp[y, x - 1] + matrix[y - 1, x - 1];
                }
            }
            for (int y = 1; y < matrix.GetLength(0) + 1; y++)
            {
                for (int x = 1; x < matrix.GetLength(1) + 1; x++)
                {
                    dp[y, x] += dp[y - 1, x];
                }
            }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            return dp[row2 + 1, col2 + 1] + dp[row1, col1] - dp[row1, col2 + 1] - dp[row2 + 1, col1];
        }
    }
}
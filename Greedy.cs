using System.Linq;

namespace Leetcode
{
    public class Greedy
    {
        public int FindLongestChain(int[,] pairs)
        {
            int[][] arr = new int[pairs.GetLength(0)][];
            for (int j = 0; j < arr.Length; j++)
            {
                arr[j] = new int[] { pairs[j, 0], pairs[j, 1] };
            }
            arr = arr.OrderBy(item => item[1]).ToArray();
            int sum = 0, n = arr.Length, i = -1;
            while (++i < n)
            {
                sum++;
                int curEnd = arr[i][1];
                while (i + 1 < n && arr[i + 1][0] <= curEnd) i++;
            }
            return sum;
        }
    }
}
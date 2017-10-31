using System;
using System.Collections.Generic;
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

        public int DistributeCandies(int[] candies)
        {
            HashSet<int> set = new HashSet<int>();
            foreach(var i in candies)
            {
                set.Add(i);
            }
            return Math.Min(set.Count,candies.Length/2);
        }

        public int CalPoints(string[] ops)
        {
            int sum = 0;
            int temp;
            Stack<int> stack = new Stack<int>();
            foreach(var c in ops)
            {
                if(c=="C")
                {
                    temp = stack.Pop();
                    sum-=temp;
                }
                else if(c=="D")
                {
                    temp = stack.Peek();
                    sum+=temp*2;
                    stack.Push(temp*2);
                }
                else if(c=="+")
                {
                    temp = stack.Pop();
                    var temp2 = stack.Peek();
                    stack.Push(temp);
                    stack.Push(temp+temp2);
                    sum+=temp+temp2;

                }
                else
                {
                    temp = int.Parse(c);
                    stack.Push(temp);
                    sum+=temp;
                }
            }
            return sum;
        }
    }
}
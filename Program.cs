using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Leetcode;

namespace Leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution temp = new Solution();
            temp.SearchMatrix(new int[,]{{1,1}},0);
        }
    }

    public class Solution
    {

        //每k个节点翻转，不足跳过
        public static ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode index = head;
            int[] cache = new int[k];
            while (true)
            {
                ListNode temp = index;
                for (int i = 0; i < k; i++)
                {
                    if (index == null)
                        return head;
                    cache[i] = index.val;
                    index = index.next;
                }
                cache = cache.Reverse().ToArray();
                for (int i = 0; i < k; i++)
                {
                    temp.val = cache[i];
                    temp = temp.next;
                }
            }
        }

        //确认needle是否为haystack的子串，返回匹配到开头index
        public int StrStr(string haystack, string needle)
        {
            var haystacks = haystack.ToCharArray();
            var needles = needle.ToCharArray();
            if (needles.Length == 0)
                return 0;
            if (haystacks.Length < needles.Length)
                return -1;
            for (int i = 0; i < haystacks.Length - needles.Length + 1; i++)
            {
                for (int j = 0; j < needles.Length; j++)
                {
                    if (haystacks[i + j] != needles[j])
                        break;
                    if (j == needles.Length - 1)
                        return i;
                }
            }
            return -1;
        }
        //感觉写的很烂,但是它过了...
        public IList<int> FindSubstring(string s, string[] words)
        {
            IList<int> answer = new List<int>();
            int wordLength = words.First().Length;
            Dictionary<string, int> template = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (template.ContainsKey(word))
                    template[word] += 1;
                else
                    template.Add(word, 1);
            }
            for (int i = 0; i < s.Length - words.Length * wordLength + 1; i++)
            {
                int j = i;
                Dictionary<string, int> temp = new Dictionary<string, int>();
                foreach (var key in template.Keys)
                {
                    temp.Add(key, template[key]);
                }
                while (true)
                {
                    string w = s.Substring(j, wordLength);
                    j += wordLength;
                    if (!temp.ContainsKey(w))
                        break;
                    if (temp[w] < 1)
                        break;
                    temp[w]--;
                    bool flag = true;
                    foreach (var key in temp.Keys)
                    {
                        if (temp[key] != 0)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        answer.Add(i);
                        break;
                    }
                }
            }
            return answer;
        }

        //情况复杂的二分查找..
        public static int Search(int[] nums, int target)
        {
            if (nums.Length < 1)
                return -1;
            int left = 0, right = nums.Length - 1;
            while (right - left > 1)
            {
                int mid = (left + right) / 2;
                if (nums[mid] == target)
                    return mid;
                if (nums[left] > nums[mid])
                {
                    if (target >= nums[left] || target <= nums[mid])
                    {
                        right = mid;
                    }
                    else
                    {
                        left = mid;
                    }
                }
                else
                {
                    if (target >= nums[mid] || (target <= nums[right] && nums[right] < nums[mid]))
                    {
                        left = mid;
                    }
                    else
                    {
                        right = mid;
                    }
                }
            }
            if (nums[left] == target)
                return left;
            if (nums[right] == target)
                return right;
            return -1;
        }

        //神烦的二分查找×2
        public static int[] SearchRange(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;
            int[] answer = new int[] { -1, -1 };
            if (nums.Length == 0)
                return answer;
            int target_l = left;
            int target_r = right;
            while (target_l <= right)
            {
                int mid = (target_l + right) / 2;
                if (nums[mid] == target && (mid == 0 || nums[mid - 1] != target))
                {
                    target_l = mid;
                    answer[0] = mid;
                    break;
                }
                if (nums[mid] == target)
                {
                    right = mid - 1;
                    answer[0] = mid;
                }
                else if (nums[mid] > target)
                {
                    right = mid - 1;
                }
                else if (nums[mid] < target)
                {
                    target_l = mid + 1;
                }
            }
            left = target_l;
            while (left <= target_r)
            {
                int mid = (left + target_r) / 2;
                if (nums[mid] == target && (mid == nums.Length - 1 || nums[mid + 1] != target))
                {
                    target_r = mid;
                    answer[1] = mid;
                    break;
                }
                if (nums[mid] > target)
                {
                    target_r = mid - 1;
                }
                else if (nums[mid] == target)
                {
                    left = mid + 1;
                    answer[1] = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return answer;
        }

        //很迷的题
        public static string CountAndSay(int n)
        {
            string answer = "1";
            for (int i = 1; i < n; i++)
            {
                StringBuilder sb = new StringBuilder();
                var temp = answer.ToCharArray();
                char curr = temp[0];
                int count = 0;
                foreach (var c in temp)
                {
                    if (c == curr)
                        count++;
                    else
                    {
                        sb.Append(count.ToString() + curr);
                        curr = c;
                        count = 1;
                    }
                }
                sb.Append(count.ToString() + curr);
                answer = sb.ToString();
            }
            return answer;
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            return null;
        }

        //对号入座，看缺什么
        public static int FirstMissingPositive(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == i + 1)
                    continue;
                if (nums[i] > nums.Length || nums[i] < 1)
                    continue;
                int temp = nums[nums[i] - 1];
                nums[nums[i] - 1] = nums[i];
                if (nums[i] != temp)
                {
                    nums[i] = temp;
                    i--;
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (i + 1 != nums[i])
                    return i + 1;
            }
            return nums.Length + 1;
        }

        //...
        public int Trap(int[] height)
        {
            if (height.Length == 0)
                return 0;
            int left = 0, right = height.Length - 1;
            int Height_L = height[left], Height_R = height[right];
            int answer = 0;
            while (left < right)
            {
                if (Height_L <= Height_R)
                {
                    left++;
                    if (height[left] > Height_L)
                    {
                        Height_L = height[left];
                    }
                    else
                    {
                        answer += Height_L - height[left];
                    }
                }
                else
                {
                    right--;
                    if (height[right] > Height_R)
                    {
                        Height_R = height[right];
                    }
                    else
                    {
                        answer += Height_R - height[right];
                    }
                }
            }
            return answer;
        }

        //转成数组 小学乘法步骤
        public static string Multiply(string num1, string num2)
        {
            int[] result = new int[num1.Length + num2.Length];
            int[] x = new int[num1.Length];
            int[] y = new int[num2.Length];
            for (int i = 0; i < x.Length; i++)
            {
                x[i] = int.Parse(num1[i].ToString());
            }
            for (int i = 0; i < y.Length; i++)
            {
                y[i] = int.Parse(num2[i].ToString());
            }
            for (int i = x.Length - 1; i >= 0; i--)
            {
                for (int j = y.Length - 1; j >= 0; j--)
                {
                    int temp = result.Length - (x.Length + y.Length - i - j - 2) - 1;
                    result[temp] += x[i] * y[j];
                    result[temp - 1] += result[temp] / 10;
                    result[temp] %= 10;
                }
            }
            int index = 0;
            while (result[index] == 0)
            {
                index++;
                if (index > result.Length - 1)
                    return "0";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = index; i < result.Length; i++)
            {
                sb.Append(result[i].ToString());
            }
            return sb.ToString();
        }

        //在步程之内找最远点 循环～～
        public int Jump(int[] nums)
        {
            if (nums.Length < 2)
                return 0;
            int steps = 0, currentMax = 0, i = 0, nextMax = 0;

            while (currentMax - i + 1 > 0)
            {
                steps++;
                for (; i <= currentMax; i++)
                {
                    nextMax = Math.Max(nextMax, nums[i] + i);
                    if (nextMax >= nums.Length - 1) return steps;
                }
                currentMax = nextMax;
            }
            return 0;
        }

        //排序转成同一个顺序
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<String, IList<String>> res = new Dictionary<string, IList<string>>();
            foreach (var str in strs)
            {
                var cs = str.ToCharArray();
                Array.Sort(cs);
                String key = new string(cs);
                if (!res.ContainsKey(key))
                {
                    res[key] = new List<string>();
                }
                res[key].Add(str);
            }
            IList<IList<string>> answer = new List<IList<string>>();
            foreach (var s in res.Keys)
            {
                answer.Add(res[s]);
            }
            return answer;

        }

        public double MyPow(double x, int n)
        {
            double temp = x;
            if (n == 0)
                return 1;
            temp = MyPow(x, n / 2);
            if (n % 2 == 0)
                return temp * temp;
            else
            {
                if (n > 0)
                    return x * temp * temp;
                else
                    return (temp * temp) / x;
            }
        }

        public static bool CanJump(int[] nums)
        {
            int nextmax = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > nextmax)
                    return false;
                nextmax = Math.Max(nextmax, i + nums[i]);
                if (nextmax >= nums.Length - 1)
                    return true;
            }
            return false;
        }

        public IList<Interval> Merge(IList<Interval> intervals)
        {
            //画重点（LINQ） nlgn
            var res = intervals.OrderBy(p => p.start).ToList();
            int index = 0;
            while (index < res.Count - 1)
            {
                if (res[index].end >= res[index + 1].start)
                {
                    res[index].end = Math.Max(res[index + 1].end, res[index].end);
                    res.RemoveAt(index + 1);
                }
                else
                {
                    index++;
                }
            }
            return res;
        }

        //Leetcode过不了,传递的数组是fixed-size的.自己手动复制再处理返回....
        //O(n)时间复杂度.做个o(lgn)二分怕是也行...
        public IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
        {
            if (intervals.Count == 0 || intervals.Last().end < newInterval.start)
            {
                intervals.Add(newInterval);
                return intervals;
            }
            for (int i = 0; i < intervals.Count; i++)
            {
                //要不i里面 要不i前面
                if (newInterval.start <= intervals[i].end)
                {
                    if (newInterval.start < intervals[i].start)
                        intervals.Insert(i, newInterval);
                    else
                        intervals[i].end = Math.Max(intervals[i].end, newInterval.end);
                    int end = intervals[i].end;
                    for (int j = i + 1; j < intervals.Count; j++)
                    {
                        if (intervals[j].end <= end)
                        {
                            intervals.RemoveAt(j);
                            j--;
                        }
                        else if (intervals[j].start > end)
                            break;
                        else if (intervals[j].end > end)
                        {
                            intervals[i].end = intervals[j].end;
                            intervals.RemoveAt(j);
                            j--;
                        }
                    }
                    break;
                }
            }
            return intervals;
        }

        public int LengthOfLastWord(string s)
        {
            s = s.Trim();
            int len = 0;
            if (s.Length == 0)
                return len;
            var cs = s.ToCharArray();
            for (int i = cs.Length - 1; i >= 0; i--)
            {
                if (cs[i] == ' ')
                {
                    break;
                }
                else
                {
                    len++;
                }
            }
            return len;
        }

        //O(n)
        public ListNode RotateRight(ListNode head, int k)
        {
            int count = 0;
            ListNode temp = head;
            //统计个数&成环
            while (true)
            {
                if (temp == null)
                {
                    return null;
                }
                count++;
                if (temp.next == null)
                {
                    temp.next = head;
                    break;
                }
                temp = temp.next;
            }
            //避免k过大
            k %= count;
            //断环
            int a = count - k;
            temp = head;
            while (a > 1)
            {
                temp = temp.next;
                a--;
            }
            var answer = temp.next;
            temp.next = null;
            return answer;
        }

        //DP 
        //dp[i,j] = dp[i-1,j]+dp[i,j-1];
        public static int UniquePaths(int m, int n)
        {
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 1; i < m + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        dp[1, 1] = 1;
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }
            return dp[m, n];
            }

        public int UniquePathsWithObstacles(int[,] obstacleGrid)
        {
            int m = obstacleGrid.GetLength(0);
            int n = obstacleGrid.GetLength(1);
            if (m < 1 || n < 1 || obstacleGrid[0, 0] == 1)
                return 0;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 1; i < m + 1; i++)
            {
                for (int j = 1; j < n + 1; j++)
                {
                    if (i == 1 && j == 1)
                    {
                        dp[1, 1] = 1;
                    }
                    else
                    {
                        if (obstacleGrid[i - 1, j - 1] == 0)
                            dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }
            return dp[m, n];
        }

        public static int MinPathSum(int[,] grid)
        {
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            if (m == 1 && n == 1)
                return grid[0, 0];
            int[,] dp = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        dp[i, j] = grid[i, j];
                    }
                    else if (i == 0)
                    {
                        dp[i, j] = grid[i, j] + dp[i, j - 1];
                    }
                    else if (j == 0)
                    {
                        dp[i, j] = grid[i, j] + (dp[i - 1, j]);
                    }
                    else
                        dp[i, j] = grid[i, j] + Math.Min(dp[i - 1, j], dp[i, j - 1]);
                }
            }
            return dp[m - 1, n - 1];
        }

        //这道题很操蛋!!
        public bool IsNumber(string s)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(s.TrimStart().TrimEnd(), @"^[\ ]*[+-]?(\d+\.?\d*|\d*\.?\d+)(e[+-]?\d+)?[\ ]*$");
        }

        //麻烦...
        public static string AddBinary(string a, string b)
        {
            char[] cs_a = a.ToCharArray();
            char[] cs_b = b.ToCharArray();
            int index_a = cs_a.Length - 1;
            int index_b = cs_b.Length - 1;
            bool[] answer = new bool[Math.Max(cs_a.Length, cs_b.Length) + 1];
            for (int i = answer.Length - 1; i > 0; i--)
            {
                bool b1, b2;
                if (index_a < 0)
                {
                    b1 = false;
                }
                else
                {
                    b1 = cs_a[index_a] == '1';
                }
                index_a--;
                if (index_b < 0)
                {
                    b2 = false;
                }
                else
                {
                    b2 = cs_b[index_b] == '1';
                }
                index_b--;
                if (answer[i])
                {
                    if (b1 && b2) answer[i - 1] = true;
                    else if (!b1 && !b2) ;
                    else
                    {
                        answer[i - 1] = true;
                        answer[i] = false;
                    }
                }
                else
                {
                    if (b1 && b2) answer[i - 1] = true;
                    else if (!b1 && !b2) ;
                    else
                    {
                        answer[i] = true;
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            if (answer.Length == 1)
                return answer[0] ? "1" : "0";
            if (answer[0])
            {
                sb.Append("1");
            }
            for (int i = 1; i < answer.Length; i++)
            {
                sb.Append(answer[i] ? "1" : "0");
            }
            return sb.ToString();
        }

        //麻烦...
        public static IList<string> FullJustify(string[] words, int maxWidth)
        {
            IList<string> answer = new List<string>();
            IList<string> temp = new List<string>();
            StringBuilder sb = new StringBuilder();
            int count = 0;
            for (int x = 0; x < words.Length; x++)
            {
                if (count + words[x].Length + temp.Count <= maxWidth)
                {
                    count += words[x].Length;
                    temp.Add(words[x]);
                }
                else if (temp.Count == 1)
                {
                    sb.Append(temp[0]);
                    while (sb.Length < maxWidth)
                    {
                        sb.Append(" ");
                    }
                    answer.Add(sb.ToString());
                    sb.Clear();
                    temp.Clear();
                    count = 0;
                    x--;
                }
                else
                {
                    x--;
                    int[] interval = new int[temp.Count - 1];
                    int left = maxWidth - count;
                    int t = left / interval.Length;
                    for (int i = 0; i < interval.Length; i++)
                    {
                        interval[i] = t;
                        left -= t;
                    }
                    int index = 0;
                    while (left > 0)
                    {
                        interval[index++]++;
                        index %= interval.Length;
                        left--;
                    }
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        sb.Append(temp[i]);
                        for (int j = 0; j < interval[i]; j++)
                            sb.Append(" ");
                    }
                    sb.Append(temp[temp.Count - 1]);
                    answer.Add(sb.ToString());
                    sb.Clear();
                    temp.Clear();
                    count = 0;
                }
            }
            for (int i = 0; i < temp.Count; i++)
            {
                sb.Append(temp[i]);
                if (temp.Count - 1 != i)
                    sb.Append(" ");
            }
            while (sb.Length < maxWidth)
            {
                sb.Append(" ");
            }
            answer.Add(sb.ToString());
            return answer;
        }

        //烦!
        public static string SimplifyPath(string path)
        {
            Stack<string> stack = new Stack<string>();
            var cs = path.ToCharArray();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cs.Length; i++)
            {
                if (cs[i] == '/' || i == cs.Length - 1)
                {
                    if (cs[i] != '/')
                        sb.Append(cs[i]);
                    var temp = sb.ToString();
                    sb.Clear();
                    if (temp == "..")
                    {
                        if (stack.Any())
                            stack.Pop();
                    }
                    else if (temp != "." && temp != "")
                    {
                        stack.Push(temp);
                    }
                }
                else
                {
                    sb.Append(cs[i]);
                }
            }
            sb.Clear();
            while (stack.Any())
                //要不自己翻过了用append?
                sb.Insert(0, "/" + stack.Pop());
            return sb.ToString() != "" ? sb.ToString() : "/";
        }


        //保存第一个的坐标，用它所在的行和列来该行或列是否要清零 o(1)space
        public void SetZeroes(int[,] matrix) {
            int y=-1,x=-1;
            bool first = true;
            for(int j=0;j<matrix.GetLength(0);j++)
            {
                for(int i=0;i<matrix.GetLength(1);i++)
                {
                    if (matrix[j, i] == 0)
                    {
                        if (first)
                        {
                            y = j;
                            x = i;
                            first = false;
                        }
                        else
                        {
                            matrix[y, i] = 0;
                            matrix[j, x] = 0;
                        }
                    }
                }
            }
            if(y<0&&x<0)
                return;
            for(int i=0;i<matrix.GetLength(1);i++)
            {
                if(i==x)
                    continue;
                else if(matrix[y,i]==0)
                {
                    for(int j=0;j<matrix.GetLength(0);j++)
                    {
                        matrix[j,i]=0;
                    }
                }
            }
            for(int j=0;j<matrix.GetLength(0);j++)
            {
                if(j==y)
                    continue;
                else if(matrix[j,x]==0)
                {
                    for(int i=0;i<matrix.GetLength(1);i++)
                    {
                        matrix[j,i]=0;
                    }
                }
            }
            for(int i=0;i<matrix.GetLength(1);i++)
            {
                matrix[y,i]=0;
            }
            for(int j=0;j<matrix.GetLength(0);j++)
            {
                matrix[j,x]=0;
            }
        }


        //二分 O(lgn*m)
        public bool SearchMatrix(int[,] matrix, int target) {
            int y = matrix.GetLength(0);
            int x = matrix.GetLength(1);
            if(x==0||y==0)
                return false;
            int l = 0;
            int r = x*y-1;
            while(r-l>1)
            {
                int mid = (l+r)/2;
                int value = matrix[mid/x,mid%x];
                if(value==target)
                    return true;
                else if(value>target)
                {
                    r = mid;
                }
                else
                {
                    l=mid;
                }
            }
            if(matrix[l/x,l%x]==target||matrix[r/x,r%x]==target)
                return true;
            return false;
        }
    }
}
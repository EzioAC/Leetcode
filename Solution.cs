using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Leetcode;

namespace Leetcode {

    public class Solutions {

        //每k个节点翻转，不足跳过
        public static ListNode ReverseKGroup (ListNode head, int k) {
            ListNode index = head;
            int[] cache = new int[k];
            while (true) {
                ListNode temp = index;
                for (int i = 0; i < k; i++) {
                    if (index == null)
                        return head;
                    cache[i] = index.val;
                    index = index.next;
                }
                cache = cache.Reverse ().ToArray ();
                for (int i = 0; i < k; i++) {
                    temp.val = cache[i];
                    temp = temp.next;
                }
            }
        }

        //确认needle是否为haystack的子串，返回匹配到开头index
        public int StrStr (string haystack, string needle) {
            var haystacks = haystack.ToCharArray ();
            var needles = needle.ToCharArray ();
            if (needles.Length == 0)
                return 0;
            if (haystacks.Length < needles.Length)
                return -1;
            for (int i = 0; i < haystacks.Length - needles.Length + 1; i++) {
                for (int j = 0; j < needles.Length; j++) {
                    if (haystacks[i + j] != needles[j])
                        break;
                    if (j == needles.Length - 1)
                        return i;
                }
            }
            return -1;
        }
        //感觉写的很烂,但是它过了...
        public IList<int> FindSubstring (string s, string[] words) {
            IList<int> answer = new List<int> ();
            int wordLength = words.First ().Length;
            Dictionary<string, int> template = new Dictionary<string, int> ();
            foreach (var word in words) {
                if (template.ContainsKey (word))
                    template[word] += 1;
                else
                    template.Add (word, 1);
            }
            for (int i = 0; i < s.Length - words.Length * wordLength + 1; i++) {
                int j = i;
                Dictionary<string, int> temp = new Dictionary<string, int> ();
                foreach (var key in template.Keys) {
                    temp.Add (key, template[key]);
                }
                while (true) {
                    string w = s.Substring (j, wordLength);
                    j += wordLength;
                    if (!temp.ContainsKey (w))
                        break;
                    if (temp[w] < 1)
                        break;
                    temp[w]--;
                    bool flag = true;
                    foreach (var key in temp.Keys) {
                        if (temp[key] != 0) {
                            flag = false;
                            break;
                        }
                    }
                    if (flag) {
                        answer.Add (i);
                        break;
                    }
                }
            }
            return answer;
        }

        //情况复杂的二分查找..
        public static int Search (int[] nums, int target) {
            if (nums.Length < 1)
                return -1;
            int left = 0, right = nums.Length - 1;
            while (right - left > 1) {
                int mid = (left + right) / 2;
                if (nums[mid] == target)
                    return mid;
                if (nums[left] > nums[mid]) {
                    if (target >= nums[left] || target <= nums[mid]) {
                        right = mid;
                    } else {
                        left = mid;
                    }
                } else {
                    if (target >= nums[mid] || (target <= nums[right] && nums[right] < nums[mid])) {
                        left = mid;
                    } else {
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
        public static int[] SearchRange (int[] nums, int target) {
            int left = 0;
            int right = nums.Length - 1;
            int[] answer = new int[] {-1, -1 };
            if (nums.Length == 0)
                return answer;
            int target_l = left;
            int target_r = right;
            while (target_l <= right) {
                int mid = (target_l + right) / 2;
                if (nums[mid] == target && (mid == 0 || nums[mid - 1] != target)) {
                    target_l = mid;
                    answer[0] = mid;
                    break;
                }
                if (nums[mid] == target) {
                    right = mid - 1;
                    answer[0] = mid;
                } else if (nums[mid] > target) {
                    right = mid - 1;
                } else if (nums[mid] < target) {
                    target_l = mid + 1;
                }
            }
            left = target_l;
            while (left <= target_r) {
                int mid = (left + target_r) / 2;
                if (nums[mid] == target && (mid == nums.Length - 1 || nums[mid + 1] != target)) {
                    target_r = mid;
                    answer[1] = mid;
                    break;
                }
                if (nums[mid] > target) {
                    target_r = mid - 1;
                } else if (nums[mid] == target) {
                    left = mid + 1;
                    answer[1] = mid;
                } else {
                    left = mid + 1;
                }
            }
            return answer;
        }

        //很迷的题
        public static string CountAndSay (int n) {
            string answer = "1";
            for (int i = 1; i < n; i++) {
                StringBuilder sb = new StringBuilder ();
                var temp = answer.ToCharArray ();
                char curr = temp[0];
                int count = 0;
                foreach (var c in temp) {
                    if (c == curr)
                        count++;
                    else {
                        sb.Append (count.ToString () + curr);
                        curr = c;
                        count = 1;
                    }
                }
                sb.Append (count.ToString () + curr);
                answer = sb.ToString ();
            }
            return answer;
        }

        public IList<IList<int>> CombinationSum (int[] candidates, int target) {
            return null;
        }

        //对号入座，看缺什么
        public static int FirstMissingPositive (int[] nums) {
            for (int i = 0; i < nums.Length; i++) {
                if (nums[i] == i + 1)
                    continue;
                if (nums[i] > nums.Length || nums[i] < 1)
                    continue;
                int temp = nums[nums[i] - 1];
                nums[nums[i] - 1] = nums[i];
                if (nums[i] != temp) {
                    nums[i] = temp;
                    i--;
                }
            }
            for (int i = 0; i < nums.Length; i++) {
                if (i + 1 != nums[i])
                    return i + 1;
            }
            return nums.Length + 1;
        }

        //...
        public int Trap (int[] height) {
            if (height.Length == 0)
                return 0;
            int left = 0, right = height.Length - 1;
            int Height_L = height[left], Height_R = height[right];
            int answer = 0;
            while (left < right) {
                if (Height_L <= Height_R) {
                    left++;
                    if (height[left] > Height_L) {
                        Height_L = height[left];
                    } else {
                        answer += Height_L - height[left];
                    }
                } else {
                    right--;
                    if (height[right] > Height_R) {
                        Height_R = height[right];
                    } else {
                        answer += Height_R - height[right];
                    }
                }
            }
            return answer;
        }

        //转成数组 小学乘法步骤
        public static string Multiply (string num1, string num2) {
            int[] result = new int[num1.Length + num2.Length];
            int[] x = new int[num1.Length];
            int[] y = new int[num2.Length];
            for (int i = 0; i < x.Length; i++) {
                x[i] = int.Parse (num1[i].ToString ());
            }
            for (int i = 0; i < y.Length; i++) {
                y[i] = int.Parse (num2[i].ToString ());
            }
            for (int i = x.Length - 1; i >= 0; i--) {
                for (int j = y.Length - 1; j >= 0; j--) {
                    int temp = result.Length - (x.Length + y.Length - i - j - 2) - 1;
                    result[temp] += x[i] * y[j];
                    result[temp - 1] += result[temp] / 10;
                    result[temp] %= 10;
                }
            }
            int index = 0;
            while (result[index] == 0) {
                index++;
                if (index > result.Length - 1)
                    return "0";
            }
            StringBuilder sb = new StringBuilder ();
            for (int i = index; i < result.Length; i++) {
                sb.Append (result[i].ToString ());
            }
            return sb.ToString ();
        }

        //在步程之内找最远点 循环～～
        public int Jump (int[] nums) {
            if (nums.Length < 2)
                return 0;
            int steps = 0, currentMax = 0, i = 0, nextMax = 0;

            while (currentMax - i + 1 > 0) {
                steps++;
                for (; i <= currentMax; i++) {
                    nextMax = Math.Max (nextMax, nums[i] + i);
                    if (nextMax >= nums.Length - 1) return steps;
                }
                currentMax = nextMax;
            }
            return 0;
        }

        //排序转成同一个顺序
        public IList<IList<string>> GroupAnagrams (string[] strs) {
            Dictionary<String, IList<String>> res = new Dictionary<string, IList<string>> ();
            foreach (var str in strs) {
                var cs = str.ToCharArray ();
                Array.Sort (cs);
                String key = new string (cs);
                if (!res.ContainsKey (key)) {
                    res[key] = new List<string> ();
                }
                res[key].Add (str);
            }
            IList<IList<string>> answer = new List<IList<string>> ();
            foreach (var s in res.Keys) {
                answer.Add (res[s]);
            }
            return answer;

        }

        public double MyPow (double x, int n) {
            double temp = x;
            if (n == 0)
                return 1;
            temp = MyPow (x, n / 2);
            if (n % 2 == 0)
                return temp * temp;
            else {
                if (n > 0)
                    return x * temp * temp;
                else
                    return (temp * temp) / x;
            }
        }

        public static bool CanJump (int[] nums) {
            int nextmax = 0;
            for (int i = 0; i < nums.Length; i++) {
                if (i > nextmax)
                    return false;
                nextmax = Math.Max (nextmax, i + nums[i]);
                if (nextmax >= nums.Length - 1)
                    return true;
            }
            return false;
        }

        public IList<Interval> Merge (IList<Interval> intervals) {
            //画重点（LINQ） nlgn
            var res = intervals.OrderBy (p => p.start).ToList ();
            int index = 0;
            while (index < res.Count - 1) {
                if (res[index].end >= res[index + 1].start) {
                    res[index].end = Math.Max (res[index + 1].end, res[index].end);
                    res.RemoveAt (index + 1);
                } else {
                    index++;
                }
            }
            return res;
        }

        //Leetcode过不了,传递的数组是fixed-size的.自己手动复制再处理返回....
        //O(n)时间复杂度.做个o(lgn)二分怕是也行...
        public IList<Interval> Insert (IList<Interval> intervals, Interval newInterval) {
            if (intervals.Count == 0 || intervals.Last ().end < newInterval.start) {
                intervals.Add (newInterval);
                return intervals;
            }
            for (int i = 0; i < intervals.Count; i++) {
                //要不i里面 要不i前面
                if (newInterval.start <= intervals[i].end) {
                    if (newInterval.start < intervals[i].start)
                        intervals.Insert (i, newInterval);
                    else
                        intervals[i].end = Math.Max (intervals[i].end, newInterval.end);
                    int end = intervals[i].end;
                    for (int j = i + 1; j < intervals.Count; j++) {
                        if (intervals[j].end <= end) {
                            intervals.RemoveAt (j);
                            j--;
                        } else if (intervals[j].start > end)
                            break;
                        else if (intervals[j].end > end) {
                            intervals[i].end = intervals[j].end;
                            intervals.RemoveAt (j);
                            j--;
                        }
                    }
                    break;
                }
            }
            return intervals;
        }

        public int LengthOfLastWord (string s) {
            s = s.Trim ();
            int len = 0;
            if (s.Length == 0)
                return len;
            var cs = s.ToCharArray ();
            for (int i = cs.Length - 1; i >= 0; i--) {
                if (cs[i] == ' ') {
                    break;
                } else {
                    len++;
                }
            }
            return len;
        }

        //O(n)
        public ListNode RotateRight (ListNode head, int k) {
            int count = 0;
            ListNode temp = head;
            //统计个数&成环
            while (true) {
                if (temp == null) {
                    return null;
                }
                count++;
                if (temp.next == null) {
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
            while (a > 1) {
                temp = temp.next;
                a--;
            }
            var answer = temp.next;
            temp.next = null;
            return answer;
        }

        //DP 
        //dp[i,j] = dp[i-1,j]+dp[i,j-1];
        public static int UniquePaths (int m, int n) {
            int[, ] dp = new int[m + 1, n + 1];
            for (int i = 1; i < m + 1; i++) {
                for (int j = 1; j < n + 1; j++) {
                    if (i == 1 && j == 1) {
                        dp[1, 1] = 1;
                    } else {
                        dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }
            return dp[m, n];
        }

        public int UniquePathsWithObstacles (int[, ] obstacleGrid) {
            int m = obstacleGrid.GetLength (0);
            int n = obstacleGrid.GetLength (1);
            if (m < 1 || n < 1 || obstacleGrid[0, 0] == 1)
                return 0;
            int[, ] dp = new int[m + 1, n + 1];
            for (int i = 1; i < m + 1; i++) {
                for (int j = 1; j < n + 1; j++) {
                    if (i == 1 && j == 1) {
                        dp[1, 1] = 1;
                    } else {
                        if (obstacleGrid[i - 1, j - 1] == 0)
                            dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }
            return dp[m, n];
        }

        public static int MinPathSum (int[, ] grid) {
            int m = grid.GetLength (0);
            int n = grid.GetLength (1);
            if (m == 1 && n == 1)
                return grid[0, 0];
            int[, ] dp = new int[m, n];
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (i == 0 && j == 0) {
                        dp[i, j] = grid[i, j];
                    } else if (i == 0) {
                        dp[i, j] = grid[i, j] + dp[i, j - 1];
                    } else if (j == 0) {
                        dp[i, j] = grid[i, j] + (dp[i - 1, j]);
                    } else
                        dp[i, j] = grid[i, j] + Math.Min (dp[i - 1, j], dp[i, j - 1]);
                }
            }
            return dp[m - 1, n - 1];
        }

        //这道题很操蛋!!
        public bool IsNumber (string s) {
            return System.Text.RegularExpressions.Regex.IsMatch (s.TrimStart ().TrimEnd (), @"^[\ ]*[+-]?(\d+\.?\d*|\d*\.?\d+)(e[+-]?\d+)?[\ ]*$");
        }

        //麻烦...
        public static string AddBinary (string a, string b) {
            char[] cs_a = a.ToCharArray ();
            char[] cs_b = b.ToCharArray ();
            int index_a = cs_a.Length - 1;
            int index_b = cs_b.Length - 1;
            bool[] answer = new bool[Math.Max (cs_a.Length, cs_b.Length) + 1];
            for (int i = answer.Length - 1; i > 0; i--) {
                bool b1, b2;
                if (index_a < 0) {
                    b1 = false;
                } else {
                    b1 = cs_a[index_a] == '1';
                }
                index_a--;
                if (index_b < 0) {
                    b2 = false;
                } else {
                    b2 = cs_b[index_b] == '1';
                }
                index_b--;
                if (answer[i]) {
                    if (b1 && b2) answer[i - 1] = true;
                    else if (!b1 && !b2);
                    else {
                        answer[i - 1] = true;
                        answer[i] = false;
                    }
                } else {
                    if (b1 && b2) answer[i - 1] = true;
                    else if (!b1 && !b2);
                    else {
                        answer[i] = true;
                    }
                }
            }
            StringBuilder sb = new StringBuilder ();
            if (answer.Length == 1)
                return answer[0] ? "1" : "0";
            if (answer[0]) {
                sb.Append ("1");
            }
            for (int i = 1; i < answer.Length; i++) {
                sb.Append (answer[i] ? "1" : "0");
            }
            return sb.ToString ();
        }

        //麻烦...
        public static IList<string> FullJustify (string[] words, int maxWidth) {
            IList<string> answer = new List<string> ();
            IList<string> temp = new List<string> ();
            StringBuilder sb = new StringBuilder ();
            int count = 0;
            for (int x = 0; x < words.Length; x++) {
                if (count + words[x].Length + temp.Count <= maxWidth) {
                    count += words[x].Length;
                    temp.Add (words[x]);
                } else if (temp.Count == 1) {
                    sb.Append (temp[0]);
                    while (sb.Length < maxWidth) {
                        sb.Append (" ");
                    }
                    answer.Add (sb.ToString ());
                    sb.Clear ();
                    temp.Clear ();
                    count = 0;
                    x--;
                } else {
                    x--;
                    int[] interval = new int[temp.Count - 1];
                    int left = maxWidth - count;
                    int t = left / interval.Length;
                    for (int i = 0; i < interval.Length; i++) {
                        interval[i] = t;
                        left -= t;
                    }
                    int index = 0;
                    while (left > 0) {
                        interval[index++]++;
                        index %= interval.Length;
                        left--;
                    }
                    for (int i = 0; i < temp.Count - 1; i++) {
                        sb.Append (temp[i]);
                        for (int j = 0; j < interval[i]; j++)
                            sb.Append (" ");
                    }
                    sb.Append (temp[temp.Count - 1]);
                    answer.Add (sb.ToString ());
                    sb.Clear ();
                    temp.Clear ();
                    count = 0;
                }
            }
            for (int i = 0; i < temp.Count; i++) {
                sb.Append (temp[i]);
                if (temp.Count - 1 != i)
                    sb.Append (" ");
            }
            while (sb.Length < maxWidth) {
                sb.Append (" ");
            }
            answer.Add (sb.ToString ());
            return answer;
        }

        //烦!
        public static string SimplifyPath (string path) {
            Stack<string> stack = new Stack<string> ();
            var cs = path.ToCharArray ();
            StringBuilder sb = new StringBuilder ();
            for (int i = 0; i < cs.Length; i++) {
                if (cs[i] == '/' || i == cs.Length - 1) {
                    if (cs[i] != '/')
                        sb.Append (cs[i]);
                    var temp = sb.ToString ();
                    sb.Clear ();
                    if (temp == "..") {
                        if (stack.Any ())
                            stack.Pop ();
                    } else if (temp != "." && temp != "") {
                        stack.Push (temp);
                    }
                } else {
                    sb.Append (cs[i]);
                }
            }
            sb.Clear ();
            while (stack.Any ())
                //要不自己翻过了用append?
                sb.Insert (0, "/" + stack.Pop ());
            return sb.ToString () != "" ? sb.ToString () : "/";
        }

        //保存第一个的坐标，用它所在的行和列来该行或列是否要清零 o(1)space
        public void SetZeroes (int[, ] matrix) {
            int y = -1, x = -1;
            bool first = true;
            for (int j = 0; j < matrix.GetLength (0); j++) {
                for (int i = 0; i < matrix.GetLength (1); i++) {
                    if (matrix[j, i] == 0) {
                        if (first) {
                            y = j;
                            x = i;
                            first = false;
                        } else {
                            matrix[y, i] = 0;
                            matrix[j, x] = 0;
                        }
                    }
                }
            }
            if (y < 0 && x < 0)
                return;
            for (int i = 0; i < matrix.GetLength (1); i++) {
                if (i == x)
                    continue;
                else if (matrix[y, i] == 0) {
                    for (int j = 0; j < matrix.GetLength (0); j++) {
                        matrix[j, i] = 0;
                    }
                }
            }
            for (int j = 0; j < matrix.GetLength (0); j++) {
                if (j == y)
                    continue;
                else if (matrix[j, x] == 0) {
                    for (int i = 0; i < matrix.GetLength (1); i++) {
                        matrix[j, i] = 0;
                    }
                }
            }
            for (int i = 0; i < matrix.GetLength (1); i++) {
                matrix[y, i] = 0;
            }
            for (int j = 0; j < matrix.GetLength (0); j++) {
                matrix[j, x] = 0;
            }
        }

        //二分 O(lgn*m)
        public bool SearchMatrix (int[, ] matrix, int target) {
            int y = matrix.GetLength (0);
            int x = matrix.GetLength (1);
            if (x == 0 || y == 0)
                return false;
            int l = 0;
            int r = x * y - 1;
            while (r - l > 1) {
                int mid = (l + r) / 2;
                int value = matrix[mid / x, mid % x];
                if (value == target)
                    return true;
                else if (value > target) {
                    r = mid;
                } else {
                    l = mid;
                }
            }
            if (matrix[l / x, l % x] == target || matrix[r / x, r % x] == target)
                return true;
            return false;
        }

        public void SortColors (int[] nums) {
            int left = 0;
            int right = nums.Length - 1;
            for (int i = 0; i <= right; i++) {
                while (nums[i] == 2 && i < right) Swap (ref nums[i], ref nums[right--]);
                while (nums[i] == 0 && i > left) Swap (ref nums[i], ref nums[left++]);
            }
        }

        private void Swap (ref int a, ref int b) {
            int temp = b;
            b = a;
            a = temp;
        }

        //参考版..
        public string MinWindow (string s, string t) {
            if (s == "" || t == "" || s.Length < t.Length) {
                return "";
            }
            int count = t.Length;
            int[] require = new int[128];
            bool[] chSet = new bool[128];
            for (int index = 0; index < count; ++index) {
                require[t[index]]++;
                chSet[t[index]] = true;
            }
            int i = -1;
            int j = 0;
            int minLen = int.MaxValue;
            int minIdx = 0;
            while (i < s.Length && j < s.Length) {
                if (count > 0) {
                    if (i == s.Length - 1)
                        break;
                    i++;
                    require[s[i]]--;
                    if (chSet[s[i]] && require[s[i]] >= 0) {
                        count--;
                    }
                } else {
                    if (minLen > i - j + 1) {
                        minLen = i - j + 1;
                        minIdx = j;
                    }
                    require[s[j]]++;
                    if (chSet[s[j]] && require[s[j]] > 0) {
                        count++;
                    }
                    j++;
                }
            }
            if (minLen == int.MaxValue) {
                return "";
            }
            return s.Substring (minIdx, minLen);
        }

        IList<IList<int>> answer;
        int[] arr;
        public IList<IList<int>> Combine (int n, int k) {
            answer = new List<IList<int>> ();
            arr = new int[k];
            GetCombine (1, n, 0);
            return answer;
        }

        private void GetCombine (int l, int r, int n) {
            if (n == arr.Length) {
                answer.Add (arr.Clone () as int[]);
                return;
            }
            if (l > r)
                return;
            for (int i = l; i <= r; i++) {
                arr[n] = i;
                GetCombine (i + 1, r, n + 1);
            }
        }

        public IList<IList<int>> Subsets (int[] nums) {
            answer = new List<IList<int>> ();
            for (int i = 0; i < nums.Length; i++) {
                arr = new int[i];
                GetSubsets (0, nums.Length - 1, 0, nums);
            }
            answer.Add (nums);
            return answer;
        }

        private void GetSubsets (int l, int r, int n, int[] nums) {
            if (n == arr.Length) {
                answer.Add (arr.Clone () as int[]);
                return;
            }
            if (l > r)
                return;
            for (int i = l; i <= r; i++) {
                arr[n] = nums[i];
                GetSubsets (i + 1, r, n + 1, nums);
            }
        }

        public IList<IList<int>> SubsetsWithDup (int[] nums) {
            IList<IList<int>> answer = new List<IList<int>> ();
            Array.Sort (nums);
            for (int i = 0; i <= nums.Length; i++) {
                int[] arr = new int[i];
                for (int j = 0; j < i; j++) {
                    arr[j] = int.MaxValue;
                }
                GetSubsetsWithDup (0, nums.Length - 1, 0, nums, answer, arr);
            }
            return answer;
        }

        private void GetSubsetsWithDup (int l, int r, int n, int[] nums, IList<IList<int>> answer, int[] arr) {
            if (n == arr.Length) {
                answer.Add (arr.Clone () as int[]);
                return;
            }
            if (l > r)
                return;
            for (int i = l; i <= r; i++) {
                if (arr[n] == nums[i])
                    continue;
                arr[n] = nums[i];
                for (int j = n + 1; j < arr.Length - 1; j++)
                    arr[j] = int.MaxValue;
                GetSubsetsWithDup (i + 1, r, n + 1, nums, answer, arr);
            }
        }

        public bool Exist (char[, ] board, string word) {
            if (word.Length == 0)
                return false;
            var cs = word.ToCharArray ();
            for (int y = 0; y < board.GetLength (0); y++) {
                for (int x = 0; x < board.GetLength (1); x++) {
                    if (board[y, x] == word[0]) {
                        board[y, x] = '!';
                        if (BT_Exist (board, cs, x, y, 1))
                            return true;
                        board[y, x] = word[0];
                    }
                }
            }
            return false;
        }

        int[] delta_x = new int[] {-1, 0, 1, 0 };
        int[] delta_y = new int[] { 0, 1, 0, -1 };
        private bool BT_Exist (char[, ] board, char[] word, int x, int y, int index) {
            if (index == word.Length)
                return true;
            for (int i = 0; i < 4; i++) {
                int tempx = x + delta_x[i];
                int tempy = y + delta_y[i];
                if (tempx >= 0 && tempx < board.GetLength (1) && tempy >= 0 && tempy < board.GetLength (0) && board[tempy, tempx] == word[index]) {
                    board[tempy, tempx] = '!';
                    if (BT_Exist (board, word, tempx, tempy, index + 1))
                        return true;
                    board[tempy, tempx] = word[index];
                }
            }
            return false;
        }

        public IList<string> FindWords (char[, ] board, string[] words) {
            IList<string> answer = new List<string> ();
            TrieNode trie = TrieNode.buildTrie (words);
            for (int y = 0; y < board.GetLength (0); y++) {
                for (int x = 0; x < board.GetLength (1); x++) {
                    BT_FindWords (board, y, x, trie, answer);
                }
            }
            return answer;
        }

        private void BT_FindWords (char[, ] board, int y, int x, TrieNode node, IList<string> answer) {
            char c = board[y, x];
            if (c == '#' || node.next[c - 'a'] == null)
                return;
            node = node.next[c - 'a'];
            if (node.word != null) {
                answer.Add (node.word);
                node.word = null;
            }
            board[y, x] = '#';
            if (y > 0) BT_FindWords (board, y - 1, x, node, answer);
            if (x > 0) BT_FindWords (board, y, x - 1, node, answer);
            if (y < board.GetLength (0) - 1) BT_FindWords (board, y + 1, x, node, answer);
            if (x < board.GetLength (1) - 1) BT_FindWords (board, y, x + 1, node, answer);
            board[y, x] = c;
        }

        public int RemoveDuplicates (int[] nums) {
            int index = 1;
            int count = 0;
            if (nums.Length == 0)
                return count;
            int num = nums[0];
            count++;
            while (index < nums.Length) {
                if (nums[index] == num) {
                    nums[count] = num;
                    count++;
                    while (index < nums.Length && nums[index] == num) {
                        index++;
                    }
                } else {
                    num = nums[index];
                    nums[count] = num;
                    count++;
                    index++;
                }
            }
            return count;
        }

        public bool Search2 (int[] nums, int target) {
            int start = 0, end = nums.Length - 1, mid = -1;
            while (start <= end) {
                mid = (start + end) / 2;
                if (nums[mid] == target) {
                    return true;
                }
                if (nums[mid] < nums[end] || nums[mid] < nums[start]) {
                    if (target > nums[mid] && target <= nums[end]) {
                        start = mid + 1;
                    } else {
                        end = mid - 1;
                    }
                } else if (nums[mid] > nums[start] || nums[mid] > nums[end]) {
                    if (target < nums[mid] && target >= nums[start]) {
                        end = mid - 1;
                    } else {
                        start = mid + 1;
                    }
                } else {
                    end--;
                }
            }
            return false;
        }

        public ListNode DeleteDuplicates (ListNode head) {
            ListNode curr = head;
            //避免head被消掉。。
            ListNode start = new ListNode (0);
            //answer.next是结果的起点
            ListNode answer = start;
            while (curr != null) {
                if (start.next == null) {
                    start.next = curr;
                } else if (start.next.val == curr.val) {
                    int val = curr.val;
                    while (curr != null && curr.val == val)
                        curr = curr.next;
                    start.next = null;
                    continue;
                } else {
                    start = start.next;
                    start.next = curr;
                }
                curr = curr.next;
            }
            return answer.next;
        }

        public int LargestRectangleArea (int[] heights) {
            //用stack保存成阶梯型计算～
            Stack<int> stack = new Stack<int> ();
            int width = 0;
            int temp = 0;
            int max = 0;
            for (int i = 0; i <= heights.Length; i++) {
                //最后收尾计算面积
                if (i == heights.Length) {
                    width = 0;
                    while (stack.Any ()) {
                        width++;
                        temp = stack.Pop ();
                        max = Math.Max (temp * width, max);
                    }
                }
                //添加
                else if (!stack.Any () || stack.Peek () < heights[i])
                    stack.Push (heights[i]);
                //前面有较大的计算面积后变小保存
                else {
                    width = 0;
                    while (stack.Any () && stack.Peek () > heights[i]) {
                        width++;
                        temp = stack.Pop ();
                        max = Math.Max (temp * width, max);
                    }
                    while (width + 1 != 0) {
                        width--;
                        stack.Push (heights[i]);
                    }
                }
            }
            return max;
        }

        public int MaximalRectangle (char[, ] matrix) {
            int[, ] height = new int[matrix.GetLength (0), matrix.GetLength (1)];
            for (int y = 0; y < height.GetLength (0); y++) {
                for (int x = 0; x < height.GetLength (1); x++) {
                    if (matrix[y, x] == '1') {
                        height[y, x] = 1;
                        if (y - 1 >= 0)
                            height[y, x] += height[y - 1, x];
                    } else {
                        height[y, x] = 0;
                    }
                }
            }
            //同上题
            Stack<int> stack = new Stack<int> ();
            int width = 0;
            int temp = 0;
            int max = 0;
            for (int y = 0; y < height.GetLength (0); y++) {
                for (int x = 0; x <= height.GetLength (1); x++) {
                    //最后收尾计算面积
                    if (x == height.GetLength (1)) {
                        width = 0;
                        while (stack.Any ()) {
                            width++;
                            temp = stack.Pop ();
                            max = Math.Max (temp * width, max);
                        }
                    }
                    //添加
                    else if (!stack.Any () || stack.Peek () < height[y, x])
                        stack.Push (height[y, x]);
                    //前面有较大的计算面积后变小保存
                    else {
                        width = 0;
                        while (stack.Any () && stack.Peek () > height[y, x]) {
                            width++;
                            temp = stack.Pop ();
                            max = Math.Max (temp * width, max);
                        }
                        while (width + 1 != 0) {
                            width--;
                            stack.Push (height[y, x]);
                        }
                    }
                }
            }
            return max;
        }

        public ListNode Partition (ListNode head, int x) {
            ListNode less = new ListNode (0);
            ListNode head_less = less;
            ListNode left = new ListNode (0);
            ListNode head_left = left;
            left.next = head;
            var temp = left;
            while (temp.next != null) {
                if (temp.next.val < x) {
                    less.next = temp.next;
                    less = less.next;
                    temp.next = temp.next.next;
                } else
                    temp = temp.next;
            }
            less.next = head_left.next;
            return head_less.next;
        }

        public bool IsScramble (string s1, string s2) {
            if (s1.Length != s2.Length)
                return false;
            if (s1 == s2)
                return true;
            int l = s1.Length;
            var cs1 = s1.ToCharArray ();
            var cs2 = s2.ToCharArray ();
            //长度，s1的其实位置,s2的其实位置
            bool[, , ] dp = new bool[l + 1, l, l];
            for (int i = 1; i <= l; i++) {
                for (int y = 0; y <= l - i; y++) {
                    for (int x = 0; x <= l - i; x++) {
                        if (i == 1) {
                            dp[i, y, x] = cs1[y] == cs2[x];
                        } else {
                            //有n-1种拆分方法～
                            for (int j = 1; j < i; j++) {
                                //一种是没置换的，另一种是置换的..
                                if ((dp[j, y, x] && dp[i - j, y + j, x + j]) || (dp[j, y + i - j, x] && dp[i - j, y, x + j])) {
                                    dp[i, y, x] = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return dp[l, 0, 0];
        }

        public void Merge (int[] nums1, int m, int[] nums2, int n) {
            int index = m + n - 1;
            int index_1 = m - 1;
            int index_2 = n - 1;
            while (index_1 > -1 && index_2 > -1)
                nums1[index--] = nums1[index_1] >= nums2[index_2] ? nums1[index_1--] : nums2[index_2--];
            while (index_2 > -1)
                nums1[index--] = nums2[index_2--];
        }

        public ListNode MergeTwoLists (ListNode l1, ListNode l2) {
            ListNode curr = new ListNode (0);
            ListNode answer = curr;
            ListNode index1 = l1;
            ListNode index2 = l2;
            while (l1 != null && l2 != null) {
                if (l1.val <= l2.val) {
                    curr.next = l1;
                    l1 = l1.next;
                } else {
                    curr.next = l2;
                    l2 = l2.next;
                }
                curr = curr.next;
            }
            if (l1 == null) {
                curr.next = l2;
            }
            if (l2 == null) {
                curr.next = l1;
            }
            return answer.next;
        }

        public IList<int> GrayCode (int n) {
            IList<int> answer = new List<int> ();
            int value = 0;
            answer.Add (value);
            if (n == 0)
                return answer;
            int[] values = new int[n];
            bool[] check = new bool[n];
            values[0] = 1;
            for (int i = 1; i < n; i++) {
                values[i] = values[i - 1] * 2;
            }
            for (int i = 1; i < values[n - 1] * 2; i++) {
                for (int j = n - 1; j >= 0; j--) {
                    if (i % values[j] == 0) {
                        if (check[j]) {
                            value -= values[j];
                        } else {
                            value += values[j];
                        }
                        check[j] = !check[j];
                        break;
                    }
                }
                answer.Add (value);
            }
            return answer;
        }

        public int NumDecodings (string s) {
            if (s.Length == 0)
                return 0;
            int[] dp = new int[s.Length];
            var cs = s.ToCharArray ();
            for (int i = 0; i < dp.Length; i++) {
                if (i == 0) {
                    if (cs[i] != '0')
                        dp[i] = 1;
                } else if (i == 1) {
                    if (cs[i] != '0')
                        dp[i] = dp[i - 1];
                    if (check (cs, i - 1)) {
                        dp[i]++;
                    }
                } else {
                    if (cs[i] != '0')
                        dp[i] = dp[i - 1];
                    if (check (cs, i - 1)) {
                        dp[i] += dp[i - 2];
                    }
                }
            }
            return dp[s.Length - 1];
        }

        //检查两位数是否可以确认一个字母
        private bool check (char[] cs, int index) {
            if (cs[index] - '0' >= 3 || cs[index] == '0')
                return false;
            if (cs[index] - '0' == 2 && cs[index + 1] - '0' > 6)
                return false;
            return true;
        }

        public ListNode ReverseBetween (ListNode head, int m, int n) {
            Stack<int> stack = new Stack<int> ();
            if (m == n)
                return head;
            var curr = head;
            ListNode mark = null;
            for (int i = 1; i <= n; i++) {
                if (i == m)
                    mark = curr;
                if (i >= m)
                    stack.Push (curr.val);
                curr = curr.next;
            }
            for (int i = 0; i <= n - m; i++) {
                mark.val = stack.Pop ();
                mark = mark.next;
            }
            return head;

        }

        public IList<string> RestoreIpAddresses (string s) {
            IList<string> answer = new List<string> ();
            BT_IpAddress (answer, "", 0, 0, s.Length - 1, s);
            return answer;
        }

        private void BT_IpAddress (IList<string> answer, string s, int level, int l, int r, string cs) {
            int len = r - l + 1;
            if (4 - level > len || 4 * (4 - level) < len)
                return;
            if (level == 4) {
                answer.Add (s);
                return;
            }
            for (int i = 1; i <= 3 && i <= len; i++) {
                if (!CheckIP (cs.Substring (l, i))) {
                    continue;
                }
                BT_IpAddress (answer, s + cs.Substring (l, i) + (level == 3 ? "" : "."), level + 1, l + i, r, cs);
            }
        }

        private bool CheckIP (string ip) {
            int value = int.Parse (ip);
            if (value > 255)
                return false;
            if (ip.Length > 1 && ip[0] == '0')
                return false;
            return true;
        }

        public IList<int> InorderTraversal (TreeNode root) {
            IList<int> answer = new List<int> ();
            Stack<TreeNode> stack = new Stack<TreeNode> ();
            if (root == null)
                return answer;
            stack.Push (root);
            while (stack.Count != 0) {
                var temp = stack.Peek ();
                if (temp.left != null) {
                    stack.Push (temp.left);
                    temp.left = null;
                } else {
                    answer.Add (temp.val);
                    stack.Pop ();
                    if (temp.right != null) {
                        stack.Push (temp.right);
                    }
                }
            }
            return answer;
        }

        public bool IsInterleave (string s1, string s2, string s3) {
            if (s1.Length + s2.Length != s3.Length)
                return false;
            var cs1 = s1.ToCharArray ();
            var cs2 = s2.ToCharArray ();
            var cs3 = s3.ToCharArray ();
            int index_1 = 0, index_2 = 0, index_3 = 0;
            while (index_3 < s3.Length) {
                //优先匹配s1
                if (index_1 < cs1.Length && cs3[index_3] == cs1[index_1]) {
                    index_1++;
                    index_3++;
                }
                //s1匹配不了才匹配s2
                else if (index_2 < cs2.Length && cs3[index_3] == cs2[index_2]) {
                    index_3++;
                    index_2++;
                }
                //都匹配不了则回退～
                else {
                    //应为是优先匹配的s1,如果s2匹配光了则说明遍历尽了,GG
                    if (index_2 == s2.Length)
                        return false;
                    //回退cs1和cs3
                    while (cs3[index_3] != cs2[index_2]) {
                        index_1--;
                        index_3--;
                        //根本没匹配到～
                        if (index_1 < 0 || index_3 < 0)
                            return false;
                    }
                    index_3++;
                    index_2++;
                }
            }
            if (index_1 == cs1.Length && index_2 == cs2.Length)
                return true;
            return false;
        }

        //中序遍历比大小～～
        public bool IsValidBST (TreeNode root) {
            bool first = true;
            if (root == null)
                return true;
            int value = int.MinValue;
            Stack<TreeNode> stack = new Stack<TreeNode> ();
            stack.Push (root);
            while (stack.Count != 0) {
                var temp = stack.Peek ();
                if (temp.left != null) {
                    stack.Push (temp.left);
                    temp.left = null;
                } else {
                    if (first) {
                        first = false;
                        value = temp.val;
                    } else if (value >= temp.val) {
                        return false;
                    } else {
                        value = temp.val;
                    }
                    stack.Pop ();
                    if (temp.right != null) {
                        stack.Push (temp.right);
                    }
                }
            }
            return true;
        }

        public void RecoverTree (TreeNode root) {
            TreeNode pre = null;
            TreeNode first = null;
            TreeNode sec = null;
            DFS_RecoverTree (root, ref pre, ref first, ref sec);
            int temp = first.val;
            first.val = sec.val;
            sec.val = temp;
        }

        private void DFS_RecoverTree (TreeNode node, ref TreeNode pre, ref TreeNode first, ref TreeNode sec) {
            if (node == null)
                return;
            DFS_RecoverTree (node.left, ref pre, ref first, ref sec);
            if (pre == null)
                pre = node;
            else {
                if (node.val < pre.val) {
                    if (first == null) {
                        first = pre;
                        sec = node;
                    } else {
                        sec = node;
                    }
                }
                pre = node;
            }
            DFS_RecoverTree (node.right, ref pre, ref first, ref sec);
        }

        public IList<IList<int>> LevelOrder (TreeNode root) {
            var answer = new List<IList<int>> ();
            DFS_LevelOrder (root, 0, answer);
            return answer;
        }

        private void DFS_LevelOrder (TreeNode node, int level, IList<IList<int>> answer) {
            if (node == null)
                return;
            while (answer.Count <= level)
                answer.Add (new List<int> ());
            answer[level].Add (node.val);
            DFS_LevelOrder (node.left, level + 1, answer);
            DFS_LevelOrder (node.right, level + 1, answer);
        }

        public IList<IList<int>> ZigzagLevelOrder (TreeNode root) {
            var answer = LevelOrder (root);
            for (int i = 1; i < answer.Count; i += 2) {
                answer[i] = answer[i].Reverse ().ToArray ();
            }
            return answer;
        }

        public bool CheckPerfectNumber(int num)
        {   
            if(num==1)
                return false;
            int sum = 1;
            for(int i = 2;i<=Math.Sqrt(num);i++)
            {
                if(num%i==0)
                {
                    sum+=i+num/i;
                }
            }
            return sum==num;
        }

        public int LongestIncreasingPath(int[,] matrix)
        {   
            //利用dp来剪枝
            int[,] dp = new int[matrix.GetLength(0),matrix.GetLength(1)];
            int answer = 0;
            for(int y = 0;y<matrix.GetLength(0);y++)
            {
                for(int x = 0;x<matrix.GetLength(1);x++)
                {
                    BT_LIP(matrix,ref answer,1,x,y,dp);
                }
            }
            return answer;
        }

        private void BT_LIP(int[,] matrix,ref int answer,int count, int x, int y,int[,] dp)
        {
            if(dp[y,x]>=count)
                return;
            else
                dp[y,x] = count;
            answer = Math.Max(answer,count);
            if(x>0&&matrix[y,x-1]>matrix[y,x])
            {
                BT_LIP(matrix,ref answer,count+1,x-1,y,dp);
            }
            if(x<matrix.GetLength(1)-1&&matrix[y,x+1]>matrix[y,x])
            {
                BT_LIP(matrix,ref answer,count+1,x+1,y,dp);
            }
            if(y>0&&matrix[y-1,x]>matrix[y,x])
            {
                BT_LIP(matrix,ref answer,count+1,x,y-1,dp);
            }
            if(y<matrix.GetLength(0)-1&&matrix[y+1,x]>matrix[y,x])
            {
                BT_LIP(matrix,ref answer,count+1,x,y+1,dp);
            }
        }

        public string RemoveDuplicateLetters(string s) 
        {
            int[] count = new int[26];
            int[] check = new int[26];
            Queue<char> c = new Queue<char>();
            var cs = s.ToCharArray();
            for(int i =0;i<cs.Length;i++)
            {
                count[cs[i]-'a']++;
            }
            StringBuilder sb = new StringBuilder();
            for(int i =0;i<cs.Length;i++)
            {
                if(count[cs[i]-'a']==1)
                {
                    for(int j =0;j<cs[i]-'a';j++)
                    {
                        if(check[j]>0)
                        {
                            while(c.Peek()!=(char)('a'+j))
                            {
                                check[c.Peek()-'a']--;
                                c.Dequeue();
                            }
                            sb.Append((char)('a'+j));
                            check[j]=0;
                            count[j]=0;
                        }
                    }   
                    sb.Append(cs[i]);
                    check[cs[i]-'a']=0;
                    count[cs[i]-'a']=0;
                    while (c.Count>0&&c.Peek() != cs[i])
                    {
                        check[c.Peek() - 'a']--;
                        c.Dequeue();
                    }
                }
                else if(count[cs[i]-'a']>1)
                {
                    check[cs[i]-'a']++;
                    c.Enqueue(cs[i]);
                }
                count[cs[i]-'a']--;
            }
            return sb.ToString();
        }


        public int LongestPalindrome(string s)
        {
            Dictionary<char,int> dictionary = new Dictionary<char, int>();
            foreach(var c in s)
            {
                if(dictionary.ContainsKey(c))
                {
                    dictionary[c]++;
                }
                else
                {
                    dictionary[c]=1;
                }
            }
            bool mid = false;
            int len = 0;
            foreach(var key in dictionary.Keys)
            {
                if(dictionary[key]%2==0)
                {
                    len+=dictionary[key];
                }
                else
                {
                    if(!mid)
                    {
                        len+=dictionary[key];
                        mid = true;
                    }
                    else
                    {
                        len+=dictionary[key]-1;
                    }
                }
            }
            return len;
        }

        public int StrangePrinter(string s)
        {
            var cs = s.ToCharArray();
            int len = cs.Length;
                if (len == 0) return 0;
            int[,] dp = new int[len, len];
            for (int i = 0; i < len; i++)
            {
                dp[i, i] = 1;
            }
            for (int i = 1; i < len; i++)
            {
                for (int j = 0; j + i < len; j++)
                {
                    dp[j, j + i] = i + 1;
                    for (int k = j + 1; k <= j + i; k++)
                    {
                        int temp = dp[j, k - 1] + dp[k, j + i];
                        if (cs[k - 1] == cs[j + i]) temp--;
                        dp[j, j + i] = Math.Min(dp[j, j + i], temp);
                    }
                }

            }
            return dp[0,s.Length-1];
        }

        public int TotalHammingDistance(int[] nums)
        {
            int sum = 0;
            int count = 0;
            for (int j = 0; j < 32; j++)
            {
                for (int i = 0; i < nums.Length; i++)
                {

                    count += (nums[i] & 1);
                    nums[i] = nums[i] >> 1;
                }
                sum += count * (nums.Length - count);
                count = 0;
            }
            return sum;
        }

        public TreeNode BuildTree(int[] preorder, int[] inorder) {
            return BuildTree(preorder,0,preorder.Length-1,inorder,0,inorder.Length-1);
        }

        private TreeNode BuildTree(int[] preorder,int l,int r,int[] inorder,int l2,int r2)
        {
            if(l>r)
                return null;
            int rootvalue = preorder[l];
            int index = 0;
            for(int i = l2;i<=r2;i++)
            {
                if(inorder[i]==rootvalue)
                {
                    index = i;
                }
            }
            var root = new TreeNode(rootvalue);
            root.left = BuildTree(preorder,l+1,l+index-l2,inorder,l2,index-1);
            root.right = BuildTree(preorder,l+index-l2+1,r,inorder,index+1,r2);
            return root;
        }

        public TreeNode BuildTree2(int[] inorder, int[] postorder) 
        {
            return BuildTree2(inorder,postorder,0,0,inorder.Length);
        }

        private TreeNode BuildTree2(int[] inorder,int[] postorder,int l1,int l2,int len)
        {
            if(len<1)
                return null;
            int index = 0;
            var root = new TreeNode(postorder[l2+len-1]);
            for(int i = l1;i<l1+len;i++)
            {
                if(inorder[i]==root.val)
                {
                    index = i;
                    break;
                }
            }
            root.left = BuildTree2(inorder,postorder,l1,l2,index-l1);
            root.right = BuildTree2(inorder,postorder,index+1,l2+index-l1,len-index+l1-1);
            return root;

        }

        public TreeNode SortedListToBST(ListNode head) {
            List<int> list = new List<int>();
            while(head!=null)
            {
                list.Add(head.val);
                head = head.next;
            }
            var arr = list.ToArray();
            return SortedListToBST(arr,0,arr.Length-1);
        }

        private TreeNode SortedListToBST(int[] arr,int l,int r)
        {
            if(l>r)
                return null;
            int mid = (l+r)/2;
            TreeNode node = new TreeNode(arr[mid]);
            node.left = SortedListToBST(arr,l,mid-1);
            node.right = SortedListToBST(arr,mid+1,r);
            return node;
        }


        //其实可以非递归
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            var answer = new List<IList<int>>();
            var stack = new Stack<int>();
            if(root==null)
                return answer;
            stack.Push(root.val);
            dfs(root,answer,stack,sum-root.val);
            return answer;
        }

        public void dfs(TreeNode node,IList<IList<int>> answer,Stack<int> curr,int sum)
        {
            if(node == null)
                return;
            if(node.left==null&&node.right==null)
            {
                if(sum==node.val)
                {
                    curr.Push(node.val);
                    //这块略尴尬
                    var temp = curr.ToList();
                    temp.Reverse();
                    answer.Add(temp);
                    curr.Pop();
                }
                return;
            }
            curr.Push(node.val);
            dfs(node.left,answer,curr,sum-node.val);
            dfs(node.right,answer,curr,sum-node.val);
            curr.Pop();
        }

        public void Flatten(TreeNode root)
        {
            TreeNode curr = root;
            TreeNode temp = null;
            while(curr!=null)
            {
                if(curr.left!=null)
                {
                    temp = curr.left;
                    while(temp.right!=null)
                    {
                        temp = temp.right;
                    }
                    temp.right = curr.right;
                    curr.right = curr.left;
                    curr.left = null;
                }
                curr = curr.right;
            }
        }

        public int NumDistinct(string s, string t)
        {
            var cs_s = s.ToCharArray();
            var cs_t = t.ToCharArray();
            int[,] dp = new int[t.Length+1,s.Length+1];
            for(int i =0;i<s.Length+1;i++)
            {
                dp[0,i]=1;
            }
            for(int y =1;y<t.Length+1;y++)
            {
                for(int x=y;x<s.Length+1;x++)
                {
                    if(cs_t[y-1]==cs_s[x-1])
                    {
                        dp[y,x] = dp[y-1,x-1]+dp[y,x-1];
                    }
                    else
                    {
                        dp[y,x] = dp[y,x-1];
                    }
                }
            }
            return dp[t.Length,s.Length];
        }

        public string ShortestPalindrome(string s)
        {
            var rev_s = new string(s.Reverse().ToArray());
            var new_s = s+"#"+rev_s;
            var next = getTable(new_s);
            int len = next[next.Length-1];
            var postfix = s.Substring(len,s.Length-len);
            var profix = new string(postfix.Reverse().ToArray());
            return profix+s.Substring(0,len)+postfix; 
            
        }

        public int[] getTable(String s)
        {
            var cs = s.ToCharArray();
            //get lookup table
            int[] table = new int[s.Length];

            //pointer that points to matched char in prefix part

            int index = 0;
            //skip index 0, we will not match a string with itself
            for (int i = 1; i < s.Length; i++)
            {
                if (s[index] == s[i])
                {
                    //we can extend match in prefix and postfix
                    table[i] = table[i - 1] + 1;
                    index++;
                }
                else
                {
                    //match failed, we try to match a shorter substring

                    //by assigning index to table[i-1], we will shorten the match string length, and jump to the 
                    //prefix part that we used to match postfix ended at i - 1
                    index = table[i - 1];

                    while (index > 0 && s[index] != s[i])
                    {
                        //we will try to shorten the match string length until we revert to the beginning of match (index 1)
                        index = table[index - 1];
                    }

                    //when we are here may either found a match char or we reach the boundary and still no luck
                    //so we need check char match
                    if (s[index] == s[i])
                    {
                        //if match, then extend one char 
                        index++;
                    }

                    table[i] = index;
                }

            }

            return table;
        }


        public int MinimumDeleteSum(string s1, string s2)
        {
            int[,] dp = new int[s1.Length+1,s2.Length+1];
            for(int y=1;y<s1.Length+1;y++)
            {
                dp[y,0] = s1[y-1]+dp[y-1,0];
            }
            for (int x = 1;x < s2.Length + 1; x++)
            {
                dp[0, x] = s2[x-1]+dp[0,x-1];
            }
            for(int y = 1;y<s1.Length+1;y++)
            {
                for(int x = 1;x<s2.Length+1;x++)
                {
                    if(s1[y-1]==s2[x-1])
                    {
                        dp[y,x]=dp[y-1,x-1];
                    }
                    else
                    {
                        dp[y,x]  = dp[y-1,x-1]+s1[y-1]+s2[x-1];
                        dp[y,x] = Math.Min(dp[y-1,x]+s1[y-1],dp[y,x]);
                        dp[y,x] = Math.Min(dp[y,x-1]+s2[x-1],dp[y,x]);
                    }
                }
            }
            return dp[s1.Length,s2.Length];
        }

        public bool HasAlternatingBits(int n)
        {
            if (n == 0) return true;
            int m = n >> 1;
            int k = (m ^ n) + 1;
            while (k > 1)
            {
                if (k % 2 == 1)
                    return false;

                k /= 2;
            }

            return true;
        }

        public bool IsOneBitCharacter(int[] bits)
        {
            bool last = false;
            for(int i = 0;i<bits.Length;i++)
            {
                if(i==bits.Length-1)
                {
                    if(last)
                    {
                        return false;
                    }
                    return true;
                }
                if(last)
                {
                    last = false;
                }
                else
                {
                    if(bits[i]==1)
                    {
                        last = true;
                    }
                    else
                    {
                        last =false;
                    }
                }
            }
            return false;
        }

        public int MaxAreaOfIsland(int[,] grid)
        {
            int area = 0;
            int max = 0;
            for(int y =0;y<grid.GetLength(0);y++)
            {
                for(int x =0;x<grid.GetLength(1);x++)
                {
                    if(grid[y,x]==1)
                    {
                    helperMAoI(grid,y,x,ref area);
                    max = Math.Max(max,area);
                    area = 0;
                    }
                }
            }
            return max;
        }

        public void helperMAoI(int[,] grid,int y,int x,ref int area)
        {
            if(grid[y,x]!=1)
                return;
            area++;
            grid[y,x]=0;
            if(y>0)
            {
                helperMAoI(grid,y-1,x,ref area);
            }
            if(x>0)
            {
                helperMAoI(grid,y,x-1,ref area);
            }
            if(y<grid.GetLength(0)-1)
            {
                helperMAoI(grid,y+1,x,ref area);
            }
            if (x < grid.GetLength(1) - 1)
            {
                helperMAoI(grid, y, x+1, ref area);
            }
        }

        public int CountBinarySubstrings(string s)
        {
            int answer =0;
            int pre=0;
            int cur=1;
            for (int i = 1; i < s.Length; i++)
            {
                if (s[i - 1] == s[i])
                {
                    cur++;
                }
                else
                {
                    pre = cur;
                    cur = 1;
                }
                if (pre>=cur)
                {
                    answer++;
                }
            }
            return answer;
        }

}}
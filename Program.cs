using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leetcode;

namespace Leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution.Multiply("111","123");
        }
    }



    public class Solution
    {

        //每k个节点翻转，不足跳过
        public static ListNode ReverseKGroup(ListNode head, int k)
        {
            ListNode index = head;
            int[] cache = new int[k];
            while(true)
            {
                ListNode temp = index;
                for(int i = 0;i<k;i++)
                {
                    if(index==null)
                        return head;
                    cache[i] = index.val;
                    index = index.next;
                }
                cache = cache.Reverse().ToArray();
                for(int i = 0;i<k;i++)
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
            if(needles.Length==0)
                return 0;
            if(haystacks.Length<needles.Length)
                return -1;
            for(int i=0;i<haystacks.Length-needles.Length+1;i++)
            {
                for(int j = 0;j<needles.Length;j++)
                {
                    if(haystacks[i+j]!=needles[j])
                        break;
                    if(j==needles.Length-1)
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
            Dictionary<string,int> template = new Dictionary<string,int>();
            foreach(var word in words)
            {
                if(template.ContainsKey(word))
                    template[word]+=1;
                else
                    template.Add(word,1);
            } 
            for(int i = 0;i<s.Length-words.Length*wordLength+1;i++)
            {
                int j = i;
                Dictionary<string,int> temp = new Dictionary<string, int>();
                foreach(var key in template.Keys)
                {
                    temp.Add(key,template[key]);
                }
                while(true)
                {
                    string w = s.Substring(j,wordLength);
                    j+=wordLength;
                    if(!temp.ContainsKey(w))
                        break;
                    if(temp[w]<1)
                        break;
                    temp[w]--;
                    bool flag = true;
                    foreach(var key in temp.Keys)
                    {
                        if(temp[key]!=0)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if(flag)
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
            if(nums.Length<1)
                return -1;
            int left = 0,right = nums.Length-1;
            while(right-left>1)
            {
                int mid = (left+right)/2;
                if(nums[mid]==target)
                    return mid;
                if(nums[left]>nums[mid])
                {
                    if(target>=nums[left]||target<=nums[mid])
                    {
                        right=mid;
                    }
                    else
                    {
                        left = mid;
                    }
                }
                else
                {
                    if(target>=nums[mid]||(target<=nums[right]&&nums[right]<nums[mid]))
                    {
                        left = mid;
                    }
                    else
                    {
                        right = mid;
                    }
                }
            }
            if(nums[left]==target)
                return left;
            if(nums[right]==target)
                return right;
            return -1;
        }

        //神烦的二分查找×2
        public static int[] SearchRange(int[] nums, int target) 
        {
            int left = 0;
            int right = nums.Length-1;
            int[] answer = new int[]{-1,-1};
            if(nums.Length==0)
                return answer;
            int target_l = left;
            int target_r = right;
            while(target_l<=right)
            {
                int mid = (target_l+right)/2;
                if(nums[mid]==target&&(mid==0||nums[mid-1]!=target))
                {
                    target_l = mid;
                    answer[0] = mid;
                    break;                    
                }
                if(nums[mid]==target)
                {
                    right = mid-1;
                    answer[0]=mid;
                }
                else if(nums[mid]>target)
                {
                    right = mid-1;
                }
                else if(nums[mid]<target)
                {
                    target_l = mid+1;
                }
            }
            left = target_l;
            while(left<=target_r)
            {
                int mid = (left+target_r)/2;
                if(nums[mid]==target&&(mid==nums.Length-1||nums[mid+1]!=target))
                {
                    target_r = mid;
                    answer[1] = mid;
                    break;                    
                }
                if(nums[mid]>target)
                {
                    target_r = mid-1;
                }
                else if(nums[mid]==target)
                {
                    left = mid+1;
                    answer[1] = mid;
                }
                else
                {
                    left = mid+1;
                }
            }
            return answer;
        }

        //很迷的题
        public static string CountAndSay(int n) 
        {
            string answer = "1";
            for(int i =1;i<n;i++)
            {
                StringBuilder sb = new StringBuilder();
                var temp = answer.ToCharArray();
                char curr = temp[0];
                int count = 0;
                foreach(var c in temp)
                {
                    if(c==curr)
                        count++;
                    else
                    {
                        sb.Append(count.ToString()+curr);
                        curr=c;
                        count=1;
                    }
                }
                sb.Append(count.ToString()+curr);
                answer = sb.ToString();
            }
            return answer;
        }   

        public IList<IList<int>> CombinationSum(int[] candidates, int target) {
            return null;
        }

        //对号入座，看缺什么
        public static int FirstMissingPositive(int[] nums) 
        {
            for(int i = 0;i<nums.Length;i++)
            {
                if(nums[i]==i+1)
                    continue;
                if(nums[i]>nums.Length||nums[i]<1)
                    continue;
                int temp = nums[nums[i]-1];
                nums[nums[i]-1]=nums[i];
                if(nums[i]!=temp)
                {
                    nums[i]=temp;
                    i--;
                }
            }
            for(int i=0;i<nums.Length;i++)
            {
                if(i+1!=nums[i])
                    return i+1;
            }
            return nums.Length+1;
        }


        //...
        public int Trap(int[] height) 
        {
            if(height.Length==0)
                return 0;
            int left = 0,right = height.Length-1;
            int Height_L = height[left],Height_R = height[right];
            int answer = 0;
            while(left<right)
            {
                if(Height_L<=Height_R)
                {
                    left++;
                    if(height[left]>Height_L)
                    {
                        Height_L = height[left];
                    }
                    else
                    {
                        answer+=Height_L - height[left];
                    }
                }
                else
                {
                    right--;
                    if(height[right]>Height_R)
                    {
                        Height_R = height[right];
                    }
                    else
                    {
                        answer+=Height_R - height[right];
                    }
                }
            }
            return answer;
        }

        //转成数组 小学乘法步骤
        public static string Multiply(string num1, string num2) 
        {
            int[] result = new int[num1.Length+num2.Length];
            int[] x = new int[num1.Length];
            int[] y = new int[num2.Length];
            for(int i = 0;i<x.Length;i++)
            {
                x[i]=int.Parse(num1[i].ToString());
            }
            for(int i = 0;i<y.Length;i++)
            {
                y[i]=int.Parse(num2[i].ToString());
            }
            for(int i = x.Length-1;i>=0;i--)
            {
                for(int j = y.Length-1;j>=0;j--)
                {
                    int temp = result.Length-(x.Length+y.Length-i-j-2)-1;
                    result[temp]+=x[i]*y[j];
                    result[temp-1]+=result[temp]/10;
                    result[temp]%=10;
                }
            }
            int index = 0;
            while(result[index]==0)
            {
                index++;
                if(index>result.Length-1)
                    return "0";
            }
            StringBuilder sb =new StringBuilder();
            for(int i = index ;i<result.Length;i++)
            {
                sb.Append(result[i].ToString());
            }
            return sb.ToString();
        }
    }
}
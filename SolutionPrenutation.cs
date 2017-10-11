using System;
using System.Collections.Generic;
using System.Text;
using Leetcode;
namespace Leetcode
{
    public class Prenutation
    {
        static bool[] check;
        static IList<IList<int>> answer;

        //这算回溯
        //还是递归爽！
        public static IList<IList<int>> Permute(int[] nums) 
        {
            answer = new List<IList<int>>();
            check = new bool[nums.Length];
            Helper(new int[nums.Length],0,ref nums);
            return answer;
        }

        public static void Helper(int[] num,int index,ref int[] nums)
        {
            if(index==num.Length)
            {
                answer.Add((int[])num.Clone());
                return;
            }
            for(int i=0;i<num.Length;i++)
            {
                if(check[i])
                    continue;
                check[i]=true;
                num[index] = nums[i];
                Helper(num,index+1,ref nums);
                check[i]=false;
            }
        }

        public static IList<IList<int>> PermuteUnique(int[] nums) 
        {
            Array.Sort(nums);
            answer = new List<IList<int>>();
            check = new bool[nums.Length];
            Helper2(new int[nums.Length],0,ref nums);
            return answer;
        }

        public static void Helper2(int[] num,int index,ref int[] nums)
        {
            if(index==num.Length)
            {
                answer.Add((int[])num.Clone());
                return;
            }
            for(int i=0;i<num.Length;i++)
            {
                if(check[i]||(
                    //和I就多了这个判断 和 排序
                (i>0)&&(nums[i]==nums[i-1])&&!check[i]&&!check[i-1]
                ))
                    continue;
                check[i]=true;
                num[index] = nums[i];
                Helper2(num,index+1,ref nums);
                check[i]=false;
            }
        }
        
        //看数型，找规律。
        public void NextPermutation(int[] nums) 
        {
            int i=nums.Length-1;
            for(;i>=1;i--)
            {
                if(nums[i]>nums[i-1])
                {
                    int j=i;
                    while(j<nums.Length&&nums[j]>nums[i-1])  
                        j++;
                    swap(nums,i-1,j-1);
                    break;
                }
            }
            for(int j=nums.Length-1;i<j;j--,i++)    
                swap(nums,i,j);//reverse array
        }
    
        public static void swap(int[] nums, int i, int j){
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public string GetPermutation(int n, int k) 
        {
            int[] nums = new int[n];
            for(int i = 0;i<=n;i++)
            {
                nums[i]=i+1;
            }
            for(int i=0;i<k;i++)
                NextPermutation(nums);
            StringBuilder sb =new StringBuilder();
            for(int i = 0;i<=n;i++)
            {
                sb.Append(nums[i].ToString());
            }
            return sb.ToString();
        }
    }
}
using System;
using System.Linq;
namespace Leetcode
{
    class TraceBack
    {
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
            int sum = nums.Sum();
            Array.Sort(nums);
            nums = nums.Reverse().ToArray();
            int subsum = sum/k;
            if(subsum*k!=sum)
                return false;
            bool[] used = new bool[nums.Length];
            while(k>0)
            {
                k--;
                bool flag = false;
                BT_CPK(subsum,0,used,nums,ref flag);
                if(!flag)
                    return false;
                else
                    flag = false;
            }
            return true;
        }

        private void BT_CPK(int left,int index,bool[] check,int[] nums,ref bool flag)
        {
            if (left == 0)
            {
                flag = true;
                return;
            }
            for(int i = index;i<nums.Length;i++)
            {
                if(nums[i]>left||check[i])
                {
                    continue;
                }
                check[i] = true;
                    BT_CPK(left-nums[i],i,check,nums,ref flag);
                if(flag)
                    return;
                check[i] = false;
            }
        }
    }
}
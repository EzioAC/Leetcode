using System;

namespace Leetcode
{
    class Recursion
    {
        public int LongestUnivaluePath(TreeNode root)
        {
            int answer = 0;
            LUP_Helper(root,ref answer,0);
            return answer;
        }
        private int LUP_Helper(TreeNode root,ref int answer,int val)
        {
            if(root==null)
                return 0;
            int l = LUP_Helper(root.left,ref answer,root.val);
            int r = LUP_Helper(root.right,ref answer,root.val);
            answer = Math.Max(answer,l+r);
            return root.val==val?Math.Max(l,r)+1:0;
        }
    }
}
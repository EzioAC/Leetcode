using System.Collections.Generic;

namespace Leetcode
{
public class BSTIterator {

    Stack<TreeNode> stack;
    public BSTIterator(TreeNode root) {
        stack = new Stack<TreeNode>();
        while(root!=null)
        {
            stack.Push(root);
            root = root.left;
        }
    }


    /** @return whether we have a next smallest number */
    public bool HasNext() {
        return stack.Count!=0;
    }

    /** @return the next smallest number */
    public int Next() {
        var temp = stack.Pop();
        int answer = temp.val;
        if(temp.right!=null)
        {
            temp = temp.right;
            while(temp!=null)
            {
                stack.Push(temp);
                temp = temp.left;
            }
        }
        return answer;
    }
}
}
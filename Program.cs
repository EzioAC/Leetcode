using System;
using System.Collections.Generic;
using System.Linq;
using Leetcode;

namespace Leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            
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




    }
}

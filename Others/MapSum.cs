namespace Leetcode.MapSum
{
    public class MapSum
    {

        /** Initialize your data structure here. */
        TrieNode root;
        public MapSum()
        {
            root = new TrieNode();
        }

        public void Insert(string key, int val)
        {
            TrieNode.Add(key,val,this.root);
        }

        public int Sum(string prefix)
        {
            return TrieNode.Find(prefix,this.root);
        }
    }

    public class TrieNode
    {
        public TrieNode[] next = new TrieNode[26];
        int count = 0;

        bool end = false;
        int wordcount = 0;

        public static void Add(string words,int count,TrieNode root)
        {
            TrieNode temp = root;
            temp.count+=count;
            foreach(var c in words)
            {  
                if(temp.next[c-'a']==null)
                {
                    temp.next[c-'a'] = new TrieNode();
                }
                temp.next[c-'a'].count+=count;
                temp = temp.next[c-'a'];
            }
            if(temp.end)
            {
                var temp2 = root;
                root.count-=temp.wordcount;
                foreach (var c in words)
                {
                    temp2.next[c - 'a'].count -= temp.wordcount;;
                    temp2 = temp2.next[c - 'a'];
                }
            }
            temp.end = true;
            temp.wordcount = count;
        }

        public static int Find(string words,TrieNode root)
        {
            TrieNode temp = root;
            foreach(var c in words)
            {
                if(temp.next[c-'a']==null)
                    return 0;
                temp = temp.next[c-'a'];
            }
            return temp.count;
        }
    }
}
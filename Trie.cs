
namespace Leetcode
{
    public class TrieNode
    {
        public TrieNode[] next = new TrieNode[26];
        public string word;
        public static TrieNode buildTrie(string[] words)
        {
            TrieNode root = new TrieNode();
            foreach (string w in words)
            {
                TrieNode p = root;
                foreach (char c in w.ToCharArray())
                {
                    int i = c - 'a';
                    if (p.next[i] == null) p.next[i] = new TrieNode();
                    p = p.next[i];
                }
                p.word = w;
            }
            return root;
        }
    }
}
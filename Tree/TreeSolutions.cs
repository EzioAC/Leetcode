using System.Collections.Generic;
using System.Linq;

namespace Leetcode.Tree
{
    public class TreeSolutions
    {
        public int[] FindRedundantConnection(int[,] edges)
        {
            int N = edges.GetLength(0);
            int[] roots = new int[N + 1];

            for (int i = 0; i < N; i++)
            {
                int leftroot = GetRoot(roots, edges[i, 0]);
                int rightroot = GetRoot(roots, edges[i, 1]);
                if (leftroot == rightroot)
                {
                    return new int[] { edges[i, 0], edges[i, 1] };
                }
                else
                {
                    roots[rightroot] = leftroot;
                }
            }

            return null;
        }

        private int GetRoot(int[] roots, int i)
        {
            if (roots[i] == 0)
            {
                return i;
            }
            else
            {
                int result = GetRoot(roots, roots[i]);
                roots[i] = result;
                return result;
            }
        }

    }
}
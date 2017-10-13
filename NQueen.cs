using System.Collections.Generic;
using System.Text;

namespace Leetcode
{
    class NQueen
    {
        bool[,] board;
        bool[] check_v, check_h;
        bool[] check_l, check_r;
        IList<IList<string>> answer;
        public IList<IList<string>> SolveNQueens(int n)
        {
            answer = new List<IList<string>>();
            board = new bool[n, n];
            check_v = new bool[n];
            check_h = new bool[n];
            check_l = new bool[2 * n - 1];
            check_r = new bool[2 * n - 1];
            Put(0);
            return answer;
        }

        int count;
        public int TotalNQueens(int n)
        {
            count = 0;
            board = new bool[n, n];
            check_v = new bool[n];
            check_h = new bool[n];
            check_l = new bool[2 * n - 1];
            check_r = new bool[2 * n - 1];
            Put2(0);
            return count;
        }

        private void Put(int depth)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (Check(i, depth))
                {
                    Mark(i, depth);
                    if (depth == board.GetLength(0) - 1)
                    {
                        answer.Add(GetBoard(board));
                    }
                    else
                        Put(depth + 1);
                    Unmark(i, depth);
                }
            }
        }

        private void Put2(int depth)
        {
            for (int i = 0; i < check_v.Length; i++)
            {
                if (Check(i, depth))
                {
                    Mark(i, depth);
                    if (depth == check_v.Length - 1)
                    {
                        count++;
                    }
                    else
                        Put2(depth + 1);
                    Unmark(i, depth);
                }
            }
        }

        private bool Check(int x, int y)
        {
            return !check_h[x] && !check_v[y] && !check_r[x - y + check_h.Length - 1] && !check_l[x + y];
        }

        private void Mark(int x, int y)
        {
            board[y, x] = true;
            check_h[x] = true;
            check_v[y] = true;
            check_r[x - y + check_h.Length - 1] = true;
            check_l[x + y] = true;
        }

        private void Unmark(int x, int y)
        {
            board[y, x] = false;
            check_h[x] = false;
            check_v[y] = false;
            check_r[x - y + check_h.Length - 1] = false;
            check_l[x + y] = false;
        }

        private IList<string> GetBoard(bool[,] board)
        {
            StringBuilder sb = new StringBuilder();
            int n = board.GetLength(0);
            IList<string> answer = new string[n];
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    sb.Append(board[y, x] ? 'Q' : '.');
                }
                answer[y] = sb.ToString();
                sb.Clear();
            }
            return answer;
        }
    }
}
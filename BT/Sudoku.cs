using System.Collections.Generic;
using System.Linq;
using System;



namespace Leetcode.BT
{
    class Sudoku
    {
        public bool IsValidSudoku(char[,] board)
        {
            bool[,] h = new bool[9,9],v = new bool[9,9],sec = new bool[9,9];
            for(int y = 0;y<9;y++)
            {
                for(int x = 0;x<9;x++)
                {
                    if(board[y,x]!='.')
                    {
                        int index = board[y,x]-'1';
                        if(h[y,index]||v[x,index]||sec[y/3*3+x/3,index])
                            return false;
                        h[y,index] = true;
                        v[x,index] = true;
                        sec[y/3*3+x/3,index] = true; 
                    }
                }
            }
            return true;
        }

        public void SolveSudoku(char[,] board)
        {
            bool answer = false;
            bool[,] h = new bool[9,9],v = new bool[9,9],sec = new bool[9,9];
            for(int y = 0;y<9;y++)
            {
                for(int x = 0;x<9;x++)
                {
                    if(board[y,x]!='.')
                    {
                        int index = board[y,x]-'1';
                        h[y,index] = true;
                        v[x,index] = true;
                        sec[y/3*3+x/3,index] = true; 
                    }
                }
            }
            Try(board,h,v,sec,ref answer,0,0);
        }
        private void Try(char[,] board,bool[,] h,bool[,] v,bool[,] sec,ref bool answer,int y,int x)
        {
            for (int i = y; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == '.')
                    {
                        for (int z = 0; z < 9 && !answer; z++)
                        {
                            if (!h[i, z] && !v[j, z] && !sec[i / 3 * 3 + j / 3, z] && !answer)
                            {
                                h[i, z] = true;
                                v[j, z] = true;
                                sec[i / 3 * 3 + j / 3, z] = true;
                                board[i, j] = (char)('1' + z);

                                Try(board, h, v, sec, ref answer, i, j);

                                if (answer)
                                    return;

                                h[i, z] = false;
                                v[j, z] = false;
                                sec[i / 3 * 3 + j / 3, z] = false;
                                board[i, j] = '.';
                            }
                        }
                        return;
                    }
                }
            }
            answer = true;
        }

        public int CountArrangement(int N)
        {
            int[] arr = new int[N+1];
            for(int i =0;i<arr.Length;i++)
            {
                arr[i] = i;
            }
            int count = 0;
            Put(arr,1,ref count);
            return count;
        }

        private void Put(int[] arr,int index,ref int count)
        {
            if (index >= arr.Length)
            {
                count++;
                return;
            }
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != i)
                    continue;
                if (arr[i] % index == 0 || index % arr[i] == 0)
                {
                    arr[i] = 0;
                    Put(arr, index + 1, ref count);
                    arr[i] = i;
                }
            }
        }
    }
}
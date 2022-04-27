using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{
    public class Board
    {
        public List<Node> children;
        Tuple<int, int> zero;
        Node board;
        public int[] checkBoard;
        int[,] tiles;
        int N;
        int NoOfMoves = 0;


        public Board(Node board, int size)
        {
            int init = 1;
            tiles = new int[size, size];
            children = new List<Node>();
            checkBoard = new int[size * size];
            int ind = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (board.data[i, j] == 0)
                    {
                        zero = new Tuple<int, int>(i, j);
                    }
                    if (i == size - 1 && j == size - 1)
                        init = 0;
                    tiles[i,j] = init;
                    init++;
                    checkBoard[ind] = board.data[i, j];
                    ind++;
                }
            }


            this.board = board;
            N = size;
        }

        public bool isSolvable()
        {
            int inversions = 0;
            int blankLine = N - zero.Item1;
            
            for (int i =0;i<N*N;i++)
            {
                for (int j = i+1; j < N * N; j++)
                {
                    if (checkBoard[j] > 0 && checkBoard[i] > 0 && checkBoard[i] > checkBoard[j])
                        inversions++;
                }
            }
            if (N % 2 == 1)
            {
                if (inversions % 2 == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if ((inversions % 2 == 0 && blankLine % 2 == 1) ||
                    (inversions % 2 == 1 && blankLine % 2 == 0) )
                    return true;
                else
                    return false;
            }
        }
        int Hamming()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board.data[i, j] != tiles[i, j] && board.data[i, j] != 0 )
                    {
                        count++;
                    }
                }
            }
            return count;

        }

        int Manhattan()
        {
            int count = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (board.data[i, j] != tiles[i, j] && board.data[i, j] != 0)
                    {
                        int x = (board.data[i, j] - 1) / N;
                        int y = (board.data[i, j] - 1) % N;
                        count += Math.Abs(x - i) + Math.Abs(y - j);
                    }
                }
            }
            return count;
        }

        public bool isGoal()
        {
            if (Hamming() == 0)
                return true;

            return false;
        }

        public void Swap(Tuple<int, int> tile)
        {
            int temp = board.data[zero.Item1, zero.Item2];
            board.data[zero.Item1, zero.Item2] = board.data[tile.Item1, tile.Item2];
            board.data[tile.Item1, tile.Item2] = temp;
            zero = tile;

        }

        public int FakeSwap(Tuple<int, int> zero, Tuple<int, int> tile)
        {
            int h = -1;
            int m = -1;
            int total = -1;
            try
            {
                int temp = board.data[zero.Item1, zero.Item2];
                board.data[zero.Item1, zero.Item2] = board.data[tile.Item1, tile.Item2];
                board.data[tile.Item1, tile.Item2] = temp;


                //h = Hamming();
                m = Manhattan();
                total = m;
                children.Add(new Node(board.data, total + board.level + 1, board.level + 1));
                int temp1 = board.data[zero.Item1, zero.Item2];
                board.data[zero.Item1, zero.Item2] = board.data[tile.Item1, tile.Item2];
                board.data[tile.Item1, tile.Item2] = temp1;
                return total;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void Move(string direction)
        {
            NoOfMoves++;
            if (direction == "up")
            {
                Swap(new Tuple<int, int>(zero.Item1 - 1, zero.Item2));
            }
            else if (direction == "down")
            {
                Swap(new Tuple<int, int>(zero.Item1 + 1, zero.Item2));
            }
            else if (direction == "left")
            {
                Swap(new Tuple<int, int>(zero.Item1, zero.Item2 - 1));
            }
            else if (direction == "right")
            {
                Swap(new Tuple<int, int>(zero.Item1, zero.Item2 + 1));
            }
        }

        public void PrintBoard()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(board.data[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        public void FindPossibleMoves()
        {
            FakeSwap(zero, new Tuple<int, int>(zero.Item1 + 1, zero.Item2));
            
            FakeSwap(zero, new Tuple<int, int>(zero.Item1 - 1, zero.Item2));
            
            FakeSwap(zero, new Tuple<int, int>(zero.Item1, zero.Item2 + 1));
            
            FakeSwap(zero, new Tuple<int, int>(zero.Item1, zero.Item2 - 1));
            
            

        }
        public void PrintnoOfMoves()
        {
            Console.WriteLine("No of moves: " + NoOfMoves);
        }


    }



}

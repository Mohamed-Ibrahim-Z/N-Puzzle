using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace algo_project
{
    internal class main
    {
        static int hashFunctionFor2DArr(int[,] arr)
        {
            int hash = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    hash = (hash * 31 + arr[i, j]) % 1000000007;
                }
                hash *= 7;
            }
            return hash;
        }

        
        static void Main(string[] args)
        {
            string[] read = System.IO.File.ReadAllLines("Sample Test/Solvable Puzzles/8 Puzzle (3).txt");
            int n = int.Parse(read[0]);
            int[,] puzzle = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] line = read[i + 2].Split(' ');
                for (int j = 0; j < n; j++)
                {
                    puzzle[i, j] = int.Parse(line[j]);
                }
            }
            

            PrQueue open = new PrQueue();
            HashSet<int> closed = new HashSet<int>();
            Node node = new Node(puzzle, 0, 0);
            open.Enqueue(node);
            Board board = new Board(node, n);
            if(!board.isSolvable())
            {
                Console.WriteLine("Unsolvable");
                return;
            }

            while (open.Count() != 0)
            {
                Node current = open.Dequeue();
                Board board1 = new Board(current, n);
                board1.PrintBoard();
                if (board1.isGoal())
                {
                    Console.WriteLine(current.level);
                    break;
                }
                
                closed.Add(hashFunctionFor2DArr(current.data));
                board1.FindPossibleMoves();
                foreach(Node child in board1.children)
                {

                    if (!closed.Contains(hashFunctionFor2DArr(child.data)))
                    {
                        open.Enqueue(child);
                    }
                    else
                        continue;
                }
            }



        }
    }
}

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
            string[] read = System.IO.File.ReadAllLines("Complete Test/solvable puzzles/Manhattan Only/15 Puzzle 5.txt");
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
            Node node = new Node(puzzle, int.MaxValue, 0,null);
            
            List<Node> Sol = new List<Node>();
            int level = 0;
            if(!node.isSolvable())
            {
                Console.WriteLine("Unsolvable");
                return;
            }      
            open.Enqueue(node);
            //node.PrintBoard();
            while (open.Count() != 0)
            {
                Node current = open.Dequeue();
                if (current.isGoal())
                {
                    level = current.level;
                    while (current.parent != null)
                    {
                        Sol.Add(current);
                        current = current.parent;
                        
                    }
                    break;
                }
                
                closed.Add(hashFunctionFor2DArr(current.data));
                current.FindPossibleMoves();
                foreach(Node child in current.children)
                {

                    if (!closed.Contains(hashFunctionFor2DArr(child.data)))
                        open.Enqueue(child);
                    
                    
                }
            }
            Console.WriteLine(level);
            //for (int i = level - 1; i >= 0; i--)
            //{
            //    for (int j = 0; j < Sol[i].data.GetLength(0); j++)
            //    {
            //        for (int k = 0; k < Sol[i].data.GetLength(0); k++)
            //        {
            //            Console.Write(Sol[i].data[j, k] + " ");

            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //}

        }
    }
}

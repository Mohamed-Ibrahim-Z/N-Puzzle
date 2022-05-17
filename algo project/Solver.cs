using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{

    public enum DistanceFunction { MANHATTEN , HAMMING };
    internal class Solver
    {
        static public HashSet<int> closed = new HashSet<int>();
        static public PrQueue open = new PrQueue();
        
        public static void printRes(List<int[,]> path)
        {
            int count = 0;
            for (int i = path.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        Console.Write(path[i][j,k] + " ");
                    }
                    Console.WriteLine();
                }
                        count++;
                        Console.WriteLine(count);
                Console.WriteLine();
            }
        }
        static public int hashFunctionFor2DArr(int[,] arr, int size)
        {
            int hash = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    hash = (hash * 31 + arr[i, j]) % 1000000007;
                }
                hash *= 7;
            }
            return hash;

        }

        int[,] puzzle;
        int size;
        DistanceFunction distanceFunction;
        public Solver(int[,] puzzle,int size, DistanceFunction distanceFunction)
        {
            this.puzzle = puzzle;
            this.size = size;
            this.distanceFunction = distanceFunction;
        }
        
        public int Solve()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<int[,]> result = new List<int[,]>();


            Node node = new Node(puzzle,size,distanceFunction);

            open.Enqueue(node);
            
            if (!node.isSolvable())
            {
                Console.WriteLine("Not solvable");
                return -1;
            }

            if (size == 3)
            {
                Console.WriteLine("Solvable\nInitial Board");
                node.PrintBoard();
            }
            int level = 0;
            while (open.Count() != 0)
            {
                Node current = open.Dequeue();
                if (current.level == current.cost)
                {
                    level = current.level;
                    if(size == 3)
                    {
                        while (current.parent != null)
                        {
                            
                            result.Add(current.data);
                            current = current.parent;
                        }
                    }
                    break;
                }
                //current.PrintBoard();
                //Console.WriteLine(current.cost);
                closed.Add(hashFunctionFor2DArr(current.data, size));
                current.getPosibolChildren();
            }
            if (size == 3)
            {
                Form1 gui = new Form1(result,this.puzzle,level);
                printRes(result);
                gui.ShowDialog();
            }
            
            Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed);
            Console.WriteLine("# Of Moves: " + level);
            //Clearing
            open.Clear();
            closed.Clear();
            return 0;
        }
    }
}

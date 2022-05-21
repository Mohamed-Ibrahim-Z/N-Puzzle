using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{

    public enum DistanceFunction { MANHATTEN , HAMMING, BFS };
    internal class Solver
    {
        static public HashSet<int> closed = new HashSet<int>();
        static public Queue<Node> BFS = new Queue<Node>();
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
                        Console.Write(path[i][j, k] + " ");
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
        public Solver(int[,] puzzle, int size, DistanceFunction distanceFunction)
        {
            this.puzzle = puzzle;
            this.size = size;
            this.distanceFunction = distanceFunction;
        }

        public int Solve()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Node node = new Node(puzzle, size, distanceFunction);  // θ(N²)
            open.Enqueue(node); // θ(Log(V))
            BFS.Enqueue(node);
            if (!node.isSolvable()) // θ(S²)
            {
                stopwatch.Stop();
                Console.WriteLine("Not solvable");
                Console.WriteLine("Time elapsed: {0:ss\\:ff} Seconds", stopwatch.Elapsed);
                return -1;
            }

            if (size == 3)
            {
                Console.WriteLine("Solvable\nInitial Board");
                node.PrintBoard(); // θ(1)
            }
            List<int[,]> result = new List<int[,]>();
            int level = 0;
            if (node.distanceFunction == DistanceFunction.BFS)
                level = BFSSolve(ref result);
            else
                level = HeuristicSolve(ref result);

            stopwatch.Stop();
            if (size == 3)
            {
                printRes(result); // Log(V)
                //Form1 gui = new Form1(result,this.puzzle, stopwatch.Elapsed.TotalSeconds); // We start the gui
                //gui.ShowDialog();
            }
            Console.WriteLine("Time elapsed: {0:ss\\:ff} Seconds", stopwatch.Elapsed);
            Console.WriteLine("# Of Moves: " + level);
            //Clearing
            open.Clear();
            closed.Clear();
            BFS.Clear();
            return 0;
        }

        private int BFSSolve(ref List<int[,]> res)
        {
            int[,] goal = new int[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    goal[i, j] = i * size + j + 1;
                }
            }
            int level = 0;
            goal[size - 1, size - 1] = 0;
            int goalHash = hashFunctionFor2DArr(goal, size);
            while (BFS.Count != 0)
            {
                Node current = BFS.Dequeue();
                if(current.hash == goalHash)
                {
                    level = current.level;
                    if (size == 3)
                    {
                        while (current.parent != null) // O(Log(V))
                        {
                            res.Add(current.data);
                            current = current.parent;
                        }
                    }
                    break;
                }
                closed.Add(current.hash); // θ(1)

                current.getPossibleChildren(); // θ(S)
                current = null;

            }
            return level;
        }

        int HeuristicSolve(ref List<int[,]> res)
        {
            int level = 0;
            while (open.Count() != 0)
            {
                Node current = open.Dequeue(); // Log(V)
                level = current.level;
                //current.PrintBoard();
                if (current.level == current.cost)
                {
                    if (size == 3)
                    {
                        while (current.parent != null) // O(Log(V))
                        {
                            res.Add(current.data);
                            current = current.parent;
                        }
                    }
                    break;
                }
                closed.Add(current.hash); // θ(1)
                current.getPossibleChildren(); // θ(S)
            } // E (Log V)
            return level;
        }




        // =================================BOUNSE======================================== //
       
    }
}

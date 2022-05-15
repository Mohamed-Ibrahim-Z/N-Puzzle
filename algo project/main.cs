using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace algo_project
{
    internal class main
    {

        static void Main(string[] args)
        {

            string[] read = System.IO.File.ReadAllLines("Complete Test/Solvable puzzles/Manhattan & Hamming/9999 Puzzle.txt");
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
            Solver solver = new Solver(puzzle, n, DistanceFunction.HAMMING);

            solver.Solve();



        }
    }
}


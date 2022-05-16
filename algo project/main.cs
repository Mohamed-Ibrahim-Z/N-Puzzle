using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace algo_project
{
    internal class main
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Choose which to test");
            Console.WriteLine("1 -> for Sample Test");
            Console.WriteLine("2 -> for Complete Test");
            Reader reader = new Reader();

            char choice = (char)Console.ReadLine()[0];
            switch (choice)
            {
                case '1':
                        reader.ReadSampleTests();
                        break;

                case '2':
                        reader.ReadCompleteTests();
                        break;

                default: 
                        Console.WriteLine("Invalid Choice!");
                        break;
            }
            //string[] read = System.IO.File.ReadAllLines("Complete Test/V. Large test case/TEST.txt");
            //int n = int.Parse(read[0]);
            //int[,] puzzle = new int[n, n];
            //for (int i = 0; i < n; i++)
            //{
            //    string[] line = read[i + 2].Split(' ');
            //    for (int j = 0; j < n; j++)
            //    {
            //        puzzle[i, j] = int.Parse(line[j]);
            //    }
            //}
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //Solver solver = new Solver(puzzle, n, DistanceFunction.MANHATTEN);

            //solver.Solve();
            //Console.WriteLine("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed);



        }
    }
}


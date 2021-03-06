using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace algo_project
{

    internal class Reader
    {


        DistanceFunction distanceFunction = DistanceFunction.MANHATTEN;
    public void ReadSampleTests()
     {
            
            string path = "Sample Test/"; 
            Console.WriteLine("Choose if Solvable Or not");
            Console.WriteLine("1 -> for Solvable");
            Console.WriteLine("2 -> for Unsolvable");
            char choice = (char)Console.ReadLine()[0];
            Console.WriteLine();
            switch (choice)
            {
                case '1':
                    {
                        path += "Solvable Puzzles";
                        Console.WriteLine("1 -> for Manhatten");
                        Console.WriteLine("2 -> for Hamming");
                        Console.WriteLine("3 -> for BFS");
                        choice = (char)Console.ReadLine()[0];
                        Console.WriteLine();
                        if (choice == '1')
                            distanceFunction = DistanceFunction.MANHATTEN;
                        else if (choice == '2')
                            distanceFunction = DistanceFunction.HAMMING;
                        else if (choice == '3')
                            distanceFunction = DistanceFunction.BFS;
                        break;
                    }
                case '2':
                    {
                        path += "Unsolvable Puzzles";
                        break;
                    }
            } 
            IterateOnFolder(path);  //θ(S  * E(Log(V)))
        } // Complexity  θ(1 +  IterateOnFolder )
        public void ReadCompleteTests()
        {
            string path = "Complete Test/";
            Console.WriteLine("Choose One to test");
            Console.WriteLine("1 -> for Solvable Puzzles");
            Console.WriteLine("2 -> for UnSolvable Puzzles");
            Console.WriteLine("3 -> for V.Large Test");

            char choice = (char)Console.ReadLine()[0];
            switch (choice)
            {
                case '1':
                    {
                        path += "Solvable puzzles/";
                        Console.WriteLine("1 -> for Manhatten Only");
                        Console.WriteLine("2 -> for Manhatten && Hamming");
                        choice = (char)Console.ReadLine()[0];
                        Console.WriteLine();
                        if (choice == '1')
                        {
                            path += "Manhattan Only";
                            Console.WriteLine("1 -> for Manhatten");
                            Console.WriteLine("2 -> for Hamming");
                            Console.WriteLine("3 -> for BFS");
                            choice = (char)Console.ReadLine()[0];
                            Console.WriteLine();
                            if (choice == '1')
                                distanceFunction = DistanceFunction.MANHATTEN;
                            else if (choice == '2')
                                distanceFunction = DistanceFunction.HAMMING;
                            else if (choice == '3')
                                distanceFunction = DistanceFunction.BFS;                            
                        }
                        else if (choice == '2')
                        {
                            path += "Manhattan & Hamming";
                            Console.WriteLine("1 -> for Manhatten");
                            Console.WriteLine("2 -> for Hamming");
                            Console.WriteLine("3 -> for BFS");
                            choice = (char)Console.ReadLine()[0];
                            Console.WriteLine();
                            if (choice == '1')
                                distanceFunction = DistanceFunction.MANHATTEN;
                            else if (choice == '2')
                                distanceFunction = DistanceFunction.HAMMING;
                            else if (choice == '3')
                                distanceFunction = DistanceFunction.BFS;
                        }

                        break;
                    }
                case '2':
                    {
                        Console.WriteLine();
                        path += "Unsolvable puzzles/";
                        break;
                    }
                case '3':
                    {
                        Console.WriteLine();
                        path += "V. Large test case/";
                        Console.WriteLine("1 -> for Manhatten");
                        Console.WriteLine("2 -> for Hamming");
                        choice = (char)Console.ReadLine()[0];
                        Console.WriteLine();
                        if (choice == '1')
                            distanceFunction = DistanceFunction.MANHATTEN;
                        else if (choice == '2')
                            distanceFunction = DistanceFunction.HAMMING;
                        break;
                    }
            }
            IterateOnFolder(path);
        }
        public void IterateOnFolder(string path)
        {
            var files = Directory.GetFiles(path, "*.txt");
            string[] text;

            foreach (var file in files)
            {
                text = File.ReadAllLines(file);
                int n = int.Parse(text[0]);
                int[,] puzzle = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    string[] line = text[i + 2].Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        puzzle[i, j] = int.Parse(line[j]);
                    }
                }
                
                string[] s = file.Split('\\');
               
                if (s.Length > 1)
                {
                    Console.WriteLine(s[1]);
                }
                else
                {
                    Console.WriteLine(s[0]);
                }
                
                Solver solver = new Solver(puzzle, n, distanceFunction);
                    solver.Solve(); // O(E Log (V))
                Console.WriteLine();
            }
        }
    }
}

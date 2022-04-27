using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{
    internal class Class1
    {
        static void Main(string[] args)
        {
            // Solving N-Puzzle problem using A* algorithm
            // The goal state is represented by a zero in the puzzle
            // The puzzle is represented by a 2D array

            // Initializing the puzzle
            int[,] puzzle = new int[3, 3] { { 0, 1, 3 }, { 4, 2, 5 }, { 7, 8, 6 } };

            // Initializing the goal state
            int[,] goal = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };

            // get the Hammming distance between the puzzle and the goal state
            int h = getH(puzzle, goal);
            
            // get all possible moves from the current state
            List<int[,]> moves = getMoves(puzzle);

            // get the f(n) value for each move
            List<int> f = new List<int>();
            foreach (int[,] move in moves)
            {
                f.Add(getH(move, goal) + getG(move));
            }

            // get the index of the move with the lowest f(n) value
            int index = f.IndexOf(f.Min());

            // get the move with the lowest f(n) value
            int[,] bestMove = moves[index];

            // print the puzzle
            printPuzzle(puzzle);

            // print the goal state
            printPuzzle(goal);

            // print the best move
            printPuzzle(bestMove);

            
        }

    }
}

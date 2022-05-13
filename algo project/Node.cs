using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class Node
    {
        public int[,] grid;       
        public int h;            
        public int level;            
        public int cost;
        public int blankRow;        
        public int blankCol;        
        public Node parent;         
        public static int size;     

        public Node(int[,] arr, int size)
        {
            Node.size = size;
            grid = new int[size, size];
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    grid[row, col] = arr[row, col];
                    if (arr[row, col] == 0)
                    {
                        blankRow = row;
                        blankCol = col;
                    }
                }
            }
            level = 0;
            parent = null;
        }

        public Node(Node parent)
        {
            grid = parent.grid.Clone() as int[,];
            this.parent = parent;
            level = parent.level + 1;
            blankRow = parent.blankRow;
            blankCol = parent.blankCol;
        }

        public int computeCost(Node node,string costType)
        {
            if(costType == "hamming")
            {
                node.h = ComputeHamming(node);
            }
            else
            {
                node.h = computeManhattan(node);
            }
            node.cost = node.level + node.h;
            return node.cost;

        }
        public bool isEqual(Node node)
        {
            if(node == null)
                return false;
            if (blankRow != node.blankRow || blankCol != node.blankCol)
                return false;
            return true;
        }
        public Node moveUp()
        {
            // Copy the parent's data (grid, g, x, y) and add parent
            Node child = new Node(this);
            
            child.grid[blankRow, blankCol] = grid[blankRow - 1, blankCol];
            child.grid[blankRow - 1, blankCol] = 0;
            child.blankRow = blankRow - 1;
            child.blankCol = blankCol;
            return child;
        }

        public Node moveDown()
        {
            // Copy the parent's data
            Node child = new Node(this);

         
            child.grid[blankRow, blankCol] = grid[blankRow + 1, blankCol];
            child.grid[blankRow + 1, blankCol] = 0;
            child.blankRow = blankRow + 1;
            child.blankCol = blankCol;
            return child;
        }

        public Node moveRight()
        {
            // Copy the parent's data
            Node child = new Node(this);

            child.grid[blankRow, blankCol] = grid[blankRow, blankCol + 1];
            child.grid[blankRow, blankCol + 1] = 0;
            child.blankRow = blankRow;
            child.blankCol = blankCol + 1;
            return child;
        }

        public Node moveLeft()
        {
            // Copy the parent's data
            Node child = new Node(this);
           
            child.grid[blankRow, blankCol] = grid[blankRow, blankCol - 1];
            child.grid[blankRow, blankCol - 1] = 0;
            child.blankRow = blankRow;
            child.blankCol = blankCol - 1;
            return child;
        }

        public int ComputeHamming(Node node)
        {
            int hamming = 0,count = 1;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (node.grid[row, col] != 0)
                    {
                        if (node.grid[row, col] != count)
                            hamming++;
                    }
                    count++;
                    count %= size * size;
                }
            }
            return hamming;
        }

        public int computeManhattan(Node node)
        {
            int manhattan = 0, expectedRow = 0, exepectedCol = 0;
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (node.grid[row, col] != 0 ) 
                    {
                        exepectedCol = (node.grid[row, col] - 1) % size;
                        expectedRow = (node.grid[row, col] - 1) / size;
                        manhattan = manhattan + Math.Abs(row - expectedRow) + Math.Abs(col - exepectedCol);
                    }

                }
             
            }
            return manhattan;
        }
        public void printState()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    // For beautifying reasons
                    String num = "0" + grid[row, col].ToString();
                    Console.Write(num.Substring(num.Length - 2));
                    if (col < size - 1) Console.Write(" | ");
                }
                Console.Write('\n');
            }
        }

        public Boolean isSolvable()
        {
            int[] tempGrid = new int[size * size];
            int cells = 0, ans=0;

            // Convert to 1D array
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    tempGrid[cells] = grid[row, col];
                    cells++;
                }
            }

            for (int cell = 0; cell < size*size; cell++)
            {
                if (tempGrid[cell] == 0) continue;
                for (int j = cell+1; j < size*size; j++)
                {
                    if (tempGrid[j] != 0 && tempGrid[cell] < tempGrid[j]) ans++;
                }
            }

            if (size%2 == 1 && ans % 2 == 0) return true;

            if (size %2 == 0)
                if (blankRow % 2 == ans % 2) return true;

            return false;
        }
        public int gridToHash()
        {
            int hash = 193;
            for (int row = 0; row <size; row++)
            {
                for(int col = 0;col<size;col++)
                {
                    hash = hash * 59 + (grid[row, col]);
                
                }
            }


            return hash;
        }
        
    }
}

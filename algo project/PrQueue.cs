using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{
    public class Node
    {
        Tuple<int, int> zero;
        public List<Node> children;
        public Node parent;
        public int[,] target;
        public int[,] data;
        public int cost;
        public int level;
        public int size;


        public Node(int[,] d, int cost, int level, Node parent)
        {
            this.size = d.GetLength(0);
            InitBoard(d);
            this.parent = parent; 
            this.cost = cost;
            this.level = level;
            this.parent = parent;
            children = new List<Node>();
        }
        
        private void InitBoard(int[,] d)
        {
            int tile = 1;
            target = new int[size, size];
            data = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1)
                    {
                        tile = 0;
                    }
                    data[i, j] = d[i, j];
                    if (isZero(i,j))
                    {
                        zero = new Tuple<int, int>(i, j);
                    }
                    target[i, j] = tile;
                    tile++;
                }
            }
        }

        private bool isZero(int i,int j)
        {
            if (data[i, j] == 0)
                return true;
            return false;
        }
        
        public bool isSolvable()
        {
            int inversions = 0;
            int blankLine = size - zero.Item1;
            int[] checkBoard = new int[size * size];
            checkBoard = data.Cast<int>().ToArray();
            for (int i = 0; i < size * size; i++)
            {
                for (int j = i + 1; j < size * size; j++)
                {
                    if (checkBoard[j] > 0 && checkBoard[i] > 0 && checkBoard[i] > checkBoard[j])
                        inversions++;
                }
            }
            if (size % 2 == 1)
            {
                if (inversions % 2 == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if ((inversions % 2 == 0 && blankLine % 2 == 1) ||
                    (inversions % 2 == 1 && blankLine % 2 == 0))
                    return true;
                else
                    return false;
            }
        }

        int Hamming()
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] != target[i, j] && data[i, j] != 0)
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
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] != target[i, j] && data[i, j] != 0)
                    {
                        int x = (data[i, j] - 1) / size;
                        int y = (data[i, j] - 1) % size;
                        count += Math.Abs(x - i) + Math.Abs(y - j);
                    }
                }
            }
            return count;
        }

        public bool isGoal()
        {
            if (cost == level)
                return true;

            return false;
        }

        private void FakeSwap(Tuple<int, int> zero, Tuple<int, int> tile)
        {
            //int h = -1;
            int m = -1;
            try
            {
                int temp = data[zero.Item1, zero.Item2];
                data[zero.Item1, zero.Item2] = data[tile.Item1, tile.Item2];
                data[tile.Item1, tile.Item2] = temp;


                //h = Hamming();
                m = Manhattan();
                children.Add(new Node(data, m + level + 1, level + 1, this));
                int temp1 = data[zero.Item1, zero.Item2];
                data[zero.Item1, zero.Item2] = data[tile.Item1, tile.Item2];
                data[tile.Item1, tile.Item2] = temp1;

            }
            catch (Exception) { }
           
        }

        public void FindPossibleMoves()
        {
            FakeSwap(zero, new Tuple<int, int>(zero.Item1 + 1, zero.Item2));

            FakeSwap(zero, new Tuple<int, int>(zero.Item1 - 1, zero.Item2));

            FakeSwap(zero, new Tuple<int, int>(zero.Item1, zero.Item2 + 1));

            FakeSwap(zero, new Tuple<int, int>(zero.Item1, zero.Item2 - 1));



        }

        public void PrintBoard()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(data[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }



    }
    internal class PrQueue
    {
        // implement priority queue
        private List<Node> queue;
        public PrQueue()
        {
            queue = new List<Node>();
        }
        public void Enqueue(Node n)
        {
            queue.Add(n);
            int i = queue.Count - 1;
            while (i > 0)
            {
                if (queue[i].cost < queue[(i - 1) / 2].cost)
                {
                    Node temp = queue[i];
                    queue[i] = queue[(i - 1) / 2];
                    queue[(i - 1) / 2] = temp;
                    i = (i - 1) / 2;
                }
                else
                {
                    break;
                }
            }
        }
        public Node Dequeue()
        {
            Node n = queue[0];
            queue[0] = queue[queue.Count - 1];
            queue.RemoveAt(queue.Count - 1);
            int i = 0;
            while (2 * i + 1 < queue.Count)
            {
                int j = 2 * i + 1;
                if (j + 1 < queue.Count && queue[j + 1].cost < queue[j].cost)
                {
                    j++;
                }
                if (queue[i].cost > queue[j].cost)
                {
                    Node temp = queue[i];
                    queue[i] = queue[j];
                    queue[j] = temp;
                    i = j;
                }
                else
                {
                    break;
                }
            }
            return n;
        }
        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

        public int Count()
        {
            return queue.Count;
        }
    }
}

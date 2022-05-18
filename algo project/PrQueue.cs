using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{
    public class Node
    {
        DistanceFunction distanceFunction;
        Tuple<int, int> zero;
        public Node parent;
        public int[,] data;
        public int cost;
        public int level;
        public int size;
        public int hash;


        public Node(int[,] d, int size, DistanceFunction distanceFunction)
        {
            this.size = size;
            InitBoard(d);
            this.parent = null;
            this.distanceFunction = distanceFunction;
            this.cost = calcDistance(distanceFunction);
            this.level = 0;
            this.hash = Solver.hashFunctionFor2DArr(d,size);
        }
        public Node(Node parent,int hash,int cost)
        {
            this.size = parent.size;
            InitBoard(parent.data);
            this.parent = parent;
            this.distanceFunction = parent.distanceFunction;
            this.cost = cost;
            this.level = parent.level + 1;
            this.parent = parent;
            this.hash = hash;
        }

        private void InitBoard(int[,] d)
        {
            data = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    data[i, j] = d[i, j];
                    if (data[i, j] == 0)
                    {
                        zero = new Tuple<int, int>(i, j);
                    }

                }
            }
        }
      
        public bool isSolvable()
        {
            int inversions = 0;
            int blankLine = size - zero.Item1;
            int[] checkBoard = data.Cast<int>().ToArray();
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
        public int calcDistance(DistanceFunction distanceFunction)
        {
            if (distanceFunction == DistanceFunction.HAMMING)
                return Hamming();
            else
                return Manhattan();
        }
        private int Hamming()
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] != i * size + j + 1 && data[i, j] != 0)
                    {
                        count++;
                    }
                }
            }
            return count;

        }

        private int Manhattan()
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (data[i, j] != i * size + j + 1 && data[i, j] != 0)
                    {
                        int x = (data[i, j] - 1) / size;
                        int y = (data[i, j] - 1) % size;
                        count += Math.Abs(x - i) + Math.Abs(y - j);
                    }
                }
            }
            return count;
        }


        public void getChilderen(DistanceFunction distanceFunction, Tuple<int, int> zero, Tuple<int, int> tile)
        {
            if (distanceFunction == DistanceFunction.HAMMING)
                GetHammingChild(zero, tile);
            else
                GetManhattanChild(zero, tile);
        }
        
        private void GetHammingChild(Tuple<int, int> zero, Tuple<int, int> tile)
        {
            data[zero.Item1, zero.Item2] = data[tile.Item1, tile.Item2];

            // if moved a correct tile + 1
            int correctX = (data[tile.Item1, tile.Item2] - 1) / size;
            int correctY = (data[tile.Item1, tile.Item2] - 1) % size;
            data[tile.Item1, tile.Item2] = 0;
            int hash;


            if (correctX == tile.Item1 && correctY == tile.Item2)
            {
                hash = Solver.hashFunctionFor2DArr(data, size);
                if (!Solver.closed.Contains(hash))
                {
                   Solver.open.Enqueue(new Node(this, hash, cost  + 2));
                   
                }

            }
            //if moved a wrong tile to correct tile -1
            else if(correctX == zero.Item1 && correctY == zero.Item2)
            {

                hash = Solver.hashFunctionFor2DArr(data, size);
                if (!Solver.closed.Contains(hash))
                {
                    Solver.open.Enqueue(new Node(this, hash, cost ));

                }

            }
            // if moved a wrong tile to wrong tile ------------
            else
            {

                hash = Solver.hashFunctionFor2DArr(data, size);
                if (!Solver.closed.Contains(hash))
                {

                    Solver.open.Enqueue(new Node(this, hash, cost  + 1));

                }

            }

            data[tile.Item1, tile.Item2] = data[zero.Item1, zero.Item2];
            data[zero.Item1, zero.Item2] = 0;


        }

        private void GetManhattanChild(Tuple<int, int> zero, Tuple<int, int> tile)
        {
            data[zero.Item1, zero.Item2] = data[tile.Item1, tile.Item2];
            int x = (data[tile.Item1, tile.Item2] - 1) / size;
            int y = (data[tile.Item1, tile.Item2] - 1) % size;



            int x1 = (data[zero.Item1, zero.Item2] - 1) / size;
            int y1 = (data[zero.Item1, zero.Item2] - 1) % size;
            int costWithZ1 = Math.Abs(x1 - zero.Item1) + Math.Abs(y1 - zero.Item2) +
                cost - Math.Abs(x - tile.Item1) - Math.Abs(y - tile.Item2) - level;
            
            data[tile.Item1, tile.Item2] = 0;

            int hash = Solver.hashFunctionFor2DArr(data, size);
            if (!Solver.closed.Contains(hash))
            {
                Solver.open.Enqueue(new Node(this, hash, costWithZ1 + level + 1));
            }
            data[tile.Item1, tile.Item2] = data[zero.Item1, zero.Item2];
            data[zero.Item1, zero.Item2] = 0;
        }


        

        public void getPosibolChildren()
        {

            if (zero.Item1 < size - 1)
            {
                getChilderen(distanceFunction,zero, new Tuple<int, int>(zero.Item1 + 1, zero.Item2));
            }
            if (zero.Item1 > 0)
            {
                getChilderen(distanceFunction, zero, new Tuple<int, int>(zero.Item1 - 1, zero.Item2));
            }
            if (zero.Item2 < size - 1)
            {
                getChilderen(distanceFunction, zero, new Tuple<int, int>(zero.Item1, zero.Item2 + 1));
            }
            if (zero.Item2 > 0)
            {
                getChilderen(distanceFunction, zero, new Tuple<int, int>(zero.Item1, zero.Item2 - 1));
            }


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
                int parent = (i - 1) / 2;
                if (queue[i].cost < queue[parent].cost)
                {
                    Node temp = queue[i];
                    queue[i] = queue[parent];
                    queue[parent] = temp;
                    i = parent;
                }
                else
                {
                    break;
                }
            }
        }
        public Node Dequeue()
        {
            if (queue.Count == 0)
            {
                return null;
            }
            Node n = queue[0];
            queue[0] = queue[queue.Count - 1];
            queue.RemoveAt(queue.Count - 1);
            MinHeapify(0);
            return n;
        }
        public void MinHeapify(int i)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int smallest = i;
            if (left < queue.Count && queue[left].cost < queue[i].cost)
            {
                smallest = left;
            }
            if (right < queue.Count && queue[right].cost < queue[smallest].cost)
            {
                smallest = right;
            }
            if (smallest != i)
            {
                Node temp = queue[i];
                queue[i] = queue[smallest];
                queue[smallest] = temp;
                MinHeapify(smallest);
            }
        }
        public bool IsEmpty()
        {
            return queue.Count == 0;
        }

        public int Count()
        {
            return queue.Count;
        }
        public Node Peek()
        {
            return queue[0];
        }
        public void Clear()
        {
            queue.Clear();
            
        }
    }
}

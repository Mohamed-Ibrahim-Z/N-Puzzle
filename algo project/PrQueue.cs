﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algo_project
{
    public class Node
    {
        public int[,] data;
        public int cost;
        public int level;

        public Node(int[,] d, int cost, int level)
        {
            data = d.Clone() as int[,];

            this.cost = cost;
            this.level = level;
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

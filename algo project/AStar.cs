using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using algo_project;
namespace N_Puzzle
{
    class AStar
    {
        //public Stack<Node> finalAnswer;
        //public HashSet<int> finished;
        public PrQueue nodes = main.open;
        public Node current, child;
        public int cnt;
        public Node aStar(Node parent,string costType)
        {
            //finalAnswer = new Stack<Node>();
            //finished = new HashSet<int>(); 
            cnt = 0;
            nodes = new PrQueue();
     
            if (!parent.isSolvable())
            {
                parent.level = -1;
                return parent;
            }

           parent.cost = parent.computeCost(parent,costType);
            if (parent.h == 0)
            {
                //getSteps(parent);
                return parent;
            }
          

            // Let's go through all four options
            getChildern(parent, costType);
            // Now we are done with the parent
          // finished.Add(parent.gridToHash());
            current = nodes.Dequeue();
            while (nodes.Count() != 0)
            {

                //current.printState();
                // Base case (Finish state)
             
                if (current.h == 0)
                {
                    //getSteps(current);
                    current.h = cnt;
                    return current;
                }
                // We will start pushing the children so remove the parent from top
             
                
                // Repeat the four combinations again (beware of the states in finished)
               //finished.Add(current.gridToHash());
                getChildern(current,costType);
                current = nodes.Dequeue();


            }
          
            // If some error happened, return 0 or -1 or anything else (number of steps)
            return null;
        }
        public static int hashFunctionFor2DArr(int[,] arr)
        {
            int hash = 0;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    hash = (hash * 31 + arr[i, j]) % 1000000007;
                }
                hash *= 7;
            }
            return hash;
        }

        public void getChildern(Node node,string costType)
        {
            if (node.blankRow + 1 <= Node.size - 1)
            {
                child = node.moveDown();
                if (!main.closed.Contains(hashFunctionFor2DArr(child.grid)))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.Enqueue(child);
                    cnt++;
                }
            }
            if (node.blankRow - 1 >= 0)
            {
                child = node.moveUp();
                if (!main.closed.Contains(hashFunctionFor2DArr(child.grid)))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.Enqueue(child);
                    cnt++;
                }
            }
            if (node.blankCol + 1 <= Node.size - 1)
            {
                child = node.moveRight();
                if (!main.closed.Contains(hashFunctionFor2DArr(child.grid)))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.Enqueue(child);
                    cnt++;
                }
            }
            if (node.blankCol - 1 >= 0)
            {
                child = node.moveLeft();
                if (!main.closed.Contains(hashFunctionFor2DArr(child.grid)))
                {
                    child.cost = child.computeCost(child, costType);
                    nodes.Enqueue(child);
                    cnt++;
                }
            }
           
        }

        
        //public void getSteps(Node node)
        //{
           
        //    while(node.parent!=null)
        //    {
        //        finalAnswer.Push(node);
        //        node = node.parent;
        //    }
        //    finalAnswer.Push(node);
        //}
        //public void printSteps()
        //{
        //    int stpCntr = 0;
        //    while (finalAnswer.Count != 0)
        //    {
        //        if (stpCntr == 0)
        //        {
        //            Console.WriteLine("Parent :");
        //            stpCntr++;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Step " + stpCntr +" :");
        //            stpCntr++;
        //        }
        //        finalAnswer.Pop().printState();
        //        Console.WriteLine("-------------");
        //    }
        //}
       
    }
}

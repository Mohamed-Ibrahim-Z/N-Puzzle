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
                        reader.ReadSampleTests(); // Complexity  θ(1 +  IterateOnFolder )
                        break;

                case '2':
                        reader.ReadCompleteTests();  // Complexity  θ(1 +  IterateOnFolder )
                        break;

                default: 
                        Console.WriteLine("Invalid Choice!");
                        break;
            }
             // Complexity  θ(1 +  IterateOnFolder )
        }
    }
}


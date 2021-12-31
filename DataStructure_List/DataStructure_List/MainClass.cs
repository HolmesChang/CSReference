using System;
using System.Collections.Generic;

namespace DataStructure_List
{
    class MainClass
    {
        public static void Main (System.String [] args)
        {
            System.Console.WriteLine("Hello Data Structure List");

            // ================================================== //
            //  Declaration AND Initialization Of 1D List
            // ================================================== //
            System.Collections.Generic.List<System.Int32> ListInt32 = new List<System.Int32>();

            // ================================================== //
            //  Push Element To Back
            // ================================================== //
            for (Int32 i=0; i<10; i++)
            {
                ListInt32.Add(i);
            }

            // ================================================== //
            //  Indexing
            // ================================================== //
            Console.WriteLine("\n\n\nIndexing\n\n\n");
            for (Int32 i = 0; i < 10; i++)
            {
                // Solution Of Printing Number W/I Zero Padding
                Console.WriteLine("{0, -5} Element = {1, 5}", i.ToString("D"), ListInt32[i].ToString("D"));
            }

            // ================================================== //
            //  Slicing
            // ================================================== //
            Console.WriteLine("\n\n\nSlicing\n\n\n");
            Int32 index = 3;
            foreach (var Element in ListInt32.GetRange(index, 5))
            {
                Console.WriteLine("Index {0, -5} = {1, 5}", index, Element);
            }

            // ================================================== //
            //  Declaration AND Initialization Of 2D List
            // ================================================== //
            Console.WriteLine("\n\n\n2D List\n\n\n");
            List<List<String>> ListMultiplicationTable = new List<List<String>>();
            for (Int32 i=1; i<=9; i++)
            {
                List<String> ListTEMP = new List<String>();
                for (Int32 j=1; j<=9; j++)
                {
                    ListTEMP.Add(String.Format("{0, -2} x {1, -2} = {2, -2}", i, j, i * j));
                }
                ListMultiplicationTable.Add(new List<string>(ListTEMP));
            }
            //Console.WriteLine(ListMultiplicationTable[0][1]);

            for (Int32 i=0; i<9; i++)
            {
                for (Int32 j=0; j<9; j++)
                {
                    Console.Write(ListMultiplicationTable[i][j]);
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("Please Enter Any Key To Exit");
            System.Console.ReadKey();
        }
    }
}

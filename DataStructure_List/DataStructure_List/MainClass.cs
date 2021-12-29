using System;
using System.Collections.Generic;

namespace DataStructure_List
{
    class MainClass
    {
        public static void Main (System.String [] args)
        {
            System.Console.WriteLine("Hello Data Structure List");

            System.Collections.Generic.List<System.Int32> ListInt32 = new System.Collections.Generic.List<System.Int32>();
            for (Int32 i=0; i<10; i++)
            {
                ListInt32.Add(i);
            }
            for (Int32 i = 0; i < 10; i++)
            {
                // Solution Of Printing Number W/I Zero Padding
                Console.WriteLine("{0, -5} Element = {1, 5}", i.ToString("D"), ListInt32[i].ToString("D"));
            }

            Console.WriteLine("Please Enter Any Key To Exit");
            System.Console.ReadKey();
        }
    }
}

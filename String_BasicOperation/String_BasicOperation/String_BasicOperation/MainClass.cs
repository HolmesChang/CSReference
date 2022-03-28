using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace String_BasicOperation
{
    class MainClass
    {
        public static void Main (String [] args)
        {
            String Name = "BtnRD_B0h";

            Console.WriteLine(Name.Substring(3, 2).Equals("RD"));

            Console.ReadLine();
        }
    }
}

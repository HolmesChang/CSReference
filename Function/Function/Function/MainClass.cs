using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Function
{
    class MainClass
    {
        public static void CalcArea (double r, out double area)
        {
            area = 3.14 * r * r;
        }

        public static void Main (String [] args)
        {
            double r, area;

            Console.Write("Please Enter Radius:");
            r = Int32.Parse(Console.ReadLine());

            CalcArea(r, out area);

            Console.WriteLine($"Area = {area}");

            Console.ReadLine();
        }
        
    }
}

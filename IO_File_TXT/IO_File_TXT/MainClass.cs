#define METHOD002

namespace IO_File_TXT
{
    class MainClass
    {


        static void Main (string [] args)
        {
            System.Console.WriteLine("Hello C#");

            const string fpath = @"D:\Publication\CS Reference\IO_File_TXT\PA1013A.csv";

#if METHOD001
            // Example #1
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(fpath);

            // Display the file contents to the console. Variable text is a string.
            System.Console.WriteLine("Contents of WriteText.txt = \n{0}", text);

            /* Workable. Temporary Disabling.
            // Example #2
            // Read each line of the file into a string array. Each element
            // of the array is one line of the file.
            string [] lines = System.IO.File.ReadAllLines(fpath);

            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                System.Console.WriteLine("\t" + line);
            }
            */
#endif

#if METHOD002
            var reader = new System.IO.StreamReader(System.IO.File.OpenRead(fpath));
            System.Collections.Generic.List<System.Collections.Generic.List<string>> reg = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
            //System.Collections.Generic.List<string> listA = new System.Collections.Generic.List<string>();
            //System.Collections.Generic.List<string> listB = new System.Collections.Generic.List<string>();
            //int row = 0;
            //int col = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                /* Debugging
                System.Console.WriteLine("Count Of Element = " + values.Length);
                int index = 0;
                foreach (var ele in values)
                {
                    System.Console.WriteLine((++index) + " Element = " + ele);
                }
                System.Console.ReadKey();  return;
                */

                System.Collections.Generic.List<string> tmp = new System.Collections.Generic.List<string>();
                foreach (string ele in values)
                {
                    if (ele.Replace(" ", "") == "")
                        tmp.Add("-");
                    else
                        tmp.Add(ele);
                }
                reg.Add(tmp);
                //listA.Add(values[0]);
                //listB.Add(values[1]);
                //foreach (var coloumn1 in listA)
                //{
                //    System.Console.WriteLine(coloumn1);
                //}
                //foreach (var coloumn2 in listB)
                //{
                //    System.Console.WriteLine(coloumn2);
                //}
            }

            foreach (var row in reg)
            {
                foreach (var col in row)
                {
                    System.Console.Write("{0, -18} ", col);
                }
                System.Console.WriteLine("");
            }
#endif

            // Keep the console window open in debug mode.
            System.Console.WriteLine("Please Enter Any Key To Exit");
            System.Console.ReadKey();
        }
    }
}

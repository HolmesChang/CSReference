using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IO_File_JSON
{
    class MainClass
    {
        public static void Main (String [] args)
        {
            Console.WriteLine("Hello I/O File JSON");

            //string jsonTEMP = @"{""Name"":""HolmesChang"",""Age"":""35""}";

            String fpath = @"UserConfig.json";
            String jsonTEMP = System.IO.File.ReadAllText(fpath);

            var dictTEMP = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonTEMP);

            foreach (KeyValuePair<String, String> Entry in dictTEMP)
            {
                Console.WriteLine("{0} => {1}", Entry.Key, Entry.Value);
            }
            

            Console.WriteLine("Please Enter Any Key To Exit");
            Console.ReadKey();
        }
    }
}

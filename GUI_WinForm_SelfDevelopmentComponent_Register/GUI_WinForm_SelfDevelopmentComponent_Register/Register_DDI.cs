using System;
using System.Collections.Generic;

namespace GUI_WinForm_SelfDevelopmentComponent_Register
{
    class Register_DDI : System.Windows.Forms.TabControl
    {
        private System.String [] ListRegTable = {"B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA"};

        private System.Windows.Forms.TabPage Page_B0h;
        private System.Windows.Forms.TabPage Page_B1h;
        private System.Windows.Forms.TabPage Page_B2h;

        public Register_DDI (System.String fpath)
        {
            // Parsing Register File
            var reader = new System.IO.StreamReader(System.IO.File.OpenRead(fpath));            // Exception Handling Necessity
            System.Collections.Generic.List<System.Collections.Generic.List<string>> ListReg = new System.Collections.Generic.List<System.Collections.Generic.List<string>>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                System.Collections.Generic.List<string> tmp = new System.Collections.Generic.List<string>();
                foreach (string ele in values)
                {
                    if (ele.Replace(" ", "") == "")
                        tmp.Add("-");
                    else
                        tmp.Add(ele);
                }
                ListReg.Add(tmp);
            }

            var ListRegDict = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.String, System.Object>>();
            var ListRegDictElement = new System.Collections.Generic.Dictionary<System.String, System.Object>();
            var reg_param_descr = new System.Collections.Generic.List<System.Collections.Generic.List<System.String>>();
            var reg_param_default = new System.Collections.Generic.List<System.String>();
            System.Boolean PushRegParam = false;
            foreach (var row in ListReg)
            {
                if ((!PushRegParam) && System.Array.Exists(ListRegTable, ele => ele == row[0].Trim()))
                {
                    // Debugging
                    System.Console.WriteLine(row[0]);

                    PushRegParam = true;
                    ListRegDictElement.Add("RegAddr", row[0]);
                    ListRegDictElement.Add("RegAddrNmonic", row[1]);
                    ListRegDictElement.Add("RegParamCnt", 0);
                    continue;
                }
                if (PushRegParam)
                {
                    if (!System.Array.Exists(ListRegTable, ele => ele == row[0].Trim()))
                    {
                        ListRegDictElement["RegParamCnt"] = ((int)ListRegDictElement["RegParamCnt"]) + 1;
                        reg_param_descr.Add(row.GetRange(2, row.Count-2));
                        reg_param_default.Add(row[row.Count-1]);
                    }
                    else
                    {
                        ListRegDictElement.Add("RegParamDescr", new List<List<String>>(reg_param_descr));
                        ListRegDictElement.Add("RegParamDefault", new List<String>(reg_param_default));
                        ListRegDict.Add(new System.Collections.Generic.Dictionary<System.String, System.Object>(ListRegDictElement));

                        // Debugging
                        System.Console.WriteLine(row[0]);

                        ListRegDictElement.Clear();
                        reg_param_descr.Clear();
                        reg_param_default.Clear();
                        PushRegParam = true;
                        ListRegDictElement.Add("RegAddr", row[0]);
                        ListRegDictElement.Add("RegAddrNmonic", row[1]);
                        ListRegDictElement.Add("RegParamCnt", 0);
                    }
                }
            }
            ListRegDictElement.Add("RegParamDescr", new List<List<String>>(reg_param_descr));
            ListRegDictElement.Add("RegParamDefault", new List<String>(reg_param_default));
            ListRegDict.Add(new System.Collections.Generic.Dictionary<System.String, System.Object>(ListRegDictElement));
            // Debugging
            System.Console.WriteLine(ListRegDict.Count + " Register");
            foreach (var RegDict in ListRegDict)
            {
                Console.WriteLine("\n\n\n");
                System.Console.WriteLine(RegDict["RegAddr"]);
                System.Console.WriteLine(RegDict["RegAddrNmonic"]);
                System.Console.WriteLine(RegDict["RegParamCnt"]);
                foreach (var row in ((List<List<String>>) RegDict["RegParamDescr"]))
                {
                    foreach (var col in row)
                    {
                        Console.Write("{0, -10} ", col);
                    }
                    Console.WriteLine("");
                }
                foreach (var ele in ((List<String>) RegDict["RegParamDefault"]))
                {
                    Console.WriteLine(ele);
                }
            }
            // Debugging

            this.Page_B0h = new System.Windows.Forms.TabPage();
            this.Page_B0h.Name = "B0h";
            this.Page_B0h.Text = "B0h";     // Text Showing On Tab

            this.Page_B1h = new System.Windows.Forms.TabPage();


            this.Page_B2h = new System.Windows.Forms.TabPage();

            // Adding TabPage Into TabControl
            this.Controls.Add(this.Page_B0h);
            this.Controls.Add(this.Page_B1h);
            this.Controls.Add(this.Page_B2h);
        }
    }
}

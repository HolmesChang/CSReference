using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Newtonsoft.Json;

namespace GUI_WinForm_SelfDevelopmentComponent_Register
{
    class Register_DDI : System.Windows.Forms.TabControl
    {
        private Int32 RegAddrBitWidth = 8;
        private Int32 RegParamBitWidth = 8;

        private String [] StringArrayRegTable = {"B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA",
                                                 "BB", "BC", "BD", "BE", "BF", "C0", "C6", "C7", "C8", "CC", "E0", "E9",
                                                 "EA"};

        public System.Collections.Generic.List<TabPage> ListTabPage = new List<TabPage>();
        public List<TableLayoutPanel> ListTLP = new List<TableLayoutPanel>();
        public List<Button> ListButton = new List<Button>();
        public List<TextBox> ListTextBox = new List<TextBox>();

        public Register_DDI (System.String fpath)
        {
            // ================================================== //
            //  Parsing UserConfig.json
            // ================================================== //
            String jsonTEMP = System.IO.File.ReadAllText(fpath);
            var dictUserConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonTEMP);
            this.RegAddrBitWidth = Int32.Parse(dictUserConfig["RegAddrBitWidth"]);
            this.RegParamBitWidth = Int32.Parse(dictUserConfig["RegParamBitWidth"]);

            // ================================================== //
            //  Parsing DDI Register Table EXCEL File Into List
            // ================================================== //
            var ReaderFile = new System.IO.StreamReader(System.IO.File.OpenRead(dictUserConfig["fpath_RegTable"]));            // Exception Handling Necessity
            List<List<string>> ListRegTable = new List<List<string>>();
            while (!ReaderFile.EndOfStream)
            {
                var Line = ReaderFile.ReadLine();
                var Tokens = Line.Split(',');

                List<string> Tmp = new List<string>();
                foreach (string Token in Tokens)
                {
                    if (Token.Replace(" ", "") == "")
                        Tmp.Add("-");
                    else
                        Tmp.Add(Token);
                }
                ListRegTable.Add(Tmp);
            }

            // ================================================== //
            //  Parsing ListRegTable Into ListRegDict
            // ================================================== //
            var ListRegDict = new List<System.Collections.Generic.Dictionary<System.String, System.Object>>();
            var ListRegDictElement = new Dictionary<String, Object>();
            var reg_param_descr = new List<List<String>>();
            var reg_param_default = new List<String>();
            System.Boolean PushRegParam = false;
            foreach (var row in ListRegTable)
            {
                if ((!PushRegParam) &&
                    System.Array.Exists(StringArrayRegTable, ele => ele == row[0].Trim()))
                {
                    PushRegParam = true;
                    ListRegDictElement.Add("RegAddr", row[0].Trim());
                    ListRegDictElement.Add("RegAddrNmonic", row[1].Trim());
                    ListRegDictElement.Add("RegParamCnt", 0);
                    continue;
                }
                if (PushRegParam)
                {
                    if (Int32.TryParse(row[1].Trim(), out _))
                    {
                        ListRegDictElement["RegParamCnt"] = ((int)ListRegDictElement["RegParamCnt"]) + 1;
                        reg_param_descr.Add(row.GetRange(2, row.Count - 2));
                        reg_param_default.Add(row[row.Count - 1]);
                    }
                    else
                    {
                        ListRegDictElement.Add("RegParamDescr", new List<List<String>>(reg_param_descr));
                        ListRegDictElement.Add("RegParamDefault", new List<String>(reg_param_default));
                        ListRegDict.Add(new Dictionary<System.String, System.Object>(ListRegDictElement));

                        ListRegDictElement.Clear();
                        reg_param_descr.Clear();
                        reg_param_default.Clear();
                        if (System.Array.Exists(StringArrayRegTable, ele => ele == row[0].Trim()))
                        {
                            PushRegParam = true;
                            ListRegDictElement.Add("RegAddr", row[0].Trim());
                            ListRegDictElement.Add("RegAddrNmonic", row[1].Trim());
                            ListRegDictElement.Add("RegParamCnt", 0);
                            continue;
                        }
                        else
                        {
                            PushRegParam = false;
                            continue;
                        }
                    }
                }
            }
            if (PushRegParam)
            {
                ListRegDictElement.Add("RegParamDescr", new List<List<String>>(reg_param_descr));
                ListRegDictElement.Add("RegParamDefault", new List<String>(reg_param_default));
                ListRegDict.Add(new Dictionary<System.String, System.Object>(ListRegDictElement));
            }

            // Debugging
            /*
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
            */
            // Debugging
            // Parsing ListRegTable Into ListRegDict

            // Forward Struction Of GUI Component Using Result Of Parsing Register File
            foreach (var RegDict in ListRegDict)
            {
                TabPage pageTEMP = new TabPage();
                pageTEMP.Name = ((String) RegDict["RegAddr"]).Trim() + "h";
                pageTEMP.Text = ((String) RegDict["RegAddr"]).Trim() + "h";
                //pageTEMP.BorderStyle = BorderStyle.FixedSingle;
                pageTEMP.AutoScroll = true;

                // Forward Struction Of TableLayoutPanel
                System.Windows.Forms.TableLayoutPanel tlpTEMP = new TableLayoutPanel();
                tlpTEMP.Name = "Tlp_" + (String)RegDict["RegAddr"] + "h";
                tlpTEMP.RowCount = ((Int32)RegDict["RegParamCnt"])*2 + 1 ;
                for (Int32 i=0; i<tlpTEMP.RowCount; i++)
                {
                    //tlpTEMP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.0F/tlpTEMP.RowCount));
                    tlpTEMP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30));
                }
                tlpTEMP.ColumnCount = RegParamBitWidth + 1;
                for (Int32 i=0; i<tlpTEMP.ColumnCount; i++)
                {
                    if (i == 0)
                    {
                        tlpTEMP.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
                        continue;
                    }
                    tlpTEMP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.0F/tlpTEMP.ColumnCount));
                }
                //tlpTEMP.BorderStyle = BorderStyle.Fixed3D;
                //tlpTEMP.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                tlpTEMP.Margin = new Padding(15);
                ListTLP.Add(tlpTEMP);
                pageTEMP.Controls.Add(tlpTEMP);

                // Forward Struction Of RD/WR Button
                System.Windows.Forms.Button btnRD = new Button();
                btnRD.Name = "BtnRD_" + (String)RegDict["RegAddr"] + "h";
                btnRD.Text = "RD";
                btnRD.Dock = DockStyle.Fill;
                btnRD.Font = new System.Drawing.Font("PMingLiU", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                btnRD.Click += BtnRDWR_LeftClicking;
                tlpTEMP.Controls.Add(btnRD, 1, 0);
                tlpTEMP.SetColumnSpan(btnRD, 1);
                ListButton.Add(btnRD);

                Button btnWR = new Button();
                btnWR.Name = "BtnWR_" + (String)RegDict["RegAddr"] + "h";
                btnWR.Text = "WR";
                btnWR.Dock = DockStyle.Fill;
                btnWR.Font = new System.Drawing.Font("PMingLiU", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                btnWR.Click += BtnRDWR_LeftClicking;
                tlpTEMP.Controls.Add(btnWR, 2, 0);          // 2nd Parameter = Column. 3rd Parameter = Row.
                tlpTEMP.SetColumnSpan(btnWR, 1);
                ListButton.Add(btnWR);

                Int32 row, col;
                row = 1;
                col = 1;
                foreach (var ListRegParamDescr in ((List<List<String>>)RegDict["RegParamDescr"]))
                {
                    col = 1;
                    foreach (var Descr in ListRegParamDescr)
                    {
                        if (col == 9)
                            break;

                        Label lblTEMP = new Label();
                        lblTEMP.Text = Descr;
                        lblTEMP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                        lblTEMP.Dock = DockStyle.Fill;
                        lblTEMP.ForeColor = Color.FromArgb(0x000000FF);
                        tlpTEMP.Controls.Add(lblTEMP, col, row);

                        col++;
                    }
                    row += 2;
                }

                row = 2;
                foreach (var RegParamDefault in ((List<String>)RegDict["RegParamDefault"]))
                {
                    Label lblTEMP = new Label();
                    lblTEMP.Text = String.Format("{0:00}", ((Int32)(row / 2)));
                    tlpTEMP.Controls.Add(lblTEMP, 0, row);
                    for (Int32 j=0; j<RegParamBitWidth; j++)
                    {
                        TextBox tbTEMP = new TextBox();
                        tbTEMP.Name = "TextBox_" + (String)RegDict["RegAddr"] + "h_{row}_{j}";
                        tbTEMP.TextAlign = HorizontalAlignment.Center;
                        tbTEMP.Dock = DockStyle.Fill;
                        tbTEMP.Font = new System.Drawing.Font("PMingLiU", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                        if ((RegParamDefault.Trim() != "-") && (RegParamDefault.Trim() != "N/A"))
                            //if (Int32.TryParse(RegParamDefault.Trim().Substring(2, 2), out _))
                            if (Uri.IsHexDigit(RegParamDefault.Trim()[2]) && Uri.IsHexDigit(RegParamDefault.Trim()[3]))
                                tbTEMP.Text = ((Int32.Parse(RegParamDefault.Trim().Substring(2, 2), System.Globalization.NumberStyles.HexNumber) >> (7-j)) & 0x1).ToString();
                        else
                            tbTEMP.Text = "";
                        tbTEMP.KeyPress += TB_KeyPress;
                        tbTEMP.DoubleClick += TB_DoubleClick;
                        ListTextBox.Add(tbTEMP);
                        tlpTEMP.Controls.Add(tbTEMP, j+1, row);
                    }
                    row += 2;
                }

                ListTabPage.Add(pageTEMP);
            }

            // Adding TabPage Into TabControl
            foreach (var Page in ListTabPage)
            {
                this.Controls.Add(Page);
            }

            // Only For Debugging
            this.Selecting += new TabControlCancelEventHandler(TabControl_Selecting);
        }

        // ================================================== //
        //
        // ================================================== //
        void TabControl_Selecting (object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;

            Console.WriteLine(current.Name);
            // Validate the current page. To cancel the select, use:
            //e.Cancel = true;
        }

        // ================================================== //
        //
        // ================================================== //
        void BtnRDWR_LeftClicking (object sender, EventArgs e)
        {
            Console.WriteLine(((Button) sender).Name);
        }
    
        void TB_KeyPress (object sender, EventArgs e)
        {
            if (((KeyPressEventArgs)e).KeyChar == ((char)Keys.D0))
            {
                ((TextBox)sender).Text = String.Empty;
                //((TextBox)sender).Text = "0";
            }

            else if (((KeyPressEventArgs)e).KeyChar == ((char)Keys.D1))
            {
                ((TextBox)sender).Text = String.Empty;
                //((TextBox)sender).Text = "1";
            }
            else
                ((KeyPressEventArgs)e).Handled = true;
        }

        void TB_DoubleClick (object sender, EventArgs e)
        {
            String tmp = "";
            if (Int32.TryParse(((TextBox)sender).Text, out _))
                tmp = ((TextBox)sender).Text;
            if (tmp == "0")
                ((TextBox)sender).Text = "1";
            if (tmp == "1")
                ((TextBox)sender).Text = "0";
        }
    }
}

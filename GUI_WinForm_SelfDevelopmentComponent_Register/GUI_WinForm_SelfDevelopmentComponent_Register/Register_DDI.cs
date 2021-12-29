using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GUI_WinForm_SelfDevelopmentComponent_Register
{
    class Register_DDI : System.Windows.Forms.TabControl
    {
        private Int32 RegParamBitWidth = 8;

        private System.String [] ListRegTable = {"B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA",
                                                 "BB", "BC", "BD", "BE", "BF", "C0", "C6", "C7", "C8", "CC", "E0", "E9",
                                                 "EA"};

        public List<TabPage> ListTabPage = new List<TabPage>();
        public List<TableLayoutPanel> ListTLP = new List<TableLayoutPanel>();
        public List<Button> ListButton = new List<Button>();
        public List<TextBox> ListTextBox = new List<TextBox>();

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
                        if (Int32.TryParse(row[1].Trim(), out _))
                        {
                            ListRegDictElement["RegParamCnt"] = ((int)ListRegDictElement["RegParamCnt"]) + 1;
                            reg_param_descr.Add(row.GetRange(2, row.Count - 2));
                            reg_param_default.Add(row[row.Count - 1]);
                        }
                        else
                        {
                            break;
                        }
                            
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
            // Parsing Register File

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
                tlpTEMP.ColumnCount = RegParamBitWidth;
                for (Int32 i=0; i<tlpTEMP.ColumnCount; i++)
                {
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
                tlpTEMP.Controls.Add(btnRD, 0, 0);
                tlpTEMP.SetColumnSpan(btnRD, 1);
                ListButton.Add(btnRD);

                Button btnWR = new Button();
                btnWR.Name = "BtnWR_" + (String)RegDict["RegAddr"] + "h";
                btnWR.Text = "WR";
                btnWR.Dock = DockStyle.Fill;
                btnWR.Font = new System.Drawing.Font("PMingLiU", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                btnWR.Click += BtnRDWR_LeftClicking;
                tlpTEMP.Controls.Add(btnWR, 1, 0);          // 2nd Parameter = Column. 3rd Parameter = Row.
                tlpTEMP.SetColumnSpan(btnWR, 1);
                ListButton.Add(btnWR);

                Int32 row, col;
                row = 1;
                col = 0;
                foreach (var ListRegParamDescr in ((List<List<String>>)RegDict["RegParamDescr"]))
                {
                    col = 0;
                    foreach (var Descr in ListRegParamDescr)
                    {
                        if (col == 8)
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
                    for(Int32 j=0; j<RegParamBitWidth; j++)
                    {
                        TextBox tbTEMP = new TextBox();
                        tbTEMP.Name = "TextBox_" + (String)RegDict["RegAddr"] + "h_{row}_{j}";
                        tbTEMP.TextAlign = HorizontalAlignment.Center;
                        tbTEMP.Dock = DockStyle.Fill;
                        tbTEMP.Font = new System.Drawing.Font("PMingLiU", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
                        if (RegParamDefault.Trim() != "-")
                            //if (Int32.TryParse(RegParamDefault.Trim().Substring(2, 2), out _))
                            if (Uri.IsHexDigit(RegParamDefault.Trim()[2]) && Uri.IsHexDigit(RegParamDefault.Trim()[3]))
                                tbTEMP.Text = ((Int32.Parse(RegParamDefault.Trim().Substring(2, 2), System.Globalization.NumberStyles.HexNumber) >> (7-j)) & 0x1).ToString();
                        else
                            tbTEMP.Text = "";
                        tbTEMP.KeyPress += TB_KeyPress;
                        tbTEMP.DoubleClick += TB_DoubleClick;
                        ListTextBox.Add(tbTEMP);
                        tlpTEMP.Controls.Add(tbTEMP, j, row);
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
            UInt32 tmp = Convert.ToUInt32(((TextBox)sender).Text);
            if (tmp == 0)
                ((TextBox)sender).Text = "1";
            else
                ((TextBox)sender).Text = "0";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace GUI_WinForm_SelfDevelopmentComponent_Register
{
    class MainClass : System.Windows.Forms.Form
    {
        private const string fpath = @"D:\Publication\CS Reference\GUI_WinForm_SelfDevelopmentComponent_Register\PA1013A.csv";
        private Register_DDI Register_DDI_PA1013 = new Register_DDI(fpath);
        public MainClass ()
        {
            // Setting Of This
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Text = "PA1013";
            this.Resize += MainClass_Resize;

            // Declaration AND Setting Of Member GUI Component Of This
            this.Register_DDI_PA1013.Location = new System.Drawing.Point(15, 15);
            this.Register_DDI_PA1013.Size = new Size(this.ClientSize.Width-30, this.ClientSize.Height-30);
            this.Register_DDI_PA1013.Anchor = System.Windows.Forms.AnchorStyles.None;
            foreach (var Page in this.Register_DDI_PA1013.ListTabPage)
            {
                Page.Size = new Size(Page.Parent.Size.Width, Page.Parent.Size.Height);
                Page.ClientSize = new Size(Page.Parent.Size.Width, Page.Parent.Size.Height);
            }
            foreach (var TLP in this.Register_DDI_PA1013.ListTLP)
            {
                //TLP.Size = new Size(TLP.Parent.Size.Width-30, TLP.Parent.Size.Height-30);
                //TLP.Width = TLP.Parent.Size.Width - 30;
                TLP.MinimumSize = new Size(TLP.Parent.Size.Width - 30, 0);
                TLP.AutoSize = true;
                TLP.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
            this.Controls.Add(this.Register_DDI_PA1013);
        }

        // ================================================== //
        //  WinForm Responsive Design
        // ================================================== //
        private void MainClass_Resize(object sender, System.EventArgs e)
        {
            Size tmp = this.ClientSize;
            this.Register_DDI_PA1013.Location = new System.Drawing.Point(15, 15);           // ReAssigning Location Is Necessary
            this.Register_DDI_PA1013.Size = new Size(tmp.Width - 30, tmp.Height - 30);
            //foreach (var Page in this.Register_DDI_PA1013.ListTabPage)
            //{
            //    Page.Size = new Size(Page.Parent.Size.Width, Page.Parent.Size.Height);
            //    Page.ClientSize = new Size(Page.Parent.Size.Width, Page.Parent.Size.Height);
            //}
            //foreach (var TLP in this.Register_DDI_PA1013.ListTLP)
            //{
            //    //TLP.Size = new Size(TLP.Parent.Size.Width-30, TLP.Parent.Size.Height-30);
            //    //TLP.Width = TLP.Parent.Size.Width - 30;
            //    TLP.MinimumSize = new Size(TLP.Parent.Size.Width - 30, 0);
            //    TLP.AutoSize = true;
            //    TLP.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //}
        }

        [System.STAThread]
        public static void Main (System.String [] args)
        {
            MainClass MyForm = new MainClass();

            System.Windows.Forms.Application.Run(MyForm);
        }
    }
}

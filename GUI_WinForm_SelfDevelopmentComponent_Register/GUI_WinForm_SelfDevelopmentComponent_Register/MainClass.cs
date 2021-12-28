namespace GUI_WinForm_SelfDevelopmentComponent_Register
{
    class MainClass : System.Windows.Forms.Form
    {
        public MainClass ()
        {
            // Setting Of This
            this.Width = 600;
            this.Height = 300;

            const string fpath = @"D:\CS Reference\GUI_WinForm_SelfDevelopmentComponent_Register\PA1013A.csv";

            // Declaration AND Definition Of Member GUI Component
            Register_DDI Register_DDI_PA1013 = new Register_DDI(fpath);
            Register_DDI_PA1013.Width = 580;
            Register_DDI_PA1013.Height = 280;
            Register_DDI_PA1013.Anchor = System.Windows.Forms.AnchorStyles.None;

            this.Controls.Add(Register_DDI_PA1013);
        }

        [System.STAThread]
        public static void Main (System.String [] args)
        {
            MainClass MyForm = new MainClass();

            System.Windows.Forms.Application.Run(MyForm);
        }
    }
}

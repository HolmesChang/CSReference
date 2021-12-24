public class MainClass : System.Windows.Forms.Form
{

    public MainClass()
    {
        this.Text = "Hello GUI WinForm";
    }
    [System.STAThread]
    public static void Main(string [] args)
    {
        MainClass MyForm = new MainClass();
        // The Application.Run method processes messages from the operating system
        // to your application. If you comment out the next line of code,
        // your application will compile and execute, but because it is not in the // message loop, it will exit after an instance of the form is created.  
        System.Windows.Forms.Application.Run(MyForm);
    }
}
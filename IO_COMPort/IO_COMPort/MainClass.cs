namespace IO_COMPort
{
    class MainClass
    {
        static void Main (System.String [] args)
        {
            System.Console.WriteLine("Hello IO COMPort");

            //System.IO.Ports.SerialPort FLDVB2P0 = new System.IO.Ports.SerialPort();
            System.Console.WriteLine("System.IO.Ports Not Support In DOTNET FrameWork 4.5.2");

            System.Console.WriteLine("Please Enter Any Key To Exit");
            System.Console.ReadKey();
        }
    }
}

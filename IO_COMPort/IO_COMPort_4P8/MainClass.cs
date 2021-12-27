namespace IO_COMPort_4P8
{
    class MainClass
    {
        static void Main (System.String [] args)
        {
            System.Console.WriteLine("Hello IO COM Port In DOTNET FrameWork 4.8");

            //System.Collections.Generic.List<System.String> ListPort = new System.Collections.Generic.List<System.String>();
            System.String [] ListPort;

            System.IO.Ports.SerialPort FLDVB2P0 = new System.IO.Ports.SerialPort();         // System AND System.IO.Ports Must
            FLDVB2P0.BaudRate = 115200;
            FLDVB2P0.Parity = System.IO.Ports.Parity.None;
            FLDVB2P0.DataBits = 8;
            FLDVB2P0.StopBits = System.IO.Ports.StopBits.One;

            ListPort = System.IO.Ports.SerialPort.GetPortNames();
            foreach (System.String Port in ListPort)
            {
                System.Console.WriteLine(Port);
            }
            FLDVB2P0.PortName = "COM3";

            if (FLDVB2P0.IsOpen)
                FLDVB2P0.Close();
            FLDVB2P0.Open();

            // COM Port WR
            FLDVB2P0.Write("UI START\r\n");
            System.Threading.Thread.Sleep(5);
            FLDVB2P0.Write("252013600\r\n");
            System.Threading.Thread.Sleep(5);
            FLDVB2P0.Write("2611FF0000\r\n");
            System.Threading.Thread.Sleep(5);

            // Sleeping For Response
            System.Threading.Thread.Sleep(100);

            // COM Port RD
            int ret = 0;
            System.Byte [] buf = new System.Byte[1024];
            //byte[] buf = new byte[1024];
            ret = FLDVB2P0.Read(buf, 0, 1024);
            System.Console.WriteLine("RX Count: " + ret);
            //System.Console.WriteLine(buf.ToString());         // Not Working
            System.Console.WriteLine(System.Text.Encoding.Default.GetString(buf));

            // Closing COM Port
            FLDVB2P0.Close();

            System.Console.WriteLine("Please Enter Any Key To Exit");
            System.Console.ReadKey();
        }
    }
}

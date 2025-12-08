using Microsoft.Win32;
using System.IO.Pipes;
using System.Text;

namespace GuiPlays
{
    internal static class Program
    {
        //public static Form1 MainForm;
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew;
            using Mutex mutex = new Mutex(true, "CuongPlayer_SingleInstance", out createdNew);

            if (!createdNew)
            {
                if (args.Length > 0)
                {
                    try
                    {
                        using var client = new NamedPipeClientStream(".", "CuongPlayerPipe", PipeDirection.Out);
                        client.Connect(300);
                        byte[] msg = Encoding.UTF8.GetBytes(args[0]);
                        client.Write(msg, 0, msg.Length);
                    }
                    catch { }
                }
                return;
            }

            ApplicationConfiguration.Initialize();

            try
            {
                RegisterProtocolHKCU("cuongplayer");
            }
            catch { }

            Form1 form1 = new Form1(args);

            Thread pipeThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        using var server = new NamedPipeServerStream("CuongPlayerPipe", PipeDirection.In);
                        server.WaitForConnection();

                        using StreamReader reader = new StreamReader(server);
                        string incoming = reader.ReadToEnd();

                        if (!string.IsNullOrWhiteSpace(incoming))
                        {
                            form1.ReceiveExternalUrl(incoming);
                        }
                    }
                    catch { }
                }
            });

            pipeThread.IsBackground = true;
            pipeThread.Start();

            Application.Run(form1);
        }


        private static void RegisterProtocolHKCU(string proto)
        {
            string exePath = Application.ExecutablePath;


            using (var baseKey = Registry.CurrentUser.CreateSubKey($"Software\\Classes\\{proto}"))
            {
                if (baseKey == null) return;
                baseKey.SetValue("", $"URL: {proto} Protocol");
                baseKey.SetValue("URL Protocol", "");

                using (var iconKey = baseKey.CreateSubKey("DefaultIcon"))
                {
                    iconKey.SetValue("", exePath + ",1");
                }

                using (var cmdKey = baseKey.CreateSubKey("shell\\open\\command"))
                {
                    cmdKey.SetValue("", $"\"{exePath}\" \"%1\"");
                }
            }
        }
    }
}
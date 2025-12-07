using Microsoft.Win32;

namespace GuiPlays
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RegisterProtocol();
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1(args));
        }
        private static void RegisterProtocol()
        {
            string exePath = Application.ExecutablePath;

            RegistryKey key = Registry.ClassesRoot.OpenSubKey("cuongplayer");
            if (key != null)
            {
                key.Close();
                return;
            }

            key = Registry.ClassesRoot.CreateSubKey("cuongplayer");
            key.SetValue("", "URL: MyApp Protocol");
            key.SetValue("URL Protocol", "");

            var iconKey = key.CreateSubKey("DefaultIcon");
            iconKey.SetValue("", exePath + ",1");

            var commandKey = key.CreateSubKey(@"shell\open\command");
            commandKey.SetValue("", $"\"{exePath}\" \"%1\"");
        }
    }
}
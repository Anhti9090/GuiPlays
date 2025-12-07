
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace GuiPlays
{
    public partial class Form1 : Form
    {
        private string url = "";

        public Form1(string[] args)
        {
            InitializeComponent();
            if (args != null && args.Length > 0)
            {
                url = args[0].Replace("cuongplayer://", "https://");
            }
        }
        private void OpenVLC(string url)
        {
            string cmd;

            if (url.Contains("cdn-1.cuong.one/video/"))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    cmd = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
                    Process.Start(cmd, $"\"{url}\"");
                }
            }
            else
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    cmd = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
                    Process.Start(cmd, $"\"{url}\"");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            OpenVLC(text);
            textBox1.Clear();
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
            {
                textBox1.Text = url;
            }
        }
    }
}


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
            Uri uri = new Uri(url);
            string cmd;
            if (uri.Host == "cdn-1.cuong.one")
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    string uga = $"\"{url}\"";
                    cmd = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
                    Process.Start(cmd, uga);
                }
            }
            else
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    string uga = $"\"{url}\"";
                    cmd = @"C:\Program Files (x86)\VideoLAN\VLC\vlc.exe";
                    Process.Start(cmd, uga);
                }
            }
        }
        public void ReceiveExternalUrl(string url)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => ReceiveExternalUrl(url)));
                return;
            }

            textBox1.Text = url.Replace("cuongplayer://", "https://");
            OpenVLC(url);
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
            textBox1.Text = textBox1.Text.Trim().Replace("cuongplayer://", "https://");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(url))
            {
                textBox1.Text = url.Replace("cuongplayer://", "https://");
            }
        }
    }
}

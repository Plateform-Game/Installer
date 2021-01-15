using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plateform_Launcher_Installer
{
    public partial class Install : Form
    {
        public Install()
        {
            InitializeComponent();

            this.FormClosing += Install_FormClosing;

            this.Shown += Install_Load;

            this.button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Install_Load(object sender, EventArgs e)
        {
            label1.Text = "Status : Checking for Internet connection...";
            Process check_internet = Process.Start(Application.ExecutablePath, "--ci");
            check_internet.WaitForExit();
            if (check_internet.ExitCode == 0)
            {
                textBox1.Text += "Check passed, downloading files...\n";
                label1.Text = "Status : Downloading file...";
               
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PlateformLauncher\\")) Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PlateformLauncher\\");
                new WebClient().DownloadFile("https://github.com/Plateform-Game/Launcher/raw/master/bin/Debug/Plateform-Launcher.exe", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PlateformLauncher\\PlateformLauncher.exe");
                new WebClient().DownloadFile("https://github.com/Plateform-Game/Launcher/raw/master/bin/Debug/Newtonsoft.Json.dll", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PlateformLauncher\\Newtonsoft.Json.dll");
                new WebClient().DownloadFile("https://github.com/Plateform-Game/Launcher/raw/master/bin/Debug/RestSharp.dll", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PlateformLauncher\\RestSharp.dll");

                textBox1.Text += "Downloaded all files, creating desktop shortcut...";
                label1.Text = "Status : Creating Desktop shortcut...";

                button1.Enabled = true;
            }
            else
            {
                textBox1.Text += "Check failed, exiting...\n";
                MessageBox.Show("Error: Please check your internet connection, must be connected for continue.\n\nCode: 502", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void Install_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

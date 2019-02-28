using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Update_Example
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        private void Update_Load(object sender, EventArgs e)
        {
            string downloadUrl = "https://example.com/file.exe"; //File url to download
            string path = Application.LocalUserAppDataPath; //Installed PATH as an example: Appdata> Your application
            string folder = path + "\\";
            string filename = "Setup.exe"; //The name of the file to download (can be changed)
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += wc_DownloadProgressChanged; //Information and event args for progress bar
                    client.DownloadFileCompleted += wc_DownloadFileCompleted; // Event args for transactions that will be completed when the download is complete
                    client.DownloadFileAsync(new Uri(downloadUrl), folder + filename); // File download process
                }

            }
            catch
            {
                MessageBox.Show("Error.");
            }
        }
        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage; // Set progress bar percentage
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = 0;

            if (e.Cancelled) //Cancelled
            {
                MessageBox.Show("Cancelled.");
                return;
            }

            if (e.Error != null) //When any error occurs
            {
                MessageBox.Show("Error.");

                return;
            }
            string path = Application.LocalUserAppDataPath;
            string folder = path + "\\";
            string filename = "Setup.exe";
            System.Diagnostics.Process.Start(folder + filename); //Start the file we downloaded
            Application.Exit(); //Close the currently open application
        }

    }
}


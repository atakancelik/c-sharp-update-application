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
            string indirilecek = "Ornek Olarak Uygulamanızın Yeni Setup Dosyası"; //İndirilecek Dosya Adresi
            string path = Application.LocalUserAppDataPath; //Kurulucak PATH örnek olarak : Appdata>Uygulamanız
            string klasor = path + "\\";
            string dosyaAdi = "Setup.exe"; //İndirilecek Dosyanın adı (Değiştirilebilir)
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += wc_DownloadProgressChanged; //Progress bar için bilgi ve event args
                    client.DownloadFileCompleted += wc_DownloadFileCompleted; // İndirme işlemi tamamlandığında olacak işlemler için event args
                    client.DownloadFileAsync(new Uri(indirilecek), klasor + dosyaAdi); // Dosya indirme işlemi
                }

            }
            catch
            {
                MessageBox.Show("Güncelleme Sırasında Bir Hata Oluştu.");
            }
        }
        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage; // Progress bar yüzdesi belirleme
        }

        private void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value = 0;

            if (e.Cancelled) //İptal edildiğinde
            {
                MessageBox.Show("İndirme İşlemi İptal Edildi.");
                return;
            }

            if (e.Error != null) //Herhangi bir hata oluştuğunda
            {
                MessageBox.Show("BELSAN Güncellenirken Bir Hata Oluştu.");

                return;
            }
            string path = Application.LocalUserAppDataPath;
            string klasor = path + "\\";
            string dosyaAdi = "Setup.exe";
            System.Diagnostics.Process.Start(klasor + dosyaAdi); //İndirdiğimiz dosyayı başlatma
            Application.Exit(); //Şuan açık olan uygulamayı kapatma
        }

    }
}


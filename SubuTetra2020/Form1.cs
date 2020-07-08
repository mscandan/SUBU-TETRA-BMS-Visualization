using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace SubuTetra2020
{
    public partial class Form1 : Form
    {
        /* Degisken tanimlamalari */
        SerialPort port = new SerialPort();

        /* Degisken tanimlamalari*/
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        // Bilgisayardan COM portlari alir ve gerekli yerde listeler
        private void PortlariListele()
        {
            string[] ports = SerialPort.GetPortNames();
            PortNamesComboBox.Items.AddRange(ports);
        }
        // SerialPort konfigurasyonu
        private void SerialPortKonf()
        {
            port.PortName = PortNamesComboBox.SelectedItem.ToString();
            port.BaudRate = Convert.ToInt32(baudrateTextBox.Text);
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            // port.DataBits = 8; // okunacak verinin biti
            port.Open();
            // port.BytesToRead = 8; arastir
        }
        // Bu bir thread olacak
        private void PillerBirOn(string tamVeri)
        {
            if (tamVeri.Contains("A1"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil1v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A2"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil2v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A3"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil3v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A4"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil4v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A5"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil5v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A6"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil6v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A7"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil7v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A8"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil8v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A9"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil9v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A10"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil10v.Text = kullanilacakVeri[1];
            }
        }
        private void PillerOnbirYirmi(string tamVeri)
        {
            if (tamVeri.Contains("A11"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil11v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A12"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil12v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A13"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil13v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A14"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil14v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A15"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil15v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A16"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil16v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A17"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil17v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A18"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil18v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A19"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil19v.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("A20"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                pil20v.Text = kullanilacakVeri[1];
            }
        }
        // burası bir thread olacak
        private void Sicaklik(string tamVeri)
        {
            if (tamVeri.Contains("B1"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                sensor1sicaklik.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("B2"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                sensor2sicaklik.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("B3"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                sensor3sicaklik.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("B4"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                sensor4sicaklik.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("B5"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                sensor5sicaklik.Text = kullanilacakVeri[1];
            }
            if (tamVeri.Contains("C1"))
            {
                string[] kullanilacakVeri = tamVeri.Split('|');
                genelAmperLabel.Text = kullanilacakVeri[1];
            }
        }
        // burası bir thread olacak
        private void GenelHesap()
        {
            double toplamVolt = Convert.ToInt32(pil1v.Text) + Convert.ToInt32(pil2v.Text) + Convert.ToInt32(pil3v.Text) + Convert.ToInt32(pil4v.Text) + Convert.ToInt32(pil5v.Text) + Convert.ToInt32(pil6v.Text) + Convert.ToInt32(pil7v.Text) + Convert.ToInt32(pil8v.Text) + Convert.ToInt32(pil9v.Text) + Convert.ToInt32(pil10v.Text) + Convert.ToInt32(pil11v.Text) + Convert.ToInt32(pil12v.Text) + Convert.ToInt32(pil13v.Text) + Convert.ToInt32(pil14v.Text) + Convert.ToInt32(pil5v.Text) + Convert.ToInt32(pil16v.Text) + Convert.ToInt32(pil7v.Text) + Convert.ToInt32(pil18v.Text) + Convert.ToInt32(pil19v.Text) + Convert.ToInt32(pil20v.Text);
            genelVoltLabel.Text = Convert.ToString(toplamVolt);
            double genelAmper = Convert.ToInt32(genelAmperLabel.Text);
            genelWattLabel.Text = Convert.ToString(toplamVolt * genelAmper);
        }
        // Veri okuma
        private void VeriOku()
        {
            string okunan = port.ReadExisting();
            string[] ayrilmisVeriler = okunan.Split(Environment.NewLine);
            if (ayrilmisVeriler.Length == 28)
            {
                for (int i = 0; i < ayrilmisVeriler.Length; i++)
                {
                    // thread 1 -> pil 1 - pil 10 arası kontrol ve atama
                    PillerBirOn(ayrilmisVeriler[i]);
                    // thread 2 -> pil 11 - pil 20 arası kontrol ve atama
                    PillerOnbirYirmi(ayrilmisVeriler[i]);
                    // thread3 -> sıcaklık sensörleri ve amper kontrol ve atama
                    Sicaklik(ayrilmisVeriler[i]);
                }
                // thread 4 -> genel hesaplamalar ve atamalar
                GenelHesap();
            }
            dataKonsol.Text += okunan;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage1");
            PortlariListele();
        }

        private void portBaglanButton_Click(object sender, EventArgs e)
        {
            if (PortNamesComboBox.SelectedIndex < 0)
            {
                baglantiKontrolLabel.ForeColor = Color.Red;
                baglantiKontrolLabel.Text = "COM Portu bulunamadı";
            }
            else if (baudrateTextBox.Text == "")
            {
                baglantiKontrolLabel.ForeColor = Color.Red;
                baglantiKontrolLabel.Text = "Baudrate değerini giriniz";
            }
            else
            {
                baglantiKontrolLabel.ForeColor = Color.Green;
                baglantiKontrolLabel.Text = "Bağlantı Başarılı";
                timer1.Enabled = true;
                SerialPortKonf();
            }
        }

        private void logKaydiButon_Click(object sender, EventArgs e)
        {
            var filePath = new OpenFileDialog();
            if (filePath.ShowDialog() == DialogResult.OK)
            {
                logKaydiYolTextBox.Text = filePath.FileName;
            }

            if (logKaydiCheckBox.Checked == true)
            {
                // log kaydı tutulacak
            }
            else
            {
                // log kaydı tutulmadan devam edilecek
            }

        }



        private void dataKonsol_TextChanged(object sender, EventArgs e)
        {
            dataKonsol.SelectionStart = dataKonsol.Text.Length;
            dataKonsol.ScrollToCaret();
            dataKonsol.Refresh();
        }

        private void baglantiKesButon_Click(object sender, EventArgs e)
        {
            port.Close();
            timer1.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            VeriOku();
        }
    }
}

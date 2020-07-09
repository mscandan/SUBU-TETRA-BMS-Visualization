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
        double max, min, fark, maxSicaklik, genelVolt;
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
            port.DataBits = 8; // okunacak verinin biti
            port.Open();
            // port.BytesToRead = 8; arastir
        }
        private double PilDegerHesabi(string veri)
        {
            if(veri.Contains('A') || veri.Contains('B') || veri.Contains('C'))
            {
                return 0;
            } else
            {
                double deger = Convert.ToDouble(veri);
                deger = deger / 100.0;
                genelVolt += deger;
                return deger;
                
            }
            
        }
        private double SicaklikDegerHesabi(string veri)
        {
            if (veri.Contains('A') || veri.Contains('B') || veri.Contains('C'))
            {
                return 0;
            } else
            {
                double deger = Convert.ToDouble(veri);
                deger = deger / 10.0;
                return deger;
                
            }
                
        }
        // Max sıcaklık, min-max gerilim hesabı
        private void GenelHesap(string [] ayrilmisVeriler)
        {
            max = Convert.ToDouble(ayrilmisVeriler[1]); // ilk veriyi max seçtik
            min = Convert.ToDouble(ayrilmisVeriler[1]); // ilk veriyi min seçtik
            for(int i = 3; i < 40; i = i + 2)
            {
                if(Convert.ToDouble(ayrilmisVeriler[i]) > max)
                {
                    max = Convert.ToDouble(ayrilmisVeriler[i]);
                }
                if(Convert.ToDouble(ayrilmisVeriler[i]) < min)
                {
                    min = Convert.ToDouble(ayrilmisVeriler[i]);
                }
            }
            minVLabel.Text = (min / 100.0).ToString();
            maxVLabel.Text = (max / 100.0).ToString();
            vFarkLabel.Text = ((max - min) / 100.0).ToString();
            // sıcaklık
            maxSicaklik = Convert.ToDouble(ayrilmisVeriler[41]);
            for(int i = 43; i < 50; i = i + 2)
            {
                if(Convert.ToDouble(ayrilmisVeriler[i]) > maxSicaklik)
                {
                    maxSicaklik = Convert.ToDouble(ayrilmisVeriler[i]);
                }
            }
            maxSicaklikLabel.Text = (maxSicaklik / 10.0).ToString();
        }
        // progress bar hesabı
        private void ProgressBarHesabi(double toplamGerilim)
        {
            // max gerilim 80 - min gerilim 60
            if(Convert.ToInt32(toplamGerilim) > 60)
            {
                int yuzde = (Convert.ToInt32(toplamGerilim) - 60) * 5;
                sarjProgress.Value = yuzde;
                if (sarjProgress.Value > 0 && sarjProgress.Value < 30)
                {
                    sarjProgress.ForeColor = Color.Red;
                }
                if (sarjProgress.Value > 30 && sarjProgress.Value < 70)
                {
                    sarjProgress.ForeColor = Color.Orange;
                }
                if(sarjProgress.Value > 70 && sarjProgress.Value <= 100)
                {
                    sarjProgress.ForeColor = Color.Green;
                }
            }
            else
            {
                sarjProgress.Value = 0;
            }
            
        }
        // Veri okuma
        private void VeriOku(string okunanVeri)
        {
            string[] ayrilmisVeriler = okunanVeri.Split('|');
            if (ayrilmisVeriler.Length == 53 && okunanVeri[okunanVeri.Length -1] == '|' && okunanVeri[0] == 'A' && okunanVeri[1] == '1')
            {
                // okunan veriyi konsola loglama
                dataKonsol.Text += okunanVeri;
                dataKonsol.Text += Environment.NewLine;
                // pil atamaları
                pil1v.Text = PilDegerHesabi(ayrilmisVeriler[1]).ToString();
                pil2v.Text = PilDegerHesabi(ayrilmisVeriler[3]).ToString();
                pil3v.Text = PilDegerHesabi(ayrilmisVeriler[5]).ToString();
                pil4v.Text = PilDegerHesabi(ayrilmisVeriler[7]).ToString();
                pil5v.Text = PilDegerHesabi(ayrilmisVeriler[9]).ToString();
                pil6v.Text = PilDegerHesabi(ayrilmisVeriler[11]).ToString();
                pil7v.Text = PilDegerHesabi(ayrilmisVeriler[13]).ToString();
                pil8v.Text = PilDegerHesabi(ayrilmisVeriler[15]).ToString();
                pil9v.Text = PilDegerHesabi(ayrilmisVeriler[17]).ToString();
                pil10v.Text = PilDegerHesabi(ayrilmisVeriler[19]).ToString();
                pil11v.Text = PilDegerHesabi(ayrilmisVeriler[21]).ToString();
                pil12v.Text = PilDegerHesabi(ayrilmisVeriler[23]).ToString();
                pil13v.Text = PilDegerHesabi(ayrilmisVeriler[25]).ToString();
                pil14v.Text = PilDegerHesabi(ayrilmisVeriler[27]).ToString();
                pil15v.Text = PilDegerHesabi(ayrilmisVeriler[29]).ToString();
                pil16v.Text = PilDegerHesabi(ayrilmisVeriler[31]).ToString();
                pil17v.Text = PilDegerHesabi(ayrilmisVeriler[33]).ToString();
                pil18v.Text = PilDegerHesabi(ayrilmisVeriler[35]).ToString();
                pil19v.Text = PilDegerHesabi(ayrilmisVeriler[37]).ToString();
                pil20v.Text = PilDegerHesabi(ayrilmisVeriler[39]).ToString();
                // sıcaklık atamaları
                sensor1sicaklik.Text = SicaklikDegerHesabi(ayrilmisVeriler[41]).ToString();
                sensor2sicaklik.Text = SicaklikDegerHesabi(ayrilmisVeriler[43]).ToString();
                sensor3sicaklik.Text = SicaklikDegerHesabi(ayrilmisVeriler[45]).ToString();
                sensor4sicaklik.Text = SicaklikDegerHesabi(ayrilmisVeriler[47]).ToString();
                sensor5sicaklik.Text = SicaklikDegerHesabi(ayrilmisVeriler[49]).ToString();
                // amper ataması
                genelAmperLabel.Text = ayrilmisVeriler[51];

                if(genelVolt.ToString().Length > 5)
                {
                    genelVoltLabel.Text = genelVolt.ToString().Substring(0, 5);
                } else
                {
                    genelVoltLabel.Text = genelVolt.ToString();
                }
                // watt hesabı
                double genelWatt = genelVolt * Convert.ToDouble(genelAmperLabel.Text);
                if(genelWatt.ToString().Length > 5)
                {
                    genelWattLabel.Text = genelWatt.ToString().Substring(0,5);
                } else
                {
                    genelWattLabel.Text = genelWatt.ToString();
                }
                GenelHesap(ayrilmisVeriler);
                ProgressBarHesabi(genelVolt);
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.SelectTab("tabPage1");
            sarjProgress.Style = ProgressBarStyle.Continuous;

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
            // dataKonsol.Refresh();
        }
        private void baglantiKesButon_Click(object sender, EventArgs e)
        {
            port.Close();
            timer1.Enabled = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string okunan = port.ReadExisting();
            VeriOku(okunan);
            genelVolt = 0;
        }
    }
}
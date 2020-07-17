using OfficeOpenXml;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace SubuTetra2020
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
        /* Degisken tanimlamalari */
        SerialPort port = new SerialPort();
        double max, min, maxSicaklik, genelVolt;
        SaveFileDialog dosyaYolu;
        int satirCount = 1;
        ExcelPackage package;
        ExcelWorksheet worksheet;
        /* Degisken tanimlamalari*/
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
                return deger;
            }
        }
        private void GenelVoltHesabi(string [] ayrilmisVeriler)
        {
            genelVolt = 0;
            for (int i = 1; i < 40; i = i + 2)
            {
                genelVolt += Convert.ToDouble(ayrilmisVeriler[i]) / 100.0;
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
            if(Convert.ToInt32(toplamGerilim) > 60 && Convert.ToInt32(toplamGerilim) <=80)
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
        
        private void DosyaLog(string [] okunanVeri, int satirSayisi)
        {
            worksheet.Cells[satirSayisi, 1].Value = DateTime.Now.ToString();
            // pil değerlerinin excell dosyasına yazdırılması
            worksheet.Cells[satirSayisi, 2].Value = PilDegerHesabi(okunanVeri[1]);
            worksheet.Cells[satirSayisi, 3].Value = PilDegerHesabi(okunanVeri[3]);
            worksheet.Cells[satirSayisi, 4].Value = PilDegerHesabi(okunanVeri[5]);
            worksheet.Cells[satirSayisi, 5].Value = PilDegerHesabi(okunanVeri[7]);
            worksheet.Cells[satirSayisi, 6].Value = PilDegerHesabi(okunanVeri[9]);
            worksheet.Cells[satirSayisi, 7].Value = PilDegerHesabi(okunanVeri[11]);
            worksheet.Cells[satirSayisi, 8].Value = PilDegerHesabi(okunanVeri[13]);
            worksheet.Cells[satirSayisi, 9].Value = PilDegerHesabi(okunanVeri[15]);
            worksheet.Cells[satirSayisi, 10].Value = PilDegerHesabi(okunanVeri[17]);
            worksheet.Cells[satirSayisi, 11].Value = PilDegerHesabi(okunanVeri[19]);
            worksheet.Cells[satirSayisi, 12].Value = PilDegerHesabi(okunanVeri[21]);
            worksheet.Cells[satirSayisi, 13].Value = PilDegerHesabi(okunanVeri[23]);
            worksheet.Cells[satirSayisi, 14].Value = PilDegerHesabi(okunanVeri[25]);
            worksheet.Cells[satirSayisi, 15].Value = PilDegerHesabi(okunanVeri[27]);
            worksheet.Cells[satirSayisi, 16].Value = PilDegerHesabi(okunanVeri[29]);
            worksheet.Cells[satirSayisi, 17].Value = PilDegerHesabi(okunanVeri[31]);
            worksheet.Cells[satirSayisi, 18].Value = PilDegerHesabi(okunanVeri[33]);
            worksheet.Cells[satirSayisi, 19].Value = PilDegerHesabi(okunanVeri[35]);
            worksheet.Cells[satirSayisi, 20].Value = PilDegerHesabi(okunanVeri[37]);
            worksheet.Cells[satirSayisi, 21].Value = PilDegerHesabi(okunanVeri[39]);
            // sıcaklık değerlerinin excel dosyasına yazdırılması
            worksheet.Cells[satirSayisi, 22].Value = SicaklikDegerHesabi(okunanVeri[41]);
            worksheet.Cells[satirSayisi, 23].Value = SicaklikDegerHesabi(okunanVeri[43]);
            worksheet.Cells[satirSayisi, 24].Value = SicaklikDegerHesabi(okunanVeri[45]);
            worksheet.Cells[satirSayisi, 25].Value = SicaklikDegerHesabi(okunanVeri[47]);
            worksheet.Cells[satirSayisi, 26].Value = SicaklikDegerHesabi(okunanVeri[49]);
            // akım değerinin excel dosyasına yazdırılması
            worksheet.Cells[satirSayisi, 27].Value = Convert.ToDouble(okunanVeri[51]);
        }
        // Veri okuma
        private void VeriOku(string okunanVeri)
        {
            string[] ayrilmisVeriler = okunanVeri.Split('|');
            if (ayrilmisVeriler.Length == 53 && okunanVeri[okunanVeri.Length -1] == '|' && okunanVeri[0] == 'A' && okunanVeri[1] == '1')
            {
                // okunan veriyi konsola loglama
                dataKonsol.Text += DateTime.Now;
                dataKonsol.Text += " - ";
                dataKonsol.Text += okunanVeri;
                dataKonsol.Text += Environment.NewLine;
                // okunan veriyi seçilen excell dosyasına loglama
                if(logKaydiCheckBox.Checked == true)
                {
                    satirCount++;
                    DosyaLog(ayrilmisVeriler, satirCount);
                }
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
                GenelVoltHesabi(ayrilmisVeriler);
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
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                package = new ExcelPackage();
                package.Workbook.Worksheets.Add("Worksheet1");
                worksheet = package.Workbook.Worksheets.FirstOrDefault();
                ExcelSetup();
            }
        }
        private void logKaydiButon_Click(object sender, EventArgs e)
        {
            dosyaYolu = new SaveFileDialog();
            if (dosyaYolu.ShowDialog() == DialogResult.OK)
            {
                logKaydiYolTextBox.Text = dosyaYolu.FileName;
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
            baglantiKontrolLabel.Text = "Bağlantı Kapalı";
            baglantiKontrolLabel.ForeColor = Color.Red;
            port.Close();
            timer1.Enabled = false;
            if(logKaydiCheckBox.Checked == true)
            {
                Stream stream = dosyaYolu.OpenFile();
                package.SaveAs(stream);
                stream.Close();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string okunan = port.ReadExisting();
            VeriOku(okunan);
        }
        private void ExcelSetup()
        {
            worksheet.Cells[1, 1].Value = "Tarih";
            worksheet.Cells[1, 2].Value = "Pil 1";
            worksheet.Cells[1, 3].Value = "Pil 2";
            worksheet.Cells[1, 4].Value = "Pil 3";
            worksheet.Cells[1, 5].Value = "Pil 4";
            worksheet.Cells[1, 6].Value = "Pil 5";
            worksheet.Cells[1, 7].Value = "Pil 6";
            worksheet.Cells[1, 8].Value = "Pil 7";
            worksheet.Cells[1, 9].Value = "Pil 8";
            worksheet.Cells[1, 10].Value = "Pil 9";
            worksheet.Cells[1, 11].Value = "Pil 10";
            worksheet.Cells[1, 12].Value = "Pil 11";
            worksheet.Cells[1, 13].Value = "Pil 12";
            worksheet.Cells[1, 14].Value = "Pil 13";
            worksheet.Cells[1, 15].Value = "Pil 14";
            worksheet.Cells[1, 16].Value = "Pil 15";
            worksheet.Cells[1, 17].Value = "Pil 16";
            worksheet.Cells[1, 18].Value = "Pil 17";
            worksheet.Cells[1, 19].Value = "Pil 18";
            worksheet.Cells[1, 20].Value = "Pil 19";
            worksheet.Cells[1, 21].Value = "Pil 20";
            worksheet.Cells[1, 22].Value = "Sonsor 1";
            worksheet.Cells[1, 23].Value = "Sensor2";
            worksheet.Cells[1, 24].Value = "Sensor3";
            worksheet.Cells[1, 25].Value = "Sensor4";
            worksheet.Cells[1, 26].Value = "Sensor5";
            worksheet.Cells[1, 27].Value = "Amper";
        }
    }
}
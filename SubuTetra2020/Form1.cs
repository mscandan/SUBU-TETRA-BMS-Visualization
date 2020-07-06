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
        // Veri okuma
        private void VeriOku(object sender, SerialDataReceivedEventArgs e)
        {
            dataKonsol.Text += port.ReadExisting();
            dataKonsol.Text += Environment.NewLine;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            port.DataReceived += new SerialDataReceivedEventHandler(VeriOku);
        }
    }
}

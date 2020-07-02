using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace SubuTetra2020
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tabControl1.SelectTab("tabPage1");
        }

        private void PortlariListele()
        {
            string[] ports = SerialPort.GetPortNames();
            PortNamesComboBox.Items.AddRange(ports);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PortlariListele();
        }

        private void portBaglanButton_Click(object sender, EventArgs e)
        {
            if(PortNamesComboBox.SelectedIndex < 0)
            {
                baglantiKontrolLabel.ForeColor = Color.Red;
                baglantiKontrolLabel.Text = "COM Portu bulunamadı";
            } else if(baudrateTextBox.Text == "")
            {
                baglantiKontrolLabel.ForeColor = Color.Red;
                baglantiKontrolLabel.Text = "Baudrate değerini giriniz";
            } else
            {
                baglantiKontrolLabel.ForeColor = Color.Green;
                baglantiKontrolLabel.Text = "Bağlantı Başarılı";
            }
        }

        private void logKaydiButon_Click(object sender, EventArgs e)
        {
            var filePath = new OpenFileDialog();
            if(filePath.ShowDialog() == DialogResult.OK)
            {
                logKaydiYolTextBox.Text = filePath.FileName;
            }

            if(logKaydiCheckBox.Checked == true)
            {
                // log kaydı tutulacak
            } else
            {
                // log kaydı tutulmadan devam edilecek
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

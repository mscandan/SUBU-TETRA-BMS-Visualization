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
namespace SubuTetra2020
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String [] ports = SerialPort.GetPortNames();
            for(int i = 0; i < ports.Length; i++)
            {
                textBox1.Text += ports[i] + Environment.NewLine;
            }
        }
    } 
}

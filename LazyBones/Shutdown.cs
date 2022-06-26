using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyBones
{
    public partial class ShutdownForm : Form
    {
        private int counter = 30;
        
        private void LabelText()
        {
            label1.Text = $"Автоматичне вимкнення компьютера через {counter.ToString()} секунд";
        }
        
        public ShutdownForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            LabelText();
            if (counter == 0) DialogResult = DialogResult.OK;
        }

        private void Shutdown_Shown(object sender, EventArgs e)
        {
            LabelText();
            timer1.Start();
        }

        private void Shutdown_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();  
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSetting
{
    public partial class msg_box : Form
    {
        public string btn1_title
        {
            set
            {
                button1.Text = value;
            }
            get
            {
                return button1.Text;
            }
        }
        public string btn2_title
        {
            set
            {
                button2.Text = value;
            }
            get
            {
                return button2.Text;
            }
        }

        public string message
        {
            set
            {
                label1.Text = value;
            }
            get
            {
                return label1.Text;
            }
        }

        public DialogResult result;




        public msg_box()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            this.Close();
        }
    }
}

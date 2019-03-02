using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace NetSetting
{
    public partial class IPText : UserControl
    {
        public IPText()
        {
            InitializeComponent();
        }
      
        public string IPAddress_string
        {
            set
            {
                if (value.Split('.').Length > 0)
                    txt1.Text = value.Split('.')[0];
                else
                    txt1.Text = "";
                if (value.Split('.').Length > 1)
                    txt2.Text = value.Split('.')[1];
                else
                    txt2.Text = "";
                if (value.Split('.').Length > 2)
                    txt3.Text = value.Split('.')[2];
                else
                    txt3.Text = "";
                if (value.Split('.').Length > 3)
                    txt4.Text = value.Split('.')[3];
                else
                    txt4.Text = "";
            }
            get
            {
                int num1;
                int num2;
                int num3;
                int num4;
                if (!(int.TryParse(txt1.Text, out num1) && num1 <= 255 && num1 >= 0) && txt1.Text != "")
                    throw new Exception("ip is incorrect");
                if (!(int.TryParse(txt2.Text, out num2) && num2 <= 255 && num2 >= 0) && txt2.Text != "")
                    throw new Exception("ip is incorrect");
                if (!(int.TryParse(txt3.Text, out num3) && num3 <= 255 && num3 >= 0) && txt3.Text != "")
                    throw new Exception("ip is incorrect");
                if (!(int.TryParse(txt4.Text, out num4) && num4 <= 255 && num4 >= 0) && txt4.Text != "")
                    throw new Exception("ip is incorrect");
                if(txt1.Text=="" && txt2.Text == "" && txt3.Text == "" && txt4.Text == "" )
                    return "";
                return (txt1.Text + "." + txt2.Text + "." + txt3.Text + "." + txt4.Text);
            }
        }

 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txt1.Text != "")
            {
                if (txt1.Text.Substring(txt1.Text.Length - 1, 1) == "." && txt1.Text.Length > 1)
                {
                    txt1.Text = txt1.Text.Substring(0, txt1.Text.Length - 1);
                    txt2.Focus();
                }
                else if (txt1.Text.Substring(txt1.Text.Length - 1, 1) == "." && txt1.Text != "")
                {
                    txt1.Text = txt1.Text.Substring(0, txt1.Text.Length - 1);
                }
                if (txt1.Text.Length == 3)
                    txt2.Focus();
            }
        }

        private void txt2_TextChanged(object sender, EventArgs e)
        {
            if (txt2.Text != "")
            {
                if (txt2.Text.Substring(txt2.Text.Length - 1, 1) == "." && txt2.Text.Length > 1)
                {
                    txt2.Text = txt2.Text.Substring(0, txt2.Text.Length - 1);
                    txt3.Focus();
                }
                else if (txt2.Text.Substring(txt2.Text.Length - 1, 1) == "." && txt2.Text != "")
                {
                    txt2.Text = txt2.Text.Substring(0, txt2.Text.Length - 1);
                }
                if (txt2.Text.Length == 3)
                    txt3.Focus();
            }
        }

        private void txt3_TextChanged(object sender, EventArgs e)
        {
            if (txt3.Text != "")
            {
                if (txt3.Text.Substring(txt3.Text.Length - 1, 1) == "." && txt3.Text.Length > 1)
                {
                    txt3.Text = txt3.Text.Substring(0, txt3.Text.Length - 1);
                    txt4.Focus();
                }
                else if (txt3.Text.Substring(txt3.Text.Length - 1, 1) == "." && txt3.Text != "")
                {
                    txt3.Text = txt3.Text.Substring(0, txt3.Text.Length - 1);
                }
                if (txt3.Text.Length == 3)
                    txt4.Focus();
            }
        }

        private void txt4_TextChanged(object sender, EventArgs e)
        {
            if (txt4.Text != "")
            {
                if (txt4.Text.Substring(txt4.Text.Length - 1, 1) == ".")
                {
                    txt4.Text = txt4.Text.Substring(0, txt4.Text.Length - 1);
                }
               if (txt4.Text != "" && txt4.Text.Substring(0, 1) == ".")
                {
                    txt4.Text = txt4.Text.Substring(1);
                }
            }
        }

        private void IPText_EnabledChanged(object sender, EventArgs e)
        {
            if(this.Enabled)
            {
                panel1.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                panel1.BackColor = System.Drawing.SystemColors.Control;
            }
        }
    }
}

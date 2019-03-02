namespace NetSetting
{
    partial class Form_netsh
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnl_info = new System.Windows.Forms.Panel();
            this.lbl_showdes = new System.Windows.Forms.Label();
            this.lbl_showip2 = new System.Windows.Forms.Label();
            this.lbl_showsubnet2 = new System.Windows.Forms.Label();
            this.lbl_showdes2 = new System.Windows.Forms.Label();
            this.lbl_showgetway2 = new System.Windows.Forms.Label();
            this.lbl_showname2 = new System.Windows.Forms.Label();
            this.lbl_dnspre2 = new System.Windows.Forms.Label();
            this.lbl_Mac = new System.Windows.Forms.Label();
            this.lbl_showaltdns2 = new System.Windows.Forms.Label();
            this.lbl_showname = new System.Windows.Forms.Label();
            this.lbl_showip = new System.Windows.Forms.Label();
            this.lbl_showsubnet = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_showaltdns = new System.Windows.Forms.Label();
            this.lbl_showgetway = new System.Windows.Forms.Label();
            this.lbl_dnspre = new System.Windows.Forms.Label();
            this.pnl_edit = new System.Windows.Forms.Panel();
            this.lbl_IP = new System.Windows.Forms.Label();
            this.pnl_txtedit = new System.Windows.Forms.Panel();
            this.rdo_manual = new System.Windows.Forms.RadioButton();
            this.rdo_dhcp = new System.Windows.Forms.RadioButton();
            this.lbl_getway = new System.Windows.Forms.Label();
            this.lbl_dns2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_subnet = new System.Windows.Forms.Label();
            this.lbl_dns1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_interfaces = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lbl_ipIE = new System.Windows.Forms.Label();
            this.lbl_PORTIE = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_IEstatus = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_portp2 = new System.Windows.Forms.TextBox();
            this.txt_portp1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ipp2 = new System.Windows.Forms.TextBox();
            this.txt_ipp1 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ipc_dns2 = new NetSetting.IPText();
            this.ipc_dns1 = new NetSetting.IPText();
            this.ipc_getway = new NetSetting.IPText();
            this.ipc_subnet = new NetSetting.IPText();
            this.ipc_ip = new NetSetting.IPText();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnl_info.SuspendLayout();
            this.pnl_edit.SuspendLayout();
            this.pnl_txtedit.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1475, 428);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmb_interfaces);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1467, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "شبکه";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(21, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "refresh";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pnl_info);
            this.groupBox1.Controls.Add(this.pnl_edit);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.groupBox1.Location = new System.Drawing.Point(8, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.groupBox1.Size = new System.Drawing.Size(1451, 356);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "تنظیمات";
            // 
            // pnl_info
            // 
            this.pnl_info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_info.Controls.Add(this.lbl_showdes);
            this.pnl_info.Controls.Add(this.lbl_showip2);
            this.pnl_info.Controls.Add(this.lbl_showsubnet2);
            this.pnl_info.Controls.Add(this.lbl_showdes2);
            this.pnl_info.Controls.Add(this.lbl_showgetway2);
            this.pnl_info.Controls.Add(this.lbl_showname2);
            this.pnl_info.Controls.Add(this.lbl_dnspre2);
            this.pnl_info.Controls.Add(this.lbl_Mac);
            this.pnl_info.Controls.Add(this.lbl_showaltdns2);
            this.pnl_info.Controls.Add(this.lbl_showname);
            this.pnl_info.Controls.Add(this.lbl_showip);
            this.pnl_info.Controls.Add(this.lbl_showsubnet);
            this.pnl_info.Controls.Add(this.label2);
            this.pnl_info.Controls.Add(this.lbl_showaltdns);
            this.pnl_info.Controls.Add(this.lbl_showgetway);
            this.pnl_info.Controls.Add(this.lbl_dnspre);
            this.pnl_info.Location = new System.Drawing.Point(1055, 33);
            this.pnl_info.Name = "pnl_info";
            this.pnl_info.Size = new System.Drawing.Size(385, 313);
            this.pnl_info.TabIndex = 13;
            // 
            // lbl_showdes
            // 
            this.lbl_showdes.AutoSize = true;
            this.lbl_showdes.Location = new System.Drawing.Point(5, 10);
            this.lbl_showdes.Name = "lbl_showdes";
            this.lbl_showdes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showdes.Size = new System.Drawing.Size(120, 25);
            this.lbl_showdes.TabIndex = 11;
            this.lbl_showdes.Text = "Description :";
            // 
            // lbl_showip2
            // 
            this.lbl_showip2.AutoSize = true;
            this.lbl_showip2.Location = new System.Drawing.Point(156, 70);
            this.lbl_showip2.Name = "lbl_showip2";
            this.lbl_showip2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showip2.Size = new System.Drawing.Size(0, 25);
            this.lbl_showip2.TabIndex = 9;
            // 
            // lbl_showsubnet2
            // 
            this.lbl_showsubnet2.AutoSize = true;
            this.lbl_showsubnet2.Location = new System.Drawing.Point(156, 99);
            this.lbl_showsubnet2.Name = "lbl_showsubnet2";
            this.lbl_showsubnet2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showsubnet2.Size = new System.Drawing.Size(0, 25);
            this.lbl_showsubnet2.TabIndex = 9;
            // 
            // lbl_showdes2
            // 
            this.lbl_showdes2.AutoSize = true;
            this.lbl_showdes2.Location = new System.Drawing.Point(156, 12);
            this.lbl_showdes2.Name = "lbl_showdes2";
            this.lbl_showdes2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showdes2.Size = new System.Drawing.Size(0, 25);
            this.lbl_showdes2.TabIndex = 10;
            // 
            // lbl_showgetway2
            // 
            this.lbl_showgetway2.AutoSize = true;
            this.lbl_showgetway2.Location = new System.Drawing.Point(156, 129);
            this.lbl_showgetway2.Name = "lbl_showgetway2";
            this.lbl_showgetway2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showgetway2.Size = new System.Drawing.Size(0, 25);
            this.lbl_showgetway2.TabIndex = 9;
            // 
            // lbl_showname2
            // 
            this.lbl_showname2.AutoSize = true;
            this.lbl_showname2.Location = new System.Drawing.Point(156, 40);
            this.lbl_showname2.Name = "lbl_showname2";
            this.lbl_showname2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showname2.Size = new System.Drawing.Size(0, 25);
            this.lbl_showname2.TabIndex = 10;
            // 
            // lbl_dnspre2
            // 
            this.lbl_dnspre2.AutoSize = true;
            this.lbl_dnspre2.Location = new System.Drawing.Point(156, 158);
            this.lbl_dnspre2.Name = "lbl_dnspre2";
            this.lbl_dnspre2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_dnspre2.Size = new System.Drawing.Size(0, 25);
            this.lbl_dnspre2.TabIndex = 9;
            // 
            // lbl_Mac
            // 
            this.lbl_Mac.AutoSize = true;
            this.lbl_Mac.Location = new System.Drawing.Point(156, 217);
            this.lbl_Mac.Name = "lbl_Mac";
            this.lbl_Mac.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Mac.Size = new System.Drawing.Size(0, 25);
            this.lbl_Mac.TabIndex = 9;
            // 
            // lbl_showaltdns2
            // 
            this.lbl_showaltdns2.AutoSize = true;
            this.lbl_showaltdns2.Location = new System.Drawing.Point(156, 188);
            this.lbl_showaltdns2.Name = "lbl_showaltdns2";
            this.lbl_showaltdns2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showaltdns2.Size = new System.Drawing.Size(0, 25);
            this.lbl_showaltdns2.TabIndex = 9;
            // 
            // lbl_showname
            // 
            this.lbl_showname.AutoSize = true;
            this.lbl_showname.Location = new System.Drawing.Point(5, 40);
            this.lbl_showname.Name = "lbl_showname";
            this.lbl_showname.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showname.Size = new System.Drawing.Size(75, 25);
            this.lbl_showname.TabIndex = 11;
            this.lbl_showname.Text = "Name :";
            // 
            // lbl_showip
            // 
            this.lbl_showip.AutoSize = true;
            this.lbl_showip.Location = new System.Drawing.Point(5, 70);
            this.lbl_showip.Name = "lbl_showip";
            this.lbl_showip.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showip.Size = new System.Drawing.Size(41, 25);
            this.lbl_showip.TabIndex = 9;
            this.lbl_showip.Text = "IP :";
            // 
            // lbl_showsubnet
            // 
            this.lbl_showsubnet.AutoSize = true;
            this.lbl_showsubnet.Location = new System.Drawing.Point(5, 99);
            this.lbl_showsubnet.Name = "lbl_showsubnet";
            this.lbl_showsubnet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showsubnet.Size = new System.Drawing.Size(81, 25);
            this.lbl_showsubnet.TabIndex = 9;
            this.lbl_showsubnet.Text = "Subnet:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 217);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(147, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "MAC Address :";
            // 
            // lbl_showaltdns
            // 
            this.lbl_showaltdns.AutoSize = true;
            this.lbl_showaltdns.Location = new System.Drawing.Point(4, 188);
            this.lbl_showaltdns.Name = "lbl_showaltdns";
            this.lbl_showaltdns.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showaltdns.Size = new System.Drawing.Size(148, 25);
            this.lbl_showaltdns.TabIndex = 9;
            this.lbl_showaltdns.Text = "Alternate DNS :";
            // 
            // lbl_showgetway
            // 
            this.lbl_showgetway.AutoSize = true;
            this.lbl_showgetway.Location = new System.Drawing.Point(5, 129);
            this.lbl_showgetway.Name = "lbl_showgetway";
            this.lbl_showgetway.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_showgetway.Size = new System.Drawing.Size(80, 25);
            this.lbl_showgetway.TabIndex = 9;
            this.lbl_showgetway.Text = "getway:";
            // 
            // lbl_dnspre
            // 
            this.lbl_dnspre.AutoSize = true;
            this.lbl_dnspre.Location = new System.Drawing.Point(5, 158);
            this.lbl_dnspre.Name = "lbl_dnspre";
            this.lbl_dnspre.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_dnspre.Size = new System.Drawing.Size(148, 25);
            this.lbl_dnspre.TabIndex = 9;
            this.lbl_dnspre.Text = "preferred DNS :";
            // 
            // pnl_edit
            // 
            this.pnl_edit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_edit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_edit.Controls.Add(this.lbl_IP);
            this.pnl_edit.Controls.Add(this.pnl_txtedit);
            this.pnl_edit.Controls.Add(this.rdo_manual);
            this.pnl_edit.Controls.Add(this.rdo_dhcp);
            this.pnl_edit.Controls.Add(this.lbl_getway);
            this.pnl_edit.Controls.Add(this.lbl_dns2);
            this.pnl_edit.Controls.Add(this.button2);
            this.pnl_edit.Controls.Add(this.button1);
            this.pnl_edit.Controls.Add(this.lbl_subnet);
            this.pnl_edit.Controls.Add(this.lbl_dns1);
            this.pnl_edit.Enabled = false;
            this.pnl_edit.Location = new System.Drawing.Point(13, 33);
            this.pnl_edit.Name = "pnl_edit";
            this.pnl_edit.Size = new System.Drawing.Size(970, 313);
            this.pnl_edit.TabIndex = 0;
            this.pnl_edit.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_edit_Paint);
            // 
            // lbl_IP
            // 
            this.lbl_IP.AutoSize = true;
            this.lbl_IP.Location = new System.Drawing.Point(13, 49);
            this.lbl_IP.Name = "lbl_IP";
            this.lbl_IP.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_IP.Size = new System.Drawing.Size(114, 25);
            this.lbl_IP.TabIndex = 3;
            this.lbl_IP.Text = "IP Address:";
            // 
            // pnl_txtedit
            // 
            this.pnl_txtedit.Controls.Add(this.ipc_dns2);
            this.pnl_txtedit.Controls.Add(this.ipc_ip);
            this.pnl_txtedit.Controls.Add(this.ipc_dns1);
            this.pnl_txtedit.Controls.Add(this.ipc_subnet);
            this.pnl_txtedit.Controls.Add(this.ipc_getway);
            this.pnl_txtedit.Location = new System.Drawing.Point(235, 40);
            this.pnl_txtedit.Name = "pnl_txtedit";
            this.pnl_txtedit.Size = new System.Drawing.Size(281, 189);
            this.pnl_txtedit.TabIndex = 2;
            // 
            // rdo_manual
            // 
            this.rdo_manual.AutoSize = true;
            this.rdo_manual.Location = new System.Drawing.Point(183, 4);
            this.rdo_manual.Name = "rdo_manual";
            this.rdo_manual.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rdo_manual.Size = new System.Drawing.Size(98, 29);
            this.rdo_manual.TabIndex = 1;
            this.rdo_manual.TabStop = true;
            this.rdo_manual.Text = "Manual";
            this.rdo_manual.UseVisualStyleBackColor = true;
            this.rdo_manual.CheckedChanged += new System.EventHandler(this.rdo_manual_CheckedChanged);
            // 
            // rdo_dhcp
            // 
            this.rdo_dhcp.AutoSize = true;
            this.rdo_dhcp.Location = new System.Drawing.Point(19, 4);
            this.rdo_dhcp.Name = "rdo_dhcp";
            this.rdo_dhcp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rdo_dhcp.Size = new System.Drawing.Size(89, 29);
            this.rdo_dhcp.TabIndex = 0;
            this.rdo_dhcp.TabStop = true;
            this.rdo_dhcp.Text = "DHCP";
            this.rdo_dhcp.UseVisualStyleBackColor = true;
            this.rdo_dhcp.CheckedChanged += new System.EventHandler(this.rdo_dhcp_CheckedChanged);
            // 
            // lbl_getway
            // 
            this.lbl_getway.AutoSize = true;
            this.lbl_getway.Location = new System.Drawing.Point(13, 125);
            this.lbl_getway.Name = "lbl_getway";
            this.lbl_getway.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_getway.Size = new System.Drawing.Size(146, 25);
            this.lbl_getway.TabIndex = 3;
            this.lbl_getway.Text = "Default getway:";
            // 
            // lbl_dns2
            // 
            this.lbl_dns2.AutoSize = true;
            this.lbl_dns2.Location = new System.Drawing.Point(13, 195);
            this.lbl_dns2.Name = "lbl_dns2";
            this.lbl_dns2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_dns2.Size = new System.Drawing.Size(202, 25);
            this.lbl_dns2.TabIndex = 3;
            this.lbl_dns2.Text = "Alternate DNS server:";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button2.Location = new System.Drawing.Point(235, 246);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 43);
            this.button2.TabIndex = 4;
            this.button2.Text = "تنظیمات پیشفرض";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button1.Location = new System.Drawing.Point(82, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 43);
            this.button1.TabIndex = 3;
            this.button1.Text = "تنظیم";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_subnet
            // 
            this.lbl_subnet.AutoSize = true;
            this.lbl_subnet.Location = new System.Drawing.Point(13, 88);
            this.lbl_subnet.Name = "lbl_subnet";
            this.lbl_subnet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_subnet.Size = new System.Drawing.Size(133, 25);
            this.lbl_subnet.TabIndex = 3;
            this.lbl_subnet.Text = "Subnet mask:";
            // 
            // lbl_dns1
            // 
            this.lbl_dns1.AutoSize = true;
            this.lbl_dns1.Location = new System.Drawing.Point(13, 160);
            this.lbl_dns1.Name = "lbl_dns1";
            this.lbl_dns1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_dns1.Size = new System.Drawing.Size(204, 25);
            this.lbl_dns1.TabIndex = 3;
            this.lbl_dns1.Text = "Preferred DNS server:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1388, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "رابط شبکه :";
            // 
            // cmb_interfaces
            // 
            this.cmb_interfaces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmb_interfaces.FormattingEnabled = true;
            this.cmb_interfaces.Location = new System.Drawing.Point(104, 11);
            this.cmb_interfaces.Name = "cmb_interfaces";
            this.cmb_interfaces.Size = new System.Drawing.Size(1278, 24);
            this.cmb_interfaces.TabIndex = 0;
            this.cmb_interfaces.SelectedIndexChanged += new System.EventHandler(this.cmb_interfaces_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lbl_ipIE);
            this.tabPage2.Controls.Add(this.lbl_PORTIE);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.lbl_IEstatus);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.button7);
            this.tabPage2.Controls.Add(this.button10);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txt_portp2);
            this.tabPage2.Controls.Add(this.txt_portp1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txt_ipp2);
            this.tabPage2.Controls.Add(this.txt_ipp1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1202, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "پراکسی";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lbl_ipIE
            // 
            this.lbl_ipIE.AutoSize = true;
            this.lbl_ipIE.Location = new System.Drawing.Point(735, 300);
            this.lbl_ipIE.Name = "lbl_ipIE";
            this.lbl_ipIE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_ipIE.Size = new System.Drawing.Size(54, 17);
            this.lbl_ipIE.TabIndex = 14;
            this.lbl_ipIE.Text = "label10";
            // 
            // lbl_PORTIE
            // 
            this.lbl_PORTIE.AutoSize = true;
            this.lbl_PORTIE.Location = new System.Drawing.Point(735, 322);
            this.lbl_PORTIE.Name = "lbl_PORTIE";
            this.lbl_PORTIE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_PORTIE.Size = new System.Drawing.Size(54, 17);
            this.lbl_PORTIE.TabIndex = 14;
            this.lbl_PORTIE.Text = "label10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(609, 322);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(97, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "Port IE Proxy :";
            // 
            // lbl_IEstatus
            // 
            this.lbl_IEstatus.AutoSize = true;
            this.lbl_IEstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lbl_IEstatus.Location = new System.Drawing.Point(582, 265);
            this.lbl_IEstatus.Name = "lbl_IEstatus";
            this.lbl_IEstatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_IEstatus.Size = new System.Drawing.Size(193, 26);
            this.lbl_IEstatus.TabIndex = 14;
            this.lbl_IEstatus.Text = "Address IE Proxy :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(609, 300);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(123, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "Address IE Proxy :";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.Control;
            this.button7.Location = new System.Drawing.Point(983, 98);
            this.button7.Name = "button7";
            this.button7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button7.Size = new System.Drawing.Size(151, 44);
            this.button7.TabIndex = 7;
            this.button7.Text = "FireFox";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.SystemColors.Control;
            this.button10.Location = new System.Drawing.Point(230, 308);
            this.button10.Name = "button10";
            this.button10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button10.Size = new System.Drawing.Size(167, 44);
            this.button10.TabIndex = 13;
            this.button10.Text = "Save";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.SystemColors.Control;
            this.button6.Location = new System.Drawing.Point(57, 308);
            this.button6.Name = "button6";
            this.button6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button6.Size = new System.Drawing.Size(167, 44);
            this.button6.TabIndex = 12;
            this.button6.Text = "No Proxy For IE && ...";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Control;
            this.button3.Location = new System.Drawing.Point(57, 96);
            this.button3.Name = "button3";
            this.button3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button3.Size = new System.Drawing.Size(151, 44);
            this.button3.TabIndex = 2;
            this.button3.Text = "IE && ...";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(876, 60);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(38, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Port:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(114, 60);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(38, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Port:";
            // 
            // txt_portp2
            // 
            this.txt_portp2.Location = new System.Drawing.Point(917, 57);
            this.txt_portp2.Name = "txt_portp2";
            this.txt_portp2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_portp2.Size = new System.Drawing.Size(71, 22);
            this.txt_portp2.TabIndex = 5;
            // 
            // txt_portp1
            // 
            this.txt_portp1.Location = new System.Drawing.Point(155, 57);
            this.txt_portp1.Name = "txt_portp1";
            this.txt_portp1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_portp1.Size = new System.Drawing.Size(71, 22);
            this.txt_portp1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(811, 32);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(103, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Proxy Address:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 32);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(103, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Proxy Address:";
            // 
            // txt_ipp2
            // 
            this.txt_ipp2.Location = new System.Drawing.Point(917, 29);
            this.txt_ipp2.Name = "txt_ipp2";
            this.txt_ipp2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_ipp2.Size = new System.Drawing.Size(213, 22);
            this.txt_ipp2.TabIndex = 4;
            // 
            // txt_ipp1
            // 
            this.txt_ipp1.Location = new System.Drawing.Point(155, 29);
            this.txt_ipp1.Name = "txt_ipp1";
            this.txt_ipp1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_ipp1.Size = new System.Drawing.Size(213, 22);
            this.txt_ipp1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1202, 402);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "درباره ما";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1475, 428);
            this.panel1.TabIndex = 5;
            // 
            // ipc_dns2
            // 
            this.ipc_dns2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ipc_dns2.IPAddress_string = "0.0.0.0";
            this.ipc_dns2.Location = new System.Drawing.Point(4, 149);
            this.ipc_dns2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ipc_dns2.Name = "ipc_dns2";
            this.ipc_dns2.Size = new System.Drawing.Size(270, 34);
            this.ipc_dns2.TabIndex = 9;
            // 
            // ipc_dns1
            // 
            this.ipc_dns1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ipc_dns1.IPAddress_string = "0.0.0.0";
            this.ipc_dns1.Location = new System.Drawing.Point(4, 113);
            this.ipc_dns1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ipc_dns1.Name = "ipc_dns1";
            this.ipc_dns1.Size = new System.Drawing.Size(270, 34);
            this.ipc_dns1.TabIndex = 8;
            // 
            // ipc_getway
            // 
            this.ipc_getway.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ipc_getway.IPAddress_string = "0.0.0.0";
            this.ipc_getway.Location = new System.Drawing.Point(4, 77);
            this.ipc_getway.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ipc_getway.Name = "ipc_getway";
            this.ipc_getway.Size = new System.Drawing.Size(270, 34);
            this.ipc_getway.TabIndex = 7;
            // 
            // ipc_subnet
            // 
            this.ipc_subnet.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ipc_subnet.IPAddress_string = "0.0.0.0";
            this.ipc_subnet.Location = new System.Drawing.Point(4, 41);
            this.ipc_subnet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ipc_subnet.Name = "ipc_subnet";
            this.ipc_subnet.Size = new System.Drawing.Size(270, 34);
            this.ipc_subnet.TabIndex = 6;
            // 
            // ipc_ip
            // 
            this.ipc_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ipc_ip.IPAddress_string = "0.0.0.0";
            this.ipc_ip.Location = new System.Drawing.Point(4, 5);
            this.ipc_ip.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ipc_ip.Name = "ipc_ip";
            this.ipc_ip.Size = new System.Drawing.Size(270, 34);
            this.ipc_ip.TabIndex = 5;
            // 
            // Form_netsh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1475, 428);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "Form_netsh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "شبکه";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.pnl_info.ResumeLayout(false);
            this.pnl_info.PerformLayout();
            this.pnl_edit.ResumeLayout(false);
            this.pnl_edit.PerformLayout();
            this.pnl_txtedit.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel pnl_info;
        private System.Windows.Forms.Label lbl_showdes;
        private System.Windows.Forms.Label lbl_showip2;
        private System.Windows.Forms.Label lbl_showsubnet2;
        private System.Windows.Forms.Label lbl_showdes2;
        private System.Windows.Forms.Label lbl_showgetway2;
        private System.Windows.Forms.Label lbl_showname2;
        private System.Windows.Forms.Label lbl_dnspre2;
        private System.Windows.Forms.Label lbl_Mac;
        private System.Windows.Forms.Label lbl_showaltdns2;
        private System.Windows.Forms.Label lbl_showname;
        private System.Windows.Forms.Label lbl_showip;
        private System.Windows.Forms.Label lbl_showsubnet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_showaltdns;
        private System.Windows.Forms.Label lbl_showgetway;
        private System.Windows.Forms.Label lbl_dnspre;
        private System.Windows.Forms.Panel pnl_edit;
        private System.Windows.Forms.Panel pnl_txtedit;
        private System.Windows.Forms.Label lbl_IP;
        private System.Windows.Forms.Label lbl_getway;
        private System.Windows.Forms.Label lbl_dns2;
        private System.Windows.Forms.Label lbl_subnet;
        private System.Windows.Forms.Label lbl_dns1;
        private System.Windows.Forms.RadioButton rdo_manual;
        private System.Windows.Forms.RadioButton rdo_dhcp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_interfaces;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_portp1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_ipp1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_portp2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ipp2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label lbl_ipIE;
        private System.Windows.Forms.Label lbl_PORTIE;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_IEstatus;
        private System.Windows.Forms.Button button4;
        private IPText ipc_dns2;
        private IPText ipc_ip;
        private IPText ipc_dns1;
        private IPText ipc_subnet;
        private IPText ipc_getway;
    }
}


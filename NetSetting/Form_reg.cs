using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System.Net;

namespace NetSetting
{
    public partial class Form_reg : Form
    {
        public Form_reg()
        {
            InitializeComponent();
            this.FormClosing += Formnew_FormClosing;
            updatelistinterface();

            upproxyie();
            if (System.IO.File.Exists(System.Environment.CurrentDirectory + @"\info.txt"))
            {
                string[] lines = System.IO.File.ReadAllLines(System.Environment.CurrentDirectory + @"\info.txt");
                if (lines.Length > 0)
                    txt_ipp1.Text = lines[0];
                if (lines.Length > 1)
                    txt_ipp2.Text = lines[1];
                if (lines.Length > 2)
                    txt_portp1.Text = lines[2];
                if (lines.Length > 3)
                    txt_portp2.Text = lines[3];
            }
        }

        private void Formnew_FormClosing(object sender, FormClosingEventArgs e)
        {
            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();
            if (RunningProcessPaths.Contains("firefox.exe"))
            {
                DialogResult dr = MessageBox.Show("مایل به بستن فایرفاکس هستید", "فایرفاکس", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3,
                    MessageBoxOptions.RightAlign);
                if (dr == DialogResult.Yes)
                {
                    KillApplication("firefox");
                    string[] lines = { txt_ipp1.Text, txt_ipp2.Text, txt_portp1.Text, txt_portp2.Text };
                    System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + @"\info.txt", lines);
                    disableproxyviaregistry();
                    upproxyie();
                }
                else if (dr == DialogResult.No)
                {
                    string[] lines = { txt_ipp1.Text, txt_ipp2.Text, txt_portp1.Text, txt_portp2.Text };
                    System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + @"\info.txt", lines);
                    disableproxyviaregistry();
                    upproxyie();
                }
                else
                {
                    e.Cancel = true;
                }

            }
            else
            {
                string[] lines = { txt_ipp1.Text, txt_ipp2.Text, txt_portp1.Text, txt_portp2.Text };
                System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + @"\info.txt", lines);
                disableproxyviaregistry();
                upproxyie();
            }
        }




        public void upproxyie()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            lbl_IEstatus.Text = registry.GetValue("ProxyEnable").ToString() == "0" ? "Proxy Disable" : "Proxy Enable";
            lbl_ipIE.Text = registry.GetValue("ProxyServer") != null ? registry.GetValue("ProxyServer").ToString().Split(':')[0] : "";
            lbl_PORTIE.Text = registry.GetValue("ProxyServer") != null ? registry.GetValue("ProxyServer").ToString().Split(':')[1] : "";
        }



        public class tcpconfig_reg
        {
            public string name = "";
            public string des = "";
            public string NetConnectionID = "";

            public string guid = "";

            public string mac = "";

            public bool EnableDNS;

            public string IPAddress = "";
            public string DefaultGateway = "";
            public bool EnableDHCP;
            public string SubnetMask = "";
            public string[] NameServer;
            public string dns1 = "";
            public string dns2 = "";

            public string Dhcpdns1 = "";
            public string Dhcpdns2 = "";
            public string[] DhcpNameServer;
            public string DhcpIPAddress = "";
            public string DhcpServer = "";
            public string DhcpSubnetMask = "";
            public string DhcpDefaultGateway = "";
        }

        public List<tcpconfig_reg> interfaces_regwmi;

        public void updatelistinterfaces_regwmi()
        {
            interfaces_regwmi = new List<tcpconfig_reg>();

            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                tcpconfig_reg item = new tcpconfig_reg();


                if (networkAdapter["NetConnectionID"] != null)
                {
                    item.NetConnectionID = networkAdapter["NetConnectionID"].ToString();
                }
                else
                {
                    continue;
                }

                item.name = networkAdapter["Name"].ToString();
                item.des = networkAdapter["Description"].ToString();
                item.guid = networkAdapter["GUID"].ToString();

                string[] IPAddress = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "IPAddress");
                if (IPAddress != null && IPAddress.Length > 0)
                    item.IPAddress = IPAddress[0];
                string[] EnableDHCP = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "EnableDHCP");
                if (EnableDHCP != null && EnableDHCP.Length > 0)
                    item.EnableDHCP = EnableDHCP[0] == "1" ? true : false;
                string[] DefaultGateway = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DefaultGateway");
                if (DefaultGateway != null && DefaultGateway.Length > 0)
                    item.DefaultGateway = DefaultGateway[0];

                string[] SubnetMask = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "SubnetMask");
                if (SubnetMask != null && SubnetMask.Length > 0)
                    item.SubnetMask = SubnetMask[0];

                string[] NameServer = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "NameServer");
                if (NameServer != null && NameServer.Length > 0)
                {
                    item.NameServer = NameServer;
                    item.dns1 = NameServer[0].Split(',')[0];
                    if (NameServer[0].Split(',').Length > 1)
                        item.dns2 = NameServer[0].Split(',')[1];
                }


                string[] DhcpIPAddress = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpIPAddress");
                if (DhcpIPAddress != null && DhcpIPAddress.Length > 0)
                    item.DhcpIPAddress = DhcpIPAddress[0];

                string[] DhcpServer = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpServer");
                if (DhcpServer != null && DhcpServer.Length > 0)
                    item.DhcpServer = DhcpServer[0];

                string[] DhcpSubnetMask = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpSubnetMask");
                if (DhcpSubnetMask != null && DhcpSubnetMask.Length > 0)
                    item.DhcpSubnetMask = DhcpSubnetMask[0];

                string[] DhcpDefaultGateway = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpDefaultGateway");
                if (DhcpDefaultGateway != null && DhcpDefaultGateway.Length > 0)
                    item.DhcpDefaultGateway = DhcpDefaultGateway[0];

                string[] DhcpNameServer = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpNameServer");
                if (DhcpNameServer != null && DhcpNameServer.Length > 0)
                {
                    item.DhcpNameServer = DhcpNameServer;
                    item.Dhcpdns1 = DhcpNameServer[0];
                    if (DhcpNameServer.Length > 1)
                        item.Dhcpdns2 = DhcpNameServer[1];
                }


                interfaces_regwmi.Add(item);

            }
        }

        public void updatelistinterface()
        {
            cmb_interfaces.Items.Clear();
            disabledit();
            updatelistinterfaces_regwmi();

            foreach (tcpconfig_reg item in interfaces_regwmi)
            {
                cmb_interfaces.Items.Add(item.NetConnectionID);
            }

        }



        public object getreg(string subkey, string key)
        {
            try
            {
                using (RegistryKey keys = Registry.LocalMachine.OpenSubKey(subkey, true))
                {
                    if (keys != null)
                    {
                        Object o = keys.GetValue(key, "");
                        if (o != null)
                        {
                            if (o.GetType() == typeof(string[]))
                                return o;
                            else if (o.GetType() == typeof(string) && o.ToString() == "")
                                return null;
                            else
                                return new string[] { o.ToString() };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public bool setreg(string subkey, string key, string value)
        {
            try
            {
                using (RegistryKey keys = Registry.LocalMachine.OpenSubKey(subkey, true))
                {
                    if (keys != null)
                    {
                        keys.SetValue(key, value);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void setip_reg(string guid, string gateway, string subnetMask, string address)
        {
            setreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + guid, "IPAddress", address);
            setreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + guid, "SubnetMask", subnetMask);
            setreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + guid, "DefaultGateway", gateway);
        }
        public void setdhcp_reg(string guid, bool enable)
        {
            setreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + guid, "EnableDHCP", enable ? "1" : "0");

            settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }
        public void setdns_reg(string guid, string dns1, string dns2)
        {
            setreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + guid, "NameServer", dns1 + "," + dns2);
        }


        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        static bool settingsReturn, refreshReturn;

        public void setproxyviaregistry(string YOURPROXY)
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 1);
            registry.SetValue("ProxyServer", YOURPROXY);

            settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        public void disableproxyviaregistry()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            registry.SetValue("ProxyEnable", 0);
            registry.SetValue("ProxyServer", "");

            settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
        }

        private void cmb_interfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_interfaces.SelectedIndex >= 0)
            {

                tcpconfig_reg item_regwmi = interfaces_regwmi.Find(a => a.NetConnectionID == cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());

                rdo_dhcp.Checked = item_regwmi.EnableDHCP;
                if (rdo_dhcp.Checked)
                {
                    lbl_dnspre2.Text = item_regwmi.Dhcpdns1;
                    lbl_showaltdns2.Text = item_regwmi.Dhcpdns2;
                    lbl_showdes2.Text = item_regwmi.des;
                    lbl_showgetway2.Text = item_regwmi.DhcpDefaultGateway;
                    lbl_showsubnet2.Text = item_regwmi.DhcpSubnetMask;
                    lbl_showname2.Text = item_regwmi.NetConnectionID;
                    lbl_showip2.Text = item_regwmi.DhcpIPAddress;
                    lbl_Mac.Text = item_regwmi.mac;
                }
                else
                {
                    lbl_dnspre2.Text = item_regwmi.dns1;
                    lbl_showaltdns2.Text = item_regwmi.dns2;
                    lbl_showdes2.Text = item_regwmi.des;
                    lbl_showgetway2.Text = item_regwmi.DefaultGateway;
                    lbl_showsubnet2.Text = item_regwmi.SubnetMask;
                    lbl_showname2.Text = item_regwmi.NetConnectionID;
                    lbl_showip2.Text = item_regwmi.IPAddress;
                    lbl_Mac.Text = item_regwmi.mac;
                }


                rdo_manual.Checked = !rdo_dhcp.Checked;

                pnl_edit.Enabled = true;

                running = true;
                disabledit();
                if (rdo_manual.Checked)
                {
                    ipc_dns1.IPAddress_string = lbl_dnspre2.Text;
                    ipc_dns2.IPAddress_string = lbl_showaltdns2.Text;
                    ipc_getway.IPAddress_string = lbl_showgetway2.Text;
                    ipc_ip.IPAddress_string = lbl_showip2.Text;
                    ipc_subnet.IPAddress_string = lbl_showsubnet2.Text;
                    enableedit();
                }
                running = false;
            }
        }


        public void SettingUpAProxyUsingFirefoxDriver(string proxystr)
        {
            var firefoxProfile = new FirefoxProfile();
            var proxy = new Proxy();
            try
            {
                string[] dirs = System.IO.Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox\Profiles");
                string profile_dir = "";
                foreach (string dir in dirs)
                {
                    if (System.IO.File.Exists(dir + @"\prefs.js"))
                    {
                        profile_dir = dir;
                        break;
                    }
                }

                if (profile_dir != "")
                    firefoxProfile = new FirefoxProfile(profile_dir);

                proxy.HttpProxy = proxystr;
                proxy.FtpProxy = proxystr;
                proxy.SslProxy = proxystr;
                proxy.SocksProxy = proxystr;

                firefoxProfile.SetProxyPreferences(proxy);
                Application.UseWaitCursor = true;
                var Driver = new FirefoxDriver(firefoxProfile);
                //  Application.UseWaitCursor = false;

                //  Driver.Navigate().GoToUrl("http/www.google.com");
            }
            catch (Exception EX)
            {
                try
                {
                    if (System.IO.Directory.Exists(Environment.CurrentDirectory + "\firefoxprofile"))
                    {
                        firefoxProfile = new FirefoxProfile(Environment.CurrentDirectory + "\firefoxprofile");
                        proxy.HttpProxy = proxystr;
                        proxy.FtpProxy = proxystr;
                        proxy.SslProxy = proxystr;
                        proxy.SocksProxy = proxystr;
                        firefoxProfile.SetProxyPreferences(proxy);
                        Application.UseWaitCursor = true;
                        var Driver = new FirefoxDriver(firefoxProfile);
                    }
                }
                catch
                {
                    firefoxProfile = new FirefoxProfile();
                    proxy = new Proxy();
                    proxy.HttpProxy = proxystr;
                    proxy.FtpProxy = proxystr;
                    proxy.SslProxy = proxystr;
                    proxy.SocksProxy = proxystr;

                    firefoxProfile.SetProxyPreferences(proxy);
                    Application.UseWaitCursor = true;
                    var Driver = new FirefoxDriver(firefoxProfile);
                }

            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (cmb_interfaces.SelectedIndex >= 0 && running == false)
                {
                    if (rdo_dhcp.Checked)
                    {
                        setdhcp_reg(interfaces_regwmi[cmb_interfaces.SelectedIndex].guid, rdo_dhcp.Checked);
                        int temp = cmb_interfaces.SelectedIndex;
                        updatelistinterface();
                        cmb_interfaces.SelectedIndex = temp;
                    }

                    else if (rdo_manual.Checked)
                    {
                        try
                        {
                            IPAddress address = null;
                            if (ipc_ip.IPAddress_string != "")
                                if (!IPAddress.TryParse(ipc_ip.IPAddress_string, out address))
                                    throw new Exception("اشتباه است IP");
                            IPAddress getway = null;
                            if (ipc_getway.IPAddress_string != "")
                                if (!IPAddress.TryParse(ipc_getway.IPAddress_string, out getway))
                                    throw new Exception("اشتباه است Getway");
                            IPAddress subnet = null;
                            if (ipc_subnet.IPAddress_string != "")
                            {
                                if (!IPAddress.TryParse(ipc_subnet.IPAddress_string, out subnet))
                                    throw new Exception("اشتباه است Subnet");
                            }

                            IPAddress dns1 = null;
                            if (ipc_dns1.IPAddress_string != "")
                                if (!IPAddress.TryParse(ipc_dns1.IPAddress_string, out dns1))

                                    throw new Exception("اشتباه است DNS1");

                            IPAddress dns2 = null;
                            if (ipc_dns2.IPAddress_string != "")
                                if (!IPAddress.TryParse(ipc_dns2.IPAddress_string, out dns2))

                                    throw new Exception("اشتباه است DNS2");

                            if (dns1 == dns2 && dns1 != null)
                                throw new Exception(" ها یکسان است DNS");
                            if (address == getway && address != null)
                                throw new Exception(" نمیتواند یکسان باشد IP و getway");
                            if (address == null && dns1 == null && dns2 == null)
                                throw new Exception("را انتخاب کنید dhcp تنظیمات");
                            if (address == null && getway != null)
                                throw new Exception(" به تنهایی نمیتواند قرار بگیرد Gatway ");
                            if ((address == null && subnet != null) || (address != null && subnet == null))
                                throw new Exception(" به تنهایی تنظیم نمی شوند IP و Subnet");

                            setdhcp_reg(interfaces_regwmi[cmb_interfaces.SelectedIndex].guid, false);
                            setip_reg(interfaces_regwmi[cmb_interfaces.SelectedIndex].guid, getway == null ? "" : getway.ToString(), subnet == null ? "" : subnet.ToString(), address == null ? "" : address.ToString());
                            setdns_reg(interfaces_regwmi[cmb_interfaces.SelectedIndex].guid, dns1 == null ? "" : dns1.ToString(), dns2 == null ? "" : dns2.ToString());

                            //   setip_wp(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString(), getway == null ? "" : getway.ToString(), subnet == null ? "" : subnet.ToString(), address == null ? "" : address.ToString());
                            //  setdns_wp(interfaces_regwmi[cmb_interfaces.SelectedIndex].NetConnectionID.ToLower(), dns1 == null ? "" : dns1.ToString(), dns2 == null ? "" : dns2.ToString());
                            int temp = cmb_interfaces.SelectedIndex;
                            updatelistinterface();
                            cmb_interfaces.SelectedIndex = temp;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("یک گزینه را انتخاب کنید");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        


        bool running = true;
        private void rdo_dhcp_CheckedChanged(object sender, EventArgs e)
        {
            if (cmb_interfaces.SelectedIndex >= 0 && running == false)
            {
                if (rdo_dhcp.Checked)
                    disabledit();
            }
        }


        public void disabledit()
        {
            ipc_dns1.IPAddress_string = ipc_dns2.IPAddress_string = ipc_getway.IPAddress_string = ipc_ip.IPAddress_string = ipc_subnet.IPAddress_string = "";
            pnl_txtedit.Enabled = false;
        }
        public void enableedit()
        {
            pnl_txtedit.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int p = 0;
                if (!int.TryParse(txt_portp1.Text, out p) || p < 0 || p > 65535)
                {
                    MessageBox.Show("پورت اشتباه است");
                    throw new Exception("");
                }

                var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();
                if (RunningProcessPaths.Contains("firefox.exe"))
                {
                    msg_box frm = new msg_box();
                    frm.ShowDialog();
                    if (frm.result == DialogResult.No)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        KillApplication("firefox");
                        SettingUpAProxyUsingFirefoxDriver(txt_ipp1.Text + ":" + txt_portp1.Text);
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        SettingUpAProxyUsingFirefoxDriver(txt_ipp1.Text + ":" + txt_portp1.Text);
                    }
                }
                else
                {
                    SettingUpAProxyUsingFirefoxDriver(txt_ipp1.Text + ":" + txt_portp1.Text);
                }
            }
            catch { }

        }



        public static void KillApplication(string name)
        {
            System.Diagnostics.Process[] procs = null;
            try
            {
                procs = Process.GetProcessesByName(name);
                Process Proc = procs[0];
                if (!Proc.HasExited)
                {
                    Proc.Kill();
                    Proc.WaitForExit();
                }
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }





        private void button3_Click(object sender, EventArgs e)
        {

            try
            {

                int port;
                if (int.TryParse(txt_portp1.Text, out port) && port > 0 && port < 65535)
                {
                    if (txt_ipp1.Text != "")
                    {
                        setproxyviaregistry(txt_ipp1.Text + ":" + txt_portp1.Text);
                        upproxyie();

                    }
                    else
                        MessageBox.Show("آدرس اشتباه است");

                }

                else
                    MessageBox.Show("پورت اشتباه است");
            }
            catch
            {

            }


        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                disableproxyviaregistry();
                upproxyie();
            }
            catch { }

        }

        private void rdo_manual_CheckedChanged(object sender, EventArgs e)
        {
            tcpconfig_reg item_regwmi = interfaces_regwmi.Find(a => a.NetConnectionID == cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());

            ipc_dns1.IPAddress_string = item_regwmi.dns1;
            ipc_dns2.IPAddress_string = item_regwmi.dns2;
            ipc_getway.IPAddress_string = item_regwmi.DefaultGateway;
            ipc_ip.IPAddress_string = item_regwmi.IPAddress;
            ipc_subnet.IPAddress_string = item_regwmi.SubnetMask;
            enableedit();
        }




        private void button5_Click(object sender, EventArgs e)
        {

            try
            {

                int port;
                if (int.TryParse(txt_portp2.Text, out port) && port > 0 && port < 65535)
                {
                    if (txt_ipp2.Text != "")
                    {
                        setproxyviaregistry(txt_ipp2.Text + ":" + txt_portp2.Text);
                        upproxyie();


                    }
                    else
                        MessageBox.Show("آدرس اشتباه است");

                }

                else
                    MessageBox.Show("پورت اشتباه است");
            }
            catch
            {

            }

        }


        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                int p = 0;
                if (!int.TryParse(txt_portp2.Text, out p) || p < 0 || p > 65535)
                {
                    MessageBox.Show("پورت اشتباه است");
                    throw new Exception("");
                }


                var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();
                if (RunningProcessPaths.Contains("firefox.exe"))
                {
                    msg_box frm = new msg_box();
                    frm.ShowDialog();
                    if (frm.result == DialogResult.No)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        KillApplication("firefox");
                        SettingUpAProxyUsingFirefoxDriver(txt_ipp2.Text + ":" + txt_portp2.Text);
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        SettingUpAProxyUsingFirefoxDriver(txt_ipp2.Text + ":" + txt_portp2.Text);
                    }
                }
                else
                {
                    SettingUpAProxyUsingFirefoxDriver(txt_ipp2.Text + ":" + txt_portp2.Text);
                }
            }
            catch { }

        }


        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = { txt_ipp1.Text, txt_ipp2.Text, txt_portp1.Text, txt_portp2.Text };
                System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + @"\info.txt", lines);
                MessageBox.Show("با موفقیت ذخیره شد");
            }
            catch
            {

            }
        }

        private void Formnew_Load(object sender, EventArgs e)
        {

        }

        private void lnkResetSetting_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                int i = cmb_interfaces.SelectedIndex;
                rdo_manual.Checked = false;
                rdo_dhcp.Checked = false;
                updatelistinterface();

                cmb_interfaces.SelectedIndex = i;
            }
            catch
            {

            }

        }

    }
}

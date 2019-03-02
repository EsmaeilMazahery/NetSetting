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
    public partial class Form_wmi : Form
    {
        public Form_wmi()
        {
            InitializeComponent();
            updatelistinterface();

            upproxyie();
            string[] lines = System.IO.File.ReadAllLines(System.Environment.CurrentDirectory + @"\info.txt");
            txt_ipp1.Text = lines[0];
            txt_ipp2.Text = lines[1];
            txt_portp1.Text = lines[2];
            txt_portp2.Text = lines[3];
        }

        public void upproxyie()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            lbl_IEstatus.Text = registry.GetValue("ProxyEnable").ToString() == "0" ? "Proxy Disable" : "Proxy Enable";
            lbl_ipIE.Text = registry.GetValue("ProxyServer") != null ? registry.GetValue("ProxyServer").ToString().Split(':')[0] : "";
            lbl_PORTIE.Text = registry.GetValue("ProxyServer") != null ? registry.GetValue("ProxyServer").ToString().Split(':')[1] : "";
        }

        public class tcpconfig
        {
            public string name = "";
            public string des = "";
            public string NetConnectionID = "";
            public string ip = "";
            public string getway = "";
            public string subnet = "";
            public string dns1 = "";
            public string dns2 = "";
            public string guid = "";
            public bool enabledhcp;
            public bool enablednsauto;
            public string mac = "";
        }



        public List<tcpconfig> interfaces_wmi;


        public void updatelistinterfaces_wmi()
        {
            interfaces_wmi = new List<tcpconfig>();

            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                tcpconfig item = new tcpconfig();


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

                foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                {
                    item.enabledhcp = (bool)configuration["DHCPEnabled"];

                    if (configuration["MACAddress"] != null)
                    {
                        string mac = configuration["MACAddress"].ToString();
                        item.mac = mac;
                    }

                    if (configuration["IPAddress"] != null)
                    {
                        string[] ipaddresses = (string[])configuration["IPAddress"];
                        if (ipaddresses != null)
                        {
                            item.ip = ipaddresses[0];
                        }

                    }

                    if (configuration["IPSubnet"] != null)
                    {
                        string[] subnets = (string[])configuration["IPSubnet"];
                        if (subnets != null)
                        {
                            item.subnet = subnets[0];
                        }
                    }

                    if (configuration["DefaultIPGateway"] != null)
                    {
                        string[] gateways = (string[])configuration["DefaultIPGateway"];

                        if (configuration["DefaultIPGateway"] != null)
                        {
                            item.getway = gateways[0];
                        }
                    }




                    string[] DNSServerSearchOrder = null;
                    if (configuration["DNSServerSearchOrder"] != null)
                    {
                        DNSServerSearchOrder = (string[])configuration["DNSServerSearchOrder"];
                    }


                    if (!item.enabledhcp)
                        item.enablednsauto = false;
                    else if (DNSServerSearchOrder == null)
                        item.enablednsauto = true;
                    else
                        item.enablednsauto = false;

                    if (DNSServerSearchOrder != null)
                    {
                        item.dns1 = DNSServerSearchOrder[0];
                        if (DNSServerSearchOrder.Length > 1)
                            item.dns2 = DNSServerSearchOrder[1];
                    }

                }
                interfaces_wmi.Add(item);
            }
        }

        public void updatelistinterface()
        {
            cmb_interfaces.Items.Clear();
            disabledit();
            updatelistinterfaces_wmi();
            foreach (tcpconfig item in interfaces_wmi)
            {
                cmb_interfaces.Items.Add(item.NetConnectionID);
            }

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

            // These lines implement the Interface in the beginning of program 
            // They cause the OS to refresh the settings, causing IP to realy update
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

        public UInt32 setIP_wmi(string netconnectionid, string ip_address, string subnet_mask)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    if (networkAdapter["NetConnectionID"].ToString() != netconnectionid)
                        continue;
                }
                else
                {
                    continue;
                }
                foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                {

                    try
                    {
                        ManagementBaseObject setdns;
                        ManagementBaseObject newIP =
                            configuration.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { ip_address };
                        newIP["SubnetMask"] = new string[] { subnet_mask };

                        setdns = configuration.InvokeMethod("EnableStatic", newIP, null);
                        UInt32 result = (UInt32)(setdns["returnValue"]);
                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {

                    }
                }
            }
            return 9999999;
        }
        public UInt32 setdhcp_wmi(string netconnectionid)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    if (networkAdapter["NetConnectionID"].ToString() != netconnectionid)
                        continue;
                }
                else
                {
                    continue;
                }
                foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                {

                    try
                    {
                        ManagementBaseObject setdns;
                        ManagementBaseObject newIP =
                            configuration.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = null;
                        newIP["SubnetMask"] = null;

                        setdns = configuration.InvokeMethod("EnableDHCP", null, null);
                        UInt32 result = (UInt32)(setdns["returnValue"]);
                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {

                    }



                }
            }
            return 9999999;
        }

        public UInt32 setGateway_wmi(string netconnectionid, string gateway)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    if (networkAdapter["NetConnectionID"].ToString() != netconnectionid)
                        continue;
                }
                else
                {
                    continue;
                }
                foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                {

                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            configuration.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 1 };

                        setGateway = configuration.InvokeMethod("SetGateways", newGateway, null);
                        UInt32 result = (UInt32)(setGateway["returnValue"]);
                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
            return 9999999;
        }



        public UInt32 setDNS_wmi(string netconnectionid)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    if (networkAdapter["NetConnectionID"].ToString() != netconnectionid)
                        continue;
                }
                else
                {
                    continue;
                }
                foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                {

                    try
                    {
                        ManagementBaseObject mboDNS = configuration.GetMethodParameters("SetDNSServerSearchOrder");
                        if (mboDNS != null)
                        {
                            ManagementBaseObject setIP;
                            mboDNS["DNSServerSearchOrder"] = null;
                            setIP = configuration.InvokeMethod("SetDNSServerSearchOrder", mboDNS, null);
                            UInt32 result = (UInt32)(setIP["returnValue"]);
                            return result;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
            return 9999999;
        }
        public UInt32 setDNS_wmi(string netconnectionid, string DNS)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    if (networkAdapter["NetConnectionID"].ToString() != netconnectionid)
                        continue;
                }
                else
                {
                    continue;
                }
                foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                {
                    try
                    {
                        ManagementBaseObject newDNS = configuration.GetMethodParameters("SetDNSServerSearchOrder");
                        if (newDNS != null)
                        {
                            ManagementBaseObject setIP;
                            newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                            setIP = configuration.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                            UInt32 result = (UInt32)(setIP["returnValue"]);
                            return result;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
            return 9999999;
        }

        private void cmb_interfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_interfaces.SelectedIndex >= 0)
            {
                tcpconfig item_wmi = interfaces_wmi.Find(a => a.NetConnectionID == cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                pnl_edit.Enabled = true;

                lbl_dnspre2.Text = item_wmi.dns1;
                lbl_showaltdns2.Text = item_wmi.dns2;
                lbl_showdes2.Text = item_wmi.des;
                lbl_showgetway2.Text = item_wmi.getway;
                lbl_showsubnet2.Text = item_wmi.subnet;
                lbl_showname2.Text = item_wmi.NetConnectionID;
                lbl_showip2.Text = item_wmi.ip;
                lbl_Mac.Text = item_wmi.mac;



                rdo_dhcp.Checked = item_wmi.enabledhcp;
                rdo_manual.Checked = !rdo_dhcp.Checked;
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
            var firefoxProfile = new FirefoxProfile(); ;
            if (profile_dir != "")
                firefoxProfile = new FirefoxProfile(profile_dir);

            var proxy = new Proxy();
            proxy.HttpProxy = proxystr;
            proxy.FtpProxy = proxystr;
            proxy.SslProxy = proxystr;
            proxy.SocksProxy = proxystr;

            firefoxProfile.SetProxyPreferences(proxy);
            Application.UseWaitCursor = true;
            var Driver = new FirefoxDriver(firefoxProfile);
            Application.UseWaitCursor = false;

            //  Driver.Navigate().GoToUrl("http/www.google.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_interfaces.SelectedIndex >= 0 && running == false)
                {
                    if (rdo_dhcp.Checked)
                    {
                        UInt32 dhcp, dns;
                        dhcp = setdhcp_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                        dns = setDNS_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                        if (dns != 0)
                            addstatus(dns + " DNS خطا تنظیمات");
                        if (dhcp != 0)
                            addstatus(dhcp + " DHCP خطا تنظیمات");
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
                            else
                                IPAddress.TryParse("255.255.255.0", out subnet);
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

                            UInt32 ipres, dnsres, getwayres = 99999;
                            if (!(address == null && subnet == null))
                                ipres = setIP_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString(), address == null ? "" : address.ToString(), subnet == null ? "" : subnet.ToString());
                            else
                                ipres = setdhcp_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                            if (!(dns1 == null && dns2 == null))
                                dnsres = setDNS_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString(), dns1 == null ? "" : dns1.ToString() + (dns2 == null ? "" : "," + dns2.ToString()));
                            else
                                dnsres = setDNS_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                            if (getway != null)
                                getwayres = setGateway_wmi(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString(), getway == null ? "" : getway.ToString());

                            if (ipres != 0)
                            {
                                addstatus(Array.Find(Error, p => p.IndexOf("(" + ipres + ")") >= 0) );
                            }
                            if (dnsres != 0)
                            {
                                addstatus(Array.Find(Error, p => p.IndexOf("(" + dnsres + ")") >= 0)) ;
                            }
                            if (getwayres != 0 && getwayres != 99999)
                            {
                                addstatus(Array.Find(Error,p=> p.IndexOf("("+getwayres+")")>=0) );
                            }

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
                    int temp = cmb_interfaces.SelectedIndex;
                    updatelistinterface();
                    cmb_interfaces.SelectedIndex = temp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string[] Error =
        {
            "Successful completion, no reboot required (0)",
            "Successful completion, reboot required (1)",
            "Method not supported on this platform (64)",
            "Unknown failure (65)",
            "Invalid subnet mask (66)",
            "An error occurred while processing an Instance that was returned (67)",
            "Invalid input parameter (68)",
            "More than 5 gateways specified (69)",
            "Invalid IP address (70)",
            "Invalid gateway IP address (71)",
            "An error occurred while accessing the Registry for the requested information (72)",
            "Invalid domain name (73)",
            "Invalid host name (74)",
            "No primary/secondary WINS server defined (75)",
            "Invalid file (76)",
            "Invalid system path (77)",
            "File copy failed (78)",
            "Invalid security parameter (79)",
            "Unable to configure TCP/IP service (80)",
            "Unable to configure DHCP service (81)",
            "Unable to renew DHCP lease (82)",
            "Unable to release DHCP lease (83)",
            "IP not enabled on adapter (84)",
            "IPX not enabled on adapter (85)",
            "Frame/network number bounds error (86)",
            "Invalid frame type (87)",
            "Invalid network number (88)",
            "Duplicate network number (89)",
            "Parameter out of bounds (90)",
            "Access denied (91)",
            "Out of memory (92)",
            "Already exists (93)",
            "Path, file or object not found (94)",
            "Unable to notify service (95)",
            "Unable to notify DNS service (96)",
            "Interface not configurable (97)",
            "Not all DHCP leases could be released/renewed (98)",
            "DHCP not enabled on adapter (100)",
            "Other (101–4294967295)"
        };
    bool running = true;
        private void rdo_dhcp_CheckedChanged(object sender, EventArgs e)
        {
            if (cmb_interfaces.SelectedIndex >= 0 && running == false)
            {
                if (rdo_dhcp.Checked)
                    disabledit();
            }
        }
        Timer stat = new Timer();
        public void addstatus(string s)
        {
            lbl_status.Visible = true;
            lbl_status.Text = lbl_status.Text + Environment.NewLine + s;
            stat.Interval = 10000;
            stat.Tick += Stat_Tick;
            stat.Enabled = true;
            stat.Start();
        }

        private void Stat_Tick(object sender, EventArgs e)
        {
            stat.Enabled = false;
            stat.Stop();
            lbl_status.Text = "";
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
            enableedit();
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

        private void button10_Click(object sender, EventArgs e)
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

        private void pnl_info_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

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
    public partial class Form_all : Form
    {
        public Form_all()
        {
            InitializeComponent();
            updatelistinterface();

            upproxyie();
            t.Interval = 5000;
            t.Enabled = true;
            t.Start();
            string[] lines = System.IO.File.ReadAllLines(System.Environment.CurrentDirectory + @"\info.txt");
            txt_ipp1.Text = lines[0];
            txt_ipp2.Text = lines[1];
            txt_ipp3.Text = lines[2];
            txt_portp1.Text = lines[3];
            txt_portp2.Text = lines[4];
            txt_portp3.Text = lines[5];
        }

        public void upproxyie()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
            lbl_IEstatus.Text = registry.GetValue("ProxyEnable").ToString() == "0" ? "Proxy Disable" : "Proxy Enable";
            lbl_ipIE.Text = registry.GetValue("ProxyServer") != null ? registry.GetValue("ProxyServer").ToString().Split(':')[0] : "";
            lbl_PORTIE.Text = registry.GetValue("ProxyServer") != null ? registry.GetValue("ProxyServer").ToString().Split(':')[1] : "";


        }

        Timer t = new Timer();
        public void timeroff()
        {
            t.Enabled = false;
            t.Stop();
        }
        public void timeron()
        {
            t.Enabled = true;
            t.Start();
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
        public class tcpconfig_reg
        {
            public string name = "";
            public string des = "";
            public string NetConnectionID = "";
            public string dns1 = "";
            public string dns2 = "";
            public string guid = "";
            public bool enablednsauto;
            public string mac = "";

            public string IPAddress = "";
            public string DefaultGateway = "";
            public bool EnableDHCP;
            public string SubnetMask = "";
          //  public string NameServer = "";

            public string DhcpIPAddress = "";
            public string DhcpServer = "";
            public string DhcpSubnetMask = "";
            public string DhcpDefaultGateway = "";
        }

        public List<tcpconfig_reg> interfaces_regwmi;
        public List<tcpconfig> interfaces_wmi;
        public List<tcpconfig> interfaces_netsh;
        public List<tcpconfig> interfaces_NetworkInterface;

        public List<tcpconfig_reg> interfaces;

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
                if (IPAddress != null)
                    item.IPAddress = IPAddress[0];
                string[] EnableDHCP = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "EnableDHCP");

                string[] DefaultGateway = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DefaultGateway");
                string[] SubnetMask = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "SubnetMask");
                string[] NameServer = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "NameServer");

                string[] DhcpIPAddress = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpIPAddress");
                string[] DhcpServer = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpServer");
                string[] DhcpSubnetMask = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpSubnetMask");
                string[] DhcpDefaultGateway = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DhcpDefaultGateway");



                
                interfaces_regwmi.Add(item);
            }
        }
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
                    else
                    {


                        string[] temp = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "IPAddress");
                        if (temp != null && temp.Length > 0)
                        {
                            item.ip = temp[0];

                            string[] temp2 = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "EnableDHCP");
                            if (temp2 != null && temp2.Length > 0)
                            {

                                item.enabledhcp = temp2[0] == "1" ? true : false;
                            }


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
                    else
                    {
                        string[] temp = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "SubnetMask");
                        if (temp != null && temp.Length > 0)
                            item.subnet = temp[0];
                    }
                    if (configuration["DefaultIPGateway"] != null)
                    {
                        string[] gateways = (string[])configuration["DefaultIPGateway"];

                        if (configuration["DefaultIPGateway"] != null)
                        {
                            item.getway = gateways[0];
                        }
                    }
                    else
                    {
                        string[] temp = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "DefaultGateway");
                        if (temp != null && temp.Length > 0)
                            item.subnet = temp[0];
                    }



                    string[] DNSServerSearchOrder = null;
                    if (configuration["DNSServerSearchOrder"] != null)
                    {
                        DNSServerSearchOrder = (string[])configuration["DNSServerSearchOrder"];
                    }
                    else if (((DNSServerSearchOrder = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item.guid, "NameServer")) != null) && DNSServerSearchOrder.Length > 0) ;
                    else
                        DNSServerSearchOrder = null;


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

        public void updatelistinterfaces_netsh()
        {
            interfaces_netsh = new List<tcpconfig>();
            string output;
            runprocess("netsh", "interface ipv4 show config", out output);
            tcpconfig item = new tcpconfig();
            bool dns = false;
            foreach (string line in output.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line == "")
                {
                    dns = false;
                    continue;
                }

                else if (line.IndexOf("Configuration for interface") >= 0)
                {
                    dns = false;
                    if (item.NetConnectionID != "")
                        interfaces_netsh.Add(item);
                    item = new tcpconfig();
                    item.NetConnectionID = line.Replace("Configuration for interface", "").Trim().Replace("\"", "");
                }
                else if (line.IndexOf("DHCP enabled:") >= 0)
                {
                    dns = false;
                    item.enabledhcp = line.Replace("DHCP enabled:", "").Trim().Replace("\"", "") == "No" ? false : true;
                }
                else if (line.IndexOf("Default Gateway:") >= 0)
                {
                    dns = false;
                    item.getway = line.Replace("Default Gateway:", "").Trim().Replace("\"", "");
                }
                else if (line.IndexOf("IP Address:") >= 0)
                {
                    dns = false;
                    item.ip = line.Replace("IP Address:", "").Trim().Replace("\"", "");
                }
                else if (line.IndexOf("Subnet Prefix:") >= 0)
                {
                    dns = false;
                    item.subnet = line.Replace("Subnet Prefix:", "").Trim();
                    item.subnet = item.subnet.Substring(item.subnet.IndexOf("(mask") + 6, item.subnet.Length - item.subnet.IndexOf("(mask") - 7);
                }
                else if (line.IndexOf("DNS servers configured through DHCP:") >= 0)
                {
                    item.dns1 = line.Replace("DNS servers configured through DHCP:", "").Trim().Replace("\"", "").Replace("None", "");
                    dns = true;
                }
                else if (line.IndexOf("Statically Configured DNS Servers:") >= 0)
                {
                    item.dns1 = line.Replace("Statically Configured DNS Servers:", "").Trim().Replace("\"", "").Replace("None", "");
                    dns = true;
                }
                else
                {
                    if (dns)
                    {
                        dns = false;
                        IPAddress ip;
                        if (IPAddress.TryParse(line.Trim(), out ip))
                            item.dns2 = ip.ToString();
                    }
                }
            }
            interfaces_netsh.Add(item);
        }

        public void updatelistinterfaces_NetworkInterface()
        {
            interfaces_NetworkInterface = new List<tcpconfig>();
            NetworkInterface[] ifaceList = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface iface in ifaceList)
            {
                if (iface.OperationalStatus == OperationalStatus.Up)
                {
                    if (iface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || iface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        tcpconfig item = new tcpconfig();
                        item.NetConnectionID = iface.Name;
                        item.name = iface.Description;
                        item.des = iface.Description;
                        item.guid = iface.Id;
                        item.mac = iface.GetPhysicalAddress().ToString();
                        IPInterfaceProperties Properties = iface.GetIPProperties();
                        IPAddressCollection DNSs = Properties.DnsAddresses;
                        bool first = true;
                        foreach (IPAddress dns in DNSs)
                        {
                            if (dns.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                if (first)
                                {
                                    item.dns1 = dns.ToString();
                                    first = false;
                                }

                                else
                                {
                                    item.dns2 = dns.ToString();
                                    break;
                                }
                            }
                        }

                        item.enablednsauto = Properties.IsDynamicDnsEnabled;

                        //   properties.IsDnsEnabled
                        //  properties.IsDynamicDnsEnabled);



                        UnicastIPAddressInformationCollection unicastIPC = Properties.UnicastAddresses;
                        foreach (UnicastIPAddressInformation unicast in unicastIPC)
                        {
                            if (unicast.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                item.ip = unicast.Address.ToString();
                                item.subnet = unicast.IPv4Mask.ToString();
                                break;
                            }
                        }
                        GatewayIPAddressInformationCollection unicastIPC1 = Properties.GatewayAddresses;
                        foreach (GatewayIPAddressInformation unicast in unicastIPC1)
                        {
                            if (unicast.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                item.getway = unicast.Address.ToString();
                                break;
                            }
                        }


                        interfaces_NetworkInterface.Add(item);
                    }
                }
            }
        }

        public void updatelistinterface()
        {
            cmb_interfaces.Items.Clear();
            disabledit();
            updatelistinterfaces_regwmi();
         //   updatelistinterfaces_wmi();
         //   updatelistinterfaces_netsh();
         //   updatelistinterfaces_NetworkInterface();
            interfaces = interfaces_regwmi;
            foreach (tcpconfig_reg item in interfaces_regwmi)
            {
                cmb_interfaces.Items.Add(item.NetConnectionID);
            }
            /*
            if (max < interfaces_netsh.Count)
            {
                max = interfaces_netsh.Count;
                interfaces = interfaces_netsh;
            }

            if (max < interfaces_NetworkInterface.Count)
            {
                max = interfaces_NetworkInterface.Count;
                interfaces = interfaces_NetworkInterface;
            }

            foreach (tcpconfig item in interfaces_netsh)
            {
                cmb_interfaces.Items.Add(item.NetConnectionID);
            }
            foreach (tcpconfig item in interfaces_wmi)
            {
                if (cmb_interfaces.Items.IndexOf(item.NetConnectionID) < 0)
                    cmb_interfaces.Items.Add(item.NetConnectionID);
            }
            foreach (tcpconfig item in interfaces_NetworkInterface)
            {
                if (cmb_interfaces.Items.IndexOf(item.NetConnectionID) < 0)
                    cmb_interfaces.Items.Add(item.NetConnectionID);
            }
            */
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
        public void runprocess(string app, string command, out string output)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(app, command);
            psi.WindowStyle = ProcessWindowStyle.Minimized;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;
            p.StartInfo = psi;
            p.Start();
            output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }

        public void addlog(string log, string type)
        {
            if (log.Trim() != "")
                lst_log.Items.Add(type + ":" + log.Trim());
        }


        public void setip_wp(string ID, string gateway, string subnetMask, string address)
        {
            string output;
            runprocess("netsh", "interface ip set address \"" + ID + "\" static \"" + address + "\" \"" + subnetMask + "\" \"" + gateway + "\"", out output);
            addlog(output, "ip");
        }
        public void enabledhcp_wp(string ID)
        {
            string output;
            runprocess("netsh", "interface ip set address \"" + ID + "\" dhcp", out output);
            addlog(output, "enable dhcp");
            runprocess("netsh", "interface ip set dns name=\"" + ID + "\" source =dhcp", out output);
            addlog(output, "enable dhcp dns");
        }


        public void setdns_wp(string ID, string dns1, string dns2)
        {
            string output;
            runprocess("netsh", "interface ip set dns \"" + ID + "\" dhcp", out output);
            addlog(output, "dns");

            if (dns1 != "")
            {
                runprocess("netsh", "interface ipv4 add dnsserver \"" + ID + "\" address=\"" + dns1 + "\" index=1", out output);
                addlog(output, "dns");
            }

            if (dns2 != "")
            {
                runprocess("netsh", "interface ipv4 add dnsserver \"" + ID + "\" address=\"" + dns2 + "\" index=2", out output);
                addlog(output, "dns");
            }

        }


        public void runprocess(string app, string command)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(app, command);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo = psi;
            p.Start();
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




        private void cmb_interfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_interfaces.SelectedIndex >= 0)
            {
                tcpconfig item_netsh = interfaces_netsh.Find(a => a.NetConnectionID == cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                tcpconfig item_wmi = interfaces_wmi.Find(a => a.NetConnectionID == cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                tcpconfig item_NetworkInterface = interfaces_NetworkInterface.Find(a => a.NetConnectionID == cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString());
                lbl_DisableDhcpOnConnect.Visible = false;



                pnl_edit.Enabled = true;
                if (item_netsh != null && item_netsh.dns1 != "")
                    lbl_dnspre2.Text = item_netsh.dns1;
                else if (item_wmi != null && item_wmi.dns1 != "")
                    lbl_dnspre2.Text = item_wmi.dns1;
                else if (item_NetworkInterface != null)
                    lbl_dnspre2.Text = item_NetworkInterface.dns1;

                if (item_netsh != null && item_netsh.dns2 != "")
                    lbl_showaltdns2.Text = item_netsh.dns2;
                else if (item_wmi != null && item_wmi.dns2 != "")
                    lbl_showaltdns2.Text = item_wmi.dns2;
                else if (item_NetworkInterface != null)
                    lbl_showaltdns2.Text = item_NetworkInterface.dns2;

                if (item_netsh != null && item_netsh.des != "")
                    lbl_showdes2.Text = item_netsh.des;
                else if (item_wmi != null && item_wmi.des != "")
                    lbl_showdes2.Text = item_wmi.des;
                else if (item_NetworkInterface != null)
                    lbl_showdes2.Text = item_NetworkInterface.des;

                if (item_netsh != null && item_netsh.getway != "")
                    lbl_showgetway2.Text = item_netsh.getway;
                else if (item_wmi != null && item_wmi.getway != "")
                    lbl_showgetway2.Text = item_wmi.getway;
                else if (item_NetworkInterface != null)
                    lbl_showgetway2.Text = item_NetworkInterface.getway;

                if (item_netsh != null && item_netsh.subnet != "")
                    lbl_showsubnet2.Text = item_netsh.subnet;
                else if (item_wmi != null && item_wmi.subnet != "")
                    lbl_showsubnet2.Text = item_wmi.subnet;
                else if (item_NetworkInterface != null)
                    lbl_showsubnet2.Text = item_NetworkInterface.subnet;

                if (item_netsh != null && item_netsh.NetConnectionID != "")
                    lbl_showname2.Text = item_netsh.NetConnectionID;
                else if (item_wmi != null && item_wmi.NetConnectionID != "")
                    lbl_showname2.Text = item_wmi.NetConnectionID;
                else if (item_NetworkInterface != null)
                    lbl_showname2.Text = item_NetworkInterface.NetConnectionID;

                if (item_netsh != null && item_netsh.ip != "")
                    lbl_showip2.Text = item_netsh.ip;
                else if (item_wmi != null && item_wmi.ip != "")
                    lbl_showip2.Text = item_wmi.ip;
                else if (item_NetworkInterface != null)
                    lbl_showip2.Text = item_NetworkInterface.ip;

                if (item_netsh != null && item_netsh.mac != "")
                    lbl_Mac.Text = item_netsh.mac;
                else if (item_wmi != null && item_wmi.mac != "")
                    lbl_Mac.Text = item_wmi.mac;
                else if (item_NetworkInterface != null)
                    lbl_Mac.Text = item_NetworkInterface.mac;



                running = true;

                if (item_netsh != null)
                {
                    if (item_netsh.enabledhcp)
                    {
                        if (item_wmi != null)
                        {
                            string[] DisableDhcpOnConnect = (string[])getreg(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces\" + item_wmi.guid, "DisableDhcpOnConnect");
                            if (DisableDhcpOnConnect != null && string.Join("", DisableDhcpOnConnect) != "")
                            {
                                lbl_DisableDhcpOnConnect.Visible = true;
                                rdo_dhcp.Checked = !(string.Join("", DisableDhcpOnConnect) == "1" ? true : false);
                                rdo_manual.Checked = !rdo_dhcp.Checked;
                            }
                            else
                            {
                                rdo_dhcp.Checked = item_netsh.enabledhcp;
                                rdo_manual.Checked = !rdo_dhcp.Checked;
                            }
                        }
                        else
                        {
                            rdo_dhcp.Checked = item_netsh.enabledhcp;
                            rdo_manual.Checked = !rdo_dhcp.Checked;
                        }
                    }
                }

                else if (item_wmi != null && item_wmi.mac != "")
                {
                    lbl_Mac.Text = item_wmi.mac;

                }




                disabledit();
                if (rdo_manual.Checked)
                {
                    txt_dns1.Text = lbl_dnspre2.Text;
                    txt_dns2.Text = lbl_showaltdns2.Text;
                    txt_getway.Text = lbl_showgetway2.Text;
                    txt_ip.Text = lbl_showip2.Text;
                    txt_subnet.Text = lbl_showsubnet2.Text;
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
            timeroff();
            try
            {
                if (cmb_interfaces.SelectedIndex >= 0 && running == false)
                {
                    if (rdo_dhcp.Checked)
                        enabledhcp_wp(interfaces[cmb_interfaces.SelectedIndex].NetConnectionID.ToLower());
                    else if (rdo_manual.Checked)
                    {
                        try
                        {
                            IPAddress address = null;
                            if (txt_ip.Text != "")
                                if (!IPAddress.TryParse(txt_ip.Text, out address))
                                    throw new Exception("اشتباه است IP");
                            IPAddress getway = null;
                            if (txt_getway.Text != "")
                                if (!IPAddress.TryParse(txt_getway.Text, out getway))
                                    throw new Exception("اشتباه است Getway");
                            IPAddress subnet = null;
                            if (txt_subnet.Text != "")
                            {
                                if (!IPAddress.TryParse(txt_subnet.Text, out subnet))
                                    throw new Exception("اشتباه است Subnet");
                            }
                            else
                                IPAddress.TryParse("255.255.255.0", out subnet);
                            IPAddress dns1 = null;
                            if (txt_dns1.Text != "")
                                if (!IPAddress.TryParse(txt_dns1.Text, out dns1))

                                    throw new Exception("اشتباه است DNS1");

                            IPAddress dns2 = null;
                            if (txt_dns2.Text != "")
                                if (!IPAddress.TryParse(txt_dns2.Text, out dns2))

                                    throw new Exception("اشتباه است DNS2");

                            if (dns1 == dns2 && dns1 != null)
                                throw new Exception(" ها یکسان است DNS");
                            if (address == getway && address != null)
                                throw new Exception(" نمیتواند یکسان باشد IP و getway");
                            if (address == null && dns1 == null && dns2 == null)
                                throw new Exception("را انتخاب کنید dhcp تنظیمات");
                            if (address == null && getway != null)
                                throw new Exception(" به تنهایی نمیتواند قرار بگیرد Gatway ");

                            setip_wp(cmb_interfaces.Items[cmb_interfaces.SelectedIndex].ToString(), getway == null ? "" : getway.ToString(), subnet == null ? "" : subnet.ToString(), address == null ? "" : address.ToString());
                            setdns_wp(interfaces[cmb_interfaces.SelectedIndex].NetConnectionID.ToLower(), dns1 == null ? "" : dns1.ToString(), dns2 == null ? "" : dns2.ToString());

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
            timeron();
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
            txt_dns1.Text = txt_dns2.Text = txt_getway.Text = txt_ip.Text = txt_subnet.Text = "";
            pnl_txtedit.Enabled = false;
        }
        public void enableedit()
        {
            pnl_txtedit.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            timeroff();
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
            timeron();
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
            timeroff();
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

            timeron();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timeroff();
            try
            {
                disableproxyviaregistry();
                upproxyie();
            }
            catch { }
            timeron();
        }

        private void rdo_manual_CheckedChanged(object sender, EventArgs e)
        {
            enableedit();
        }

        private void lbl_showname_Click(object sender, EventArgs e)
        {

        }

        private void lbl_showip2_Click(object sender, EventArgs e)
        {

        }

        private void ctlModernBlack1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            timeroff();
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

            timeron();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            timeroff();
            try
            {

                int port;
                if (int.TryParse(txt_portp3.Text, out port) && port > 0 && port < 65535)
                {
                    if (txt_ipp3.Text != "")
                    {
                        setproxyviaregistry(txt_ipp3.Text + ":" + txt_portp3.Text);
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

            timeron();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            timeroff();
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
            timeron();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timeroff();
            try
            {
                int p = 0;
                if (!int.TryParse(txt_portp3.Text, out p) || p < 0 || p > 65535)
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
                        SettingUpAProxyUsingFirefoxDriver(txt_ipp3.Text + ":" + txt_portp3.Text);
                        Cursor.Current = Cursors.Default;
                    }
                    else
                    {
                        SettingUpAProxyUsingFirefoxDriver(txt_ipp3.Text + ":" + txt_portp3.Text);
                    }
                }
                else
                {
                    SettingUpAProxyUsingFirefoxDriver(txt_ipp3.Text + ":" + txt_portp3.Text);
                }
            }
            catch { }
            timeron();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = { txt_ipp1.Text, txt_ipp2.Text, txt_ipp3.Text, txt_portp1.Text, txt_portp2.Text, txt_portp3.Text };
                System.IO.File.WriteAllLines(System.Environment.CurrentDirectory + @"\info.txt", lines);
                MessageBox.Show("با موفقیت ذخیره شد");
            }
            catch
            {

            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (lst_log.Visible)
            {
                lst_log.Visible = false;
                this.Size = new Size(this.Size.Width, this.Size.Height - lst_log.Size.Height);
            }
            else
            {
                lst_log.Visible = true;
                this.Size = new Size(this.Size.Width, this.Size.Height + lst_log.Size.Height);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            timeroff();
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
            timeron();
        }
    }
}

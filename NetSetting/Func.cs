using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetSetting
{
    class Func
    {

        public void setip(string ID, string gateway, string subnetMask, string address)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    string NetID = networkAdapter["NetConnectionID"] as string;
                    if (string.Compare(NetID,
                        ID, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        try
                        {
                            foreach (ManagementObject configuration in
                                             networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                            {

                                if (address == "")
                                {

                                }
                                else
                                {
                                    var newAddress = configuration.GetMethodParameters("EnableStatic");
                                    newAddress["IPAddress"] = new string[] { address };
                                    newAddress["SubnetMask"] = new string[] { subnetMask };
                                    var newGateway = configuration.GetMethodParameters("SetGateways");
                                    newGateway["DefaultIPGateway"] = new string[] { gateway };
                                    newGateway["GatewayCostMetric"] = new int[] { 1 };
                                }


                                /*

                                ManagementBaseObject aaa = configuration.InvokeMethod("EnableStatic", newAddress, null);
                                string ss = aaa["returnValue"].ToString();
                                aaa = configuration.InvokeMethod("SetGateways", newGateway, null);
                                ss = aaa["returnValue"].ToString();*/
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
                else
                {
                    continue;
                }
            }
        }


        public void setip_netsh(string ID, string gateway, string subnetMask, string address)
        {
            string output;
            runprocess("netsh", "interface ip set address \"" + ID + "\" static \"" + address + "\" \"" + subnetMask + "\" \"" + gateway + "\"", out output);
            addlog(output, "ip");
        }
        public void enabledhcp_netsh(string ID)
        {
            string output;
            runprocess("netsh", "interface ip set address \"" + ID + "\" dhcp", out output);
            addlog(output, "enable dhcp");
            runprocess("netsh", "interface ip set dns name=\"" + ID + "\" source =dhcp", out output);
            addlog(output, "enable dhcp dns");
        }


        public void setdns_netsh(string ID, string dns1, string dns2)
        {
            string output;
            runprocess("netsh", "interface ip set dns \"" + ID + "\" dhcp", out output);
            addlog(output, "dns");

            if (dns1 != "")
            {
                runprocess("netsh", "interface ipv4 add dnsserver \"" + ID + "\" address=\"" + dns1 + "\" index=1", out output);
                addlog(output, "dns");
            }

            if (dns2 != "" && dns2 != "0.0.0.0")
            {
                runprocess("netsh", "interface ipv4 add dnsserver \"" + ID + "\" address=\"" + dns2 + "\" index=2", out output);
                addlog(output, "dns");
            }

        }


        public void addlog(string log, string type)
        {
            if (log.Trim() != "")
                Console.Write(type + ":" + log.Trim());
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


        public void runprocess(string app, string command)
        {
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(app, command);
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo = psi;
            p.Start();
        }




        public void setdns(string ID, string dns1, string dns2)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    string NetID = networkAdapter["NetConnectionID"] as string;
                    if (string.Compare(NetID,
                        ID, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        try
                        {
                            foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                            {
                                string[] newAddress = { dns1, dns2 };
                                configuration.InvokeMethod("SetDNSServerSearchOrder", newAddress);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
                else
                {
                    continue;
                }
            }
        }

        public void enabledhcp(string ID)
        {
            SelectQuery wmiQuery = new SelectQuery("SELECT * FROM Win32_NetworkAdapter");
            ManagementObjectSearcher searchProcedure = new ManagementObjectSearcher(wmiQuery);
            foreach (ManagementObject networkAdapter in searchProcedure.Get())
            {
                if (networkAdapter["NetConnectionID"] != null)
                {
                    string NetID = networkAdapter["NetConnectionID"] as string;
                    if (string.Compare(NetID,
                        ID, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        try
                        {
                            foreach (ManagementObject configuration in
                    networkAdapter.GetRelated("Win32_NetworkAdapterConfiguration"))
                            {
                                configuration.InvokeMethod("EnableDHCP", null);
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                }
                else
                {
                    continue;
                }
            }
        }


        public string getreg(string subkey, string key)
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
                            return o.ToString();
                        }
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
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


        public void setIP(string ip_address, string subnet_mask)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setIP;
                        ManagementBaseObject newIP =
                            objMO.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { ip_address };
                        newIP["SubnetMask"] = new string[] { subnet_mask };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
            }
        }
        /// <summary>
        /// Set's a new Gateway address of the local machine
        /// </summary>
        /// <param name="gateway">The Gateway IP Address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void setGateway(string gateway)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setGateway;
                        ManagementBaseObject newGateway =
                            objMO.GetMethodParameters("SetGateways");

                        newGateway["DefaultIPGateway"] = new string[] { gateway };
                        newGateway["GatewayCostMetric"] = new int[] { 1 };

                        setGateway = objMO.InvokeMethod("SetGateways", newGateway, null);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// Set's the DNS Server of the local machine
        /// </summary>
        /// <param name="NIC">NIC address</param>
        /// <param name="DNS">DNS server address</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void setDNS(string NIC, string DNS)
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    // if you are using the System.Net.NetworkInformation.NetworkInterface you'll need to change this line to if (objMO["Caption"].ToString().Contains(NIC)) and pass in the Description property instead of the name 
                    if (objMO["Caption"].Equals(NIC))
                    {
                        try
                        {
                            ManagementBaseObject newDNS =
                                objMO.GetMethodParameters("SetDNSServerSearchOrder");
                            newDNS["DNSServerSearchOrder"] = DNS.Split(',');
                            ManagementBaseObject setDNS =
                                objMO.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public void ShowActiveTcpConnections()
        {
            Console.WriteLine("Active TCP Connections");
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            foreach (TcpConnectionInformation c in connections)
            {
                Console.WriteLine(c.LocalEndPoint.ToString() + " <==> " + c.RemoteEndPoint.ToString());
            }
        }
        public void DisplayDnsConfiguration()
        {

            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine(adapter.Description);
                Console.WriteLine("  DNS suffix .............................. :" +
                    properties.DnsSuffix);
                Console.WriteLine("  DNS enabled ............................. : " +
                    properties.IsDnsEnabled);
                Console.WriteLine("  Dynamically configured DNS .............. : " +
                    properties.IsDynamicDnsEnabled);
            }

        }

        public void upnet2()
        {
            NetworkInterface[] ifaceList = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface iface in ifaceList)
            {
                if (iface.OperationalStatus == OperationalStatus.Up)
                {

                    Console.WriteLine("Namett: " + iface.Name);
                    Console.WriteLine("Typett: " + iface.NetworkInterfaceType);
                    Console.WriteLine("Statustt: " + iface.OperationalStatus);
                    Console.WriteLine("Speedtt: " + iface.Speed);
                    Console.WriteLine("Descriptiont: " + iface.Description);

                    UnicastIPAddressInformationCollection unicastIPC = iface.GetIPProperties().UnicastAddresses;
                    foreach (UnicastIPAddressInformation unicast in unicastIPC)
                    {
                        Console.WriteLine(unicast.Address.AddressFamily + "t: " + unicast.Address);
                    }
                    Console.WriteLine("=======================================");
                }
            }
        }



        public void upnet()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine(ni.Name);
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            Console.WriteLine(ip.Address.ToString());
                        }
                    }
                }
            }
        }








    }
}

using LazyBones.Properties;
using NLog;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyBones
{
    internal class Vpn
    {
        private static readonly string _vpnPath = new StringBuilder().Append(@"C:\Program Files\SoftEther VPN Client\")
                                                                     .Append(Environment.Is64BitOperatingSystem ? "vpncmd_x64" : "vpncmd")
                                                                     .Append(".exe").ToString();
        private static readonly string _vpnArg = new StringBuilder().Append("127.0.0.1 /CLIENT /IN:\"")
                    .Append(Path.GetDirectoryName(Application.ExecutablePath))
                    .Append(Path.DirectorySeparatorChar.ToString())
                    .Append("infile.txt\" /OUT:log.txt").ToString();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static bool _connected = false;
        public static bool Connected => _connected;

        static Vpn()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(AddressChangedCallback);
        }
        
        static void AddressChangedCallback(object sender, EventArgs e)
        {
                _connected = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(i => i.Name.Contains("VPN"))
                    .Select(o => o.OperationalStatus)
                    .FirstOrDefault() == OperationalStatus.Up;
        }

        private static void Execute(string[] lines)
        {
            try
            {
                File.WriteAllLines("infile.txt", lines);
                Process.Start(_vpnPath, _vpnArg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public static void Connect()
        {
            try
            {
                Totp totp = new Totp(Base32Encoding.ToBytes(Settings.Default.token));
                string temp = Settings.Default.remoteip;
                temp = temp.Replace(".", "");
                temp = temp.Substring(temp.Length - 4, 4);
                string[] lines =
                    {
                    $"AccountUsernameSet {Settings.Default.vpnname} /USERNAME:{Settings.Default.user}",
                    $"AccountPasswordSet {Settings.Default.vpnname} /PASSWORD:{temp}{totp.ComputeTotp()} /TYPE:radius",
                    $"AccountConnect {Settings.Default.vpnname}"
                    };
                Execute(lines);
                Logger.Info("Під'єднуємо VPN.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public static void Disconnect()
        {
            string[] lines =
            {
               $"AccountDisconnect {Settings.Default.vpnname}"
            };
            Execute(lines);
            Logger.Info("Від'єднуємо VPN.");
        }
    }
}

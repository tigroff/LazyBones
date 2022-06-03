using LazyBones.Properties;
using NLog;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LazyBones
{
    internal class Vpn
    {
        private static readonly string _vpnPath = @"C:\Program Files\SoftEther VPN Client\vpncmd_x64.exe";
        private static readonly string _vpnArg = new StringBuilder().Append("127.0.0.1 /CLIENT /IN:\"")
                    .Append(Path.GetDirectoryName(Application.ExecutablePath))
                    .Append(Path.DirectorySeparatorChar.ToString())
                    .Append("infile.txt\" /OUT:log.txt").ToString();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static void Execute(string[] lines)
        {
            try
            {
                File.WriteAllLines("infile.txt", lines);
                System.Diagnostics.Process.Start(_vpnPath, _vpnArg);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public static bool Connect()
        {
            try
            {
                Totp totp = new Totp(Base32Encoding.ToBytes(Settings.Default.token));
                string temp = Settings.Default.remoteip;
                temp = temp.Replace(".", "");
                temp = temp.Substring(temp.Length - 4, 4);
                string[] lines =
                    {
                    $"AccountUsernameSet Ukrtatnafta /USERNAME:{Settings.Default.user}",
                    $"AccountPasswordSet Ukrtatnafta /PASSWORD:{temp}{totp.ComputeTotp()} /TYPE:radius",
                    $"AccountConnect {Settings.Default.vpnname}"
                    };
                Execute(lines);
                Logger.Info("VPN під'єднано.");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return false;
            }
        }

        public static void Disconnect()
        {
            string[] lines =
            {
               $"AccountDisconnect {Settings.Default.vpnname}"
            };
            Execute(lines);
            Logger.Info("VPN від'єднано.");
        }


    }
}

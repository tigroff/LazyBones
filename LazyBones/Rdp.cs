using LazyBones.Properties;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LazyBones
{
    internal class Rdp
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static Random _rand = new Random();

        public static bool Connected
        {
            get { return Process.GetProcessesByName("mstsc").Any(); }
        }
        
        public static void Connect()
        {
            try
            {
                Thread.Sleep(_rand.Next(2000, 5000));  //Decimal.ToInt32(sleepTime.Value) * 1000);
                System.Diagnostics.Process.Start("mstsc.exe", Settings.Default.rdpPath);
                Logger.Info("RDP під'єднано.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
        public static void Disconnect()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("mstsc"))
                {
                    proc.Kill();
                    Logger.Info("RDP від'єднано.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

    }
}

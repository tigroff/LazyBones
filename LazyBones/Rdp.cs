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

        public static bool Connected
        {
            get { return Process.GetProcessesByName("mstsc").Any(); }
        }
        
        public static void Connect()
        {
            try
            {
                Process.Start("mstsc.exe", Settings.Default.rdpPath);
                Logger.Info("Під'єднуємо RDP.");
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
                    Logger.Info("Від'єднуємо RDP.");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

    }
}

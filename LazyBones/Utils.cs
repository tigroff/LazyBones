using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LazyBones
{
    internal class Utils
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static bool _firstPingLog = true;

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                _firstPingLog = true;
                return true;
            }
            catch
            {
                if (_firstPingLog)
                {
                    Logger.Warn("Немає інтернету.");
                    _firstPingLog = false;
                }
                return false;
            }
        }

        public static bool CheckIfVpnConnected()
        {
            try
            {
                Dns.GetHostEntry("softvpn.nafta.priv");
                _firstPingLog = true;
                return true;
            }
            catch
            {
                if (_firstPingLog)
                {
                    Logger.Warn("Немає зв'язку з робочою мережею.");
                    _firstPingLog = false;
                }
                return false;
            }
            
        
        }
    }
}

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
        public enum Connections
        { 
            Internet, SoftVpn
        }

        public static bool CheckForConnection(Connections sw)
        {
            string res = "Невідома помилка.";
            try
            {
                switch (sw)
                {
                    case Connections.Internet:
                        using (var client = new WebClient())
                        using (client.OpenRead("http://google.com/generate_204"))
                            res = "Немає інтернету.";
                        break;
                    case Connections.SoftVpn:
                        Dns.GetHostEntry("softvpn.nafta.priv");
                        res = "Немає зв'язку з робочою мережею.";
                        break;
                    default:
                        break;
                }

                _firstPingLog = true;
                return true;
            }
            catch
            {
                if (_firstPingLog)
                {
                    Logger.Warn(res);
                    _firstPingLog = false;
                }
                return false;
            }
        }
    }
}

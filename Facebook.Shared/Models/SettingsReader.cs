using Leaf.xNet;
using System.IO;
using System.Linq;

namespace Facebook.Shared.Models
{
    public static class SettingsReader
    {
        public static ProxyType ProxyType { get; private set; }

        public static void LoadSettings(string path)
        {
            var lines = File.ReadAllLines(path);

            var _proxyType = lines.First(x => x.StartsWith("proxyType")).Split('=').Last();
            switch (_proxyType)
            {
                case "http":
                    ProxyType = ProxyType.HTTP;
                    break;
                case "socks4":
                    ProxyType = ProxyType.Socks4;
                    break;
                case "socks5":
                    ProxyType = ProxyType.Socks5;
                    break;
                default:
                    ProxyType = ProxyType.Socks5;
                    break;
            }
        }
    }
}

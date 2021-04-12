using Leaf.xNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter.Core.Proxy
{
    public static class ProxyProvider
    {
        private static object _lock = new object();

        private static ProxyType _proxyType;
        private static List<ProxyClient> _proxyClients;
        private static int _index = 0;

        public static ProxyClient GetProxy()
        {
            lock (_lock)
            {
                _index++;
                if (_index >= _proxyClients.Count)
                    _index = 0;

                return _proxyClients[_index];
            }
        }

        public static void LoadProxy(string path, ProxyType proxyType)
        {
            _proxyType = proxyType;
            _proxyClients = DataLoader.GetProxies(path, _proxyType);
        }
    }
}

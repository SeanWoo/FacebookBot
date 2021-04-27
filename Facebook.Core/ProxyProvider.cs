using Facebook.Shared.Interfaces;
using Leaf.xNet;
using System.Collections.Generic;

namespace Facebook.Core
{
    public class ProxyProvider : IProxyProvider
    {
        private object _lock = new object();

        private IDataLoader _dataLoader;
        private ProxyType _proxyType;
        private List<ProxyClient> _proxyClients;
        private int _index = 0;

        public ProxyProvider(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public ProxyClient GetProxy()
        {
            lock (_lock)
            {
                _index++;
                if (_index >= _proxyClients.Count)
                    _index = 0;

                return _proxyClients[_index];
            }
        }

        public void LoadProxy(string path, ProxyType proxyType)
        {
            _proxyType = proxyType;
            _proxyClients = _dataLoader.GetProxies(path, _proxyType);
        }
    }
}

using Leaf.xNet;

namespace Facebook.Shared.Interfaces
{
    public interface IProxyProvider
    {
        ProxyClient GetProxy();
        void LoadProxy(string path, ProxyType proxyType);
    }
}

using Facebook.Shared.Models;
using Leaf.xNet;
using System.Collections.Generic;

namespace Facebook.Shared.Interfaces
{
    public interface IDataLoader
    {
        List<AccountData> GetAccountsList(string path);
        Queue<AccountData> GetAccountsQueue(string path);
        List<ProxyClient> GetProxies(string path, ProxyType proxyType);
    }
}

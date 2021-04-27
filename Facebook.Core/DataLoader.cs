using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using Leaf.xNet;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Facebook.Core
{
    public class DataLoader : IDataLoader
    {
        public List<AccountData> GetAccountsList(string path)
        {
            if (!File.Exists(path))
                return null;

            if (new FileInfo(path).Extension != ".json")
                return null;


            var text = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(text))
            {
                return new List<AccountData>();
            }

            var accounts = JsonConvert.DeserializeObject<List<AccountData>>(text).Where(x => x != null).ToList();
            for (int i = 0; i < accounts.Count; i++)
                accounts[i].Username = "Account " + i;

            return accounts;
        }
        public Queue<AccountData> GetAccountsQueue(string path)
        {
            var fileInfo = new FileInfo(path);
            if (fileInfo.Extension != ".json")
                return null;

            var text = File.ReadAllText(path);
            var accounts = JsonConvert.DeserializeObject<List<AccountData>>(text).Where(x => x != null).ToList();

            for (int i = 0; i < accounts.Count; i++)
                accounts[i].Username = "Account " + i;

            return new Queue<AccountData>(accounts);
        }

        public List<ProxyClient> GetProxies(string path, ProxyType proxyType)
        {
            var proxies = File.ReadAllLines(path).Select(x => {
                var proxySplit = x.Split();
                var client = ProxyClient.Parse(proxyType, proxySplit[0]);
                if(proxySplit.Length > 1)
                    client.Username = proxySplit[1];
                if (proxySplit.Length > 2)
                    client.Password = proxySplit[2];

                return client;
            });

            return proxies.ToList();
        }
    }
}

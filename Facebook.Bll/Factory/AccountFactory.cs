using DryIoc;
using Facebook.Shared.Interfaces;
using Facebook.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bll.Factory
{
    public class AccountFactory : IFactory<IAccount>
    {
        private IResolverContext _context;

        public AccountFactory(IResolverContext context)
        {
            _context = context;
        }

        public IEnumerable<IAccount> Get()
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<AccountData>>(File.ReadAllText(SharedData.PATH_TO_ACCOUNT));
            if (result is null)
                return null;
            if (!result.Any())
                return null;
            return result.Select(x => {
                var account = _context.Resolve<IAccount>();
                account.AccountData = x;
                return account;
            }).ToList();
        }
    }
}

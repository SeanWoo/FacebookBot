using DryIoc;
using Facebook.Bll;
using Facebook.Bll.Factory;
using Facebook.Core;
using Facebook.Shared.Interfaces;

namespace Facebook.CLI
{
    class Program
    {
        private static Container c = new Container();
        public static IResolverContext Resolver { get => c; }

        static void Main(string[] args)
        {
            Register();
            StartServices();
        }

        private static void Register()
        {
            c.Register<MessageListener>(Reuse.Singleton);

            c.Register<IProxyProvider, ProxyProvider>(Reuse.Singleton);
            c.Register<IDataLoader, DataLoader>(Reuse.Singleton);

            c.Register<IAccount, Account>(Reuse.Transient);

            c.Register<IFactory<IAccount>, AccountFactory>(Reuse.Singleton);
        }

        private static void StartServices()
        {
            c.Resolve<MessageListener>().Start();
        }
    }
}

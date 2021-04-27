using DryIoc;
using Facebook.Core;
using Facebook.DI.Interfaces;

namespace Facebook.DI
{
    public static class InitializeDI
    {
        private static Container _container;

        static InitializeDI()
        {
            _container.Register<IAccount, Account>
        }
    }
}

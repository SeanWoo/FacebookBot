using Facebook.Shared.Models;
using System.IO;

namespace Facebook.CLI
{
    public static class FileInitializer
    {
        public static void Initialize()
        {
            if (!File.Exists(SharedData.PATH_TO_ACCOUNT))
                File.Create(SharedData.PATH_TO_ACCOUNT).Close();

            if (!File.Exists(SharedData.PATH_TO_PROXY))
                File.Create(SharedData.PATH_TO_PROXY).Close();

            if (!File.Exists(SharedData.PATH_TO_SETTINGS))
            {
                var lines = new string[]
                {
                    "proxyType=http"
                };
                File.WriteAllLines(SharedData.PATH_TO_SETTINGS, lines);
            }
        }
    }
}

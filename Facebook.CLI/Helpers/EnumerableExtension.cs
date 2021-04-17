using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.CLI.Helpers
{
    public static class EnumerableExtension
    {
        private static Random _random = new Random();

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            var index = _random.Next(0, enumerable.Count());

            var counter = 0;
            foreach (var item in enumerable)
            {
                if (index == counter)
                    return item;
                counter++;
            }

            return default(T);
        }
    }
}

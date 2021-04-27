using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Shared.Models.Extensions
{
    public static class EnumerableExtension
    {
        private static Random rand = new Random();

        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            var index = rand.Next(0, enumerable.Count());
            var counter = 0;
            foreach (var item in enumerable)
            {
                if(counter == index)
                {
                    return item;
                }
                counter++;
            }
            return default(T);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Redbridge.IntegrationTesting
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> items)
            where T : class
        {
            var itemsArray = items.ToArray();
            var totalItems = itemsArray.Count();
            if (totalItems > 0)
            {
                var random = new Random(DateTime.UtcNow.Millisecond);
                var randomIndex = random.Next(0, totalItems - 1);
                return itemsArray.ElementAt(randomIndex);
            }

            throw new IntegrationTestException("Cannot choose a random item, no items supplied in the enumerable.");
        }

        public static T FirstOrDefaultRandom<T>(this IEnumerable<T> items)
            where T : class
        {
            if (items != null)
            {
                var totalItems = items.Count();
                if (totalItems > 0)
                {
                    var random = new Random(DateTime.UtcNow.Millisecond);
                    var randomIndex = random.Next(0, totalItems - 1);
                    return items.ElementAt(randomIndex);
                }
                return null;
            }
            return null;
        }
    }
}

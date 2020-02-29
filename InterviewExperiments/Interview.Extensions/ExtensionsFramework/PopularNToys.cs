using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionsFramework
{
    public class PopularNToys
    {
        // METHOD SIGNATURE BEGINS, THIS METHOD IS REQUIRED
        public List<string> popularNToys(int numToys,
            int topToys,
            List<string> toys,
            int numQuotes, List<string> quotes)
        {
            var result = new List<string>();

            // Create a dictionary of the toys.
            var toyCounts = new ConcurrentDictionary<string, int>();

            foreach (var toy in toys)
            {
                toyCounts.AddOrUpdate(toy, 0, (key, existingCount) => existingCount + 1);
            }

            // Go through the quotes to find the counts.
            foreach (var toy in quotes.SelectMany(quote => toys.Where(toy => quote.Contains(toy))))
            {
                toyCounts.AddOrUpdate(toy, 0, (key, existingCount) => existingCount + 1);
            }

            // Figure out the highest counts and return a list.
            foreach (var toyCount in toyCounts.OrderByDescending(v => v.Value))
            {
                if (result.Count < topToys)
                {
                    result.Add(toyCount.Key);
                }
            }

            return result;
        }
        // METHOD SIGNATURE ENDS
	}
}

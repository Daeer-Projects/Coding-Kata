using System;
using System.Threading.Tasks;

namespace DataMungingPartThree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello.");

            // To get the first component running and ensuring it works correctly, I am just creating things here.
            // ToDo: When we have more components, we need to bootstrap all of the construction.

            // We need to register the components (only one at the moment).
            // We need to set up the event hub.
            // We need to set up the logging for the core, components and program.

            // Just getting it to work for now.  Using the same method I did for part one and two.


            var boot = new Bootstrapper();
            var runningTasks = Task.Factory.StartNew(async () => await boot.ProcessItemsAsync().ConfigureAwait(false));

            Console.WriteLine($"The result is: {runningTasks.GetAwaiter().GetResult().GetAwaiter().GetResult().ProcessResult}.");

            Console.WriteLine("I hoped you enjoyed it!");

        }

    }
}

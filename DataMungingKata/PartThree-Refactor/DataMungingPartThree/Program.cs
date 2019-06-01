using System;
using System.Threading;

namespace DataMungingPartThreeV2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello.");
            Console.WriteLine("With this version, I am going to try and refactor the components into the core project more.");
            Console.WriteLine("Done most of the work now.  Just needs some summary blocks and instructions.");

            // To get the first component running and ensuring it works correctly, I am just creating things here.

            // We need to register the components (only one at the moment).
            // We need to set up the event hub.
            // We need to set up the logging for the core, components and program.

            var boot = new Bootstrapper();
            boot.ProcessItemsAsync();

            // Still need to ensure that everything is still running when the results come in.
            var completed = false;
            Console.WriteLine("Process Running...  Press 'q' to quit.");
            do
            {
                Thread.Sleep(10);
                var key = Console.ReadKey();
                if (key.KeyChar.Equals('q'))
                {
                    completed = true;
                }
            } while (!completed);

            Console.WriteLine();
            Console.WriteLine("I hoped you enjoyed it!");
        }
    }
}

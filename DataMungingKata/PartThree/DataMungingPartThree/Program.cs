using System;
using System.Threading;
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
            boot.ProcessItemsAsync();
            //var runningTasks = Task.Factory.StartNew(() => boot.ProcessItemsAsync());

            // Still need to ensure that everything is still running when the results come in.
            // There has to be a better way of waiting for all tasks and child tasks to complete.
            //runningTasks.GetAwaiter().GetResult();
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


            Console.WriteLine("I hoped you enjoyed it!");
        }
    }
}

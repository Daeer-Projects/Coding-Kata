using System;

namespace KarateChopKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = new int[4];
            var location = items.Chop(2);

            Console.WriteLine(location);
        }
    }
}

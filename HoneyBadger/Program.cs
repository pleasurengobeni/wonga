using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoneyBadger
{
    class Program
    {
        /// <summary>
        /// For the numbers 1 through 100, print "Honey" if the number is divisible by 4 and "Badger" if the
        /// number is divisble by 6. If the number is divisble by both 4 and 6, print "HoneyBadger". If the number
        /// is divisble by neither, print the number.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            for(int i = 1; i <= 100; i++)
            {
                if ((i % 4) == 0 && (i % 6) > 0)
                    Console.WriteLine("Honey");
                else if ((i % 6) == 0 && (i % 4) > 0)
                    Console.WriteLine("Badger");
                else if ((i % 6) == 0 && (i % 4) == 0)
                    Console.WriteLine("HoneyBadger");
                else
                    Console.WriteLine(i);

            }


            Console.ReadKey();
        }
    }
}

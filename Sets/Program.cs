using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    class Program
    {
        /// <summary>
        /// Given a finite set of unique numbers, find all the runs in the set. Runs are 1 or more consecutive numbers.
        // That is, given {1,59,12,43,4,58,5,13,46,3,6}, the output should be: {1}, {3,4,5,6}, {12,13}, {43}, {46},{58,59}.
        // Note that the size of the set may be very large.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var input = new[] {1, 59, 12, 43, 4, 58, 5, 13, 46, 3, 6};
            Array.Sort(input);
            string sets = "";

            for(int i = 0; i < input.Length; i++)
            {
                if (!sets.Contains("," + input[i].ToString() + ",") && !sets.Contains("," + input[i].ToString() + "}"))
                {
                    sets += "{" + input[i].ToString();

                    var value = input[i]+1;
                    bool found = false;

                    do
                    {
                        for (int j = 0; j < input.Length; j++)
                        {
                            if (value.Equals(input[j]))
                            {
                                sets += "," + value;
                                found = true;
                                value++;
                            }
                            else
                                found = false;
                        }
                    } while (found);

                    sets += "},";
                }
            }


            Console.WriteLine(sets);
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopN
{
    class Program
    {
        /// <summary>
        /// Write a program, topN, that given an arbitrarily large file and a number, N, containing individual numbers on each line (e.g. 200Gb file), 
        /// will output the largest N numbers, highest first. Tell me about the run time/space complexity of it, and whether you think there's room for improvement in your approach.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var top = TopN("InputFile.txt", 5);
            foreach (var i in top)
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }

        private static IList<int> TopN(string fileName, int top)
        {

            /*
             * in terms of time complexity, I believe I have used a good approach by using the orderByDescending function
             * than to implement my own comparison method, but for very large file it will take longer to do the sorting,
             * space complexity I feel like I have declared too many variables for storing the lists, somehow i feel it could be 
             * done better, just cant think of it now, but this will affect memory especially on larger input files.
             */

            List<int> list = new List<int>();

            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);

            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                try
                {
                    list.Add(int.Parse(line));
                }
                catch { }
            }


            var sorted = (list.OrderByDescending(x => x)                
                  .ToList()).Take(top);


            IList<int> returnList = new List<int>();
            foreach(int i in sorted)
            {
                returnList.Add(i);
            }

            return returnList;
        }
    }
}

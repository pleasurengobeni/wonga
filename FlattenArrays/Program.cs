using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlattenArrays
{
    class Program
    {
        /// <summary>
        /// Write a program that will take a nested array and flatten it
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var nestedArray = new[] { new object[] {1, 2, new[] { 3}, 5, 6}/*, new object[] {4}*/ };

            var flattenedArray = Flatten(nestedArray);

            foreach (var item in flattenedArray)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();

            /*
            Expected output: 
            1
            2
            3
            4
            5
            6
            */
        }

        private static object[] Flatten(object[][] nestedArray)
        {
            List<object> list = new List<object>();//list to store unknown number of elements

            for (int i = 0; i < nestedArray.GetLength(0); i++) //get lenght of parameter array
            {
                var obj = nestedArray[i];
                for (int j = 0; j < obj.GetLength(0); j++)
                {
                    if (obj[j].GetType().IsArray)
                    {
                        IEnumerable myObj = obj[j] as IEnumerable;
                        foreach (object o in myObj)
                        {
                            list.Add(o);
                        }
                    }
                    else
                    {
                        list.Add(obj[j]);
                    }
                }
            }

            list.Sort();
            return list.ToArray();
        }
    }
}


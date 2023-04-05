using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Linq;
using System.IO;
using sun.nio.cs.ext;

namespace Test_Wotking
{
    public class Solution
    {

        public static void Main(String[] args)
        {
            var n = Console.ReadLine();
            var X = Console.ReadLine().Split().Select(double.Parse).ToArray();
            var Y = Console.ReadLine().Split().Select(double.Parse).ToArray();

            Console.WriteLine(SpearmanRank(X, Y).ToString("f3"));
        }

        static double SpearmanRank(double[] X, double[] Y)
        {
            var xRank = GetRank(X);
            var YRank = GetRank(Y);

            var sum = 0.0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += Math.Pow(xRank[i] - YRank[i], 2);
            }

            return 1.0 - (6 * sum) / (X.Length * (Math.Pow(X.Length, 2) - 1));
        }

        private static double[] GetRank(double[] arr)
        {
            var length = arr.Length;
            var dictionary = new SortedDictionary<double, List<int>>();
            for (int i = 0; i < length; i++)
            {
                var current = arr[i];
                List<int> listWithIntegers;
                if (!dictionary.TryGetValue(current, out listWithIntegers))
                {
                    listWithIntegers = new List<int>();
                    dictionary.Add(current, listWithIntegers);
                }

                listWithIntegers.Add(i);
            }

            var ranks = new double[length];
            var counter = 1;
            foreach (var element in dictionary)
            {
                var list = element.Value;
                foreach (var number in list)
                {
                    ranks[number] = counter;
                }

                counter++;
            }

            return ranks;
        }
    }
}

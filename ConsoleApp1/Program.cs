using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var list = new List<string>();
            for (int i = 0; i < 100000000; i++)
            {
                list.Add(SageTools.Utils.ShortIdGen.NextId());
            }
            var list2 = list.Distinct().ToList();

            Console.WriteLine($"原有:{list.Count}，重复了{list.Count-list2.Count}");
            Console.ReadKey();
        }

      
    }
}

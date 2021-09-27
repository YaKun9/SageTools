using System;
using SageTools.Extension;

namespace SageTools.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {

           System.Console.WriteLine(DateTime.Now.AddDays(-1).EndOfDay());

           var time1 = new DateTime(DateTime.Now.Year, 1, 1);
           System.Console.WriteLine(time1.EndOfDay());
           System.Console.WriteLine(DateTime.Now.IsAfternoon());
           System.Console.WriteLine(DateTime.Now.IsMorning());
           System.Console.WriteLine(DateTime.Now.IsNow());

           System.Console.ReadKey();
        }
    }
}

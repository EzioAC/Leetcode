using System;
using Leetcode.MapSum;
namespace Leetcode
{
    class Program {
        static void Main (string[] ars) {
            MapSum.MapSum test = new MapSum.MapSum ();
            test.Insert("aa",3);            
            Console.WriteLine(test.Sum("a"));
            test.Insert("aa",2);
            Console.WriteLine(test.Sum("a"));
             Console.WriteLine(test.Sum("aa"));
             Console.WriteLine(test.Sum("aaa"));
        }
    }
}
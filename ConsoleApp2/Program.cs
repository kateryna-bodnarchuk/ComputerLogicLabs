using Lab1;
using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var perfectDisjunctionNormalForm = new UnitTest1.PerfectDisjunctionNormalForm(UnitTest1.GetOutputBool());

            uint input = uint.Parse(Console.ReadLine());
            bool result = perfectDisjunctionNormalForm.Execute(input);
            Console.WriteLine(result ? 1 : 0);
            Console.ReadKey();
        }
    }
}

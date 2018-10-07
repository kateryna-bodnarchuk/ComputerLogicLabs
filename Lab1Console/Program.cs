using LogicAlgebra.FunctionOptimization;
using Lab1;
using System;
using LogicAlgebra;
using LogicAlgebra.IntLogic;

namespace Lab1Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var perfectDisjunctionNormalForm = new PerfectDisjunctionNormalFormBinary(
                BitTools.GetOutputBool(KaterynaBodnarchukTask.KateBodnarchukCase));

            uint input = uint.Parse(Console.ReadLine());
            bool result = perfectDisjunctionNormalForm.Evaluate(input);
            Console.WriteLine(result ? 1 : 0);
            Console.ReadKey();
        }
    }
}

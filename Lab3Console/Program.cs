using LogicAlgebra;
using LogicAlgebra.Core;
using LogicAlgebra.FunctionOptimization;
using LogicAlgebra.IntLogic;
using System;
using System.Collections.Generic;

namespace Lab3Console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] output = BitTools.GetOutputBool(KaterynaBodnarchukTask.KateBodnarchukCase);
            List<Implicant> constituents = PositiveMcCluskeyMethod.GetConstituents(output);
            Console.WriteLine("Original Function: " + LogicConvert.ToOrFunction(constituents).GetFormulaString());

            ImplicantDisjunctionNormalForm minimalDisjunctionalNormalFunction = PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(output);

            OrFunction minimalDisjunctionNormaForm = LogicConvert.ToOrFunction(minimalDisjunctionalNormalFunction.Implicants);
            Console.WriteLine("Optimized Function: " + minimalDisjunctionNormaForm.GetFormulaString());

            uint input = uint.Parse(Console.ReadLine());
            Console.WriteLine("Output: " + minimalDisjunctionalNormalFunction.Evaluate(input));
            Console.ReadKey();
        }
    }
}

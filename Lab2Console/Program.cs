using LogicAlgebra.FunctionOptimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lab2Console
{
    class Program
    {
        static void Main(string[] args)
        {
            uint[] outputIntegers = BitTools.KateBodnarchukCase;
            bool[] output = BitTools.GetOutputBool(outputIntegers);
            List<Implicant> constituents = PositiveMcCluskeyMethod.GetConstituents(output);
            string constituentsDisjunctionFormString = 
                Implicant.GetDisjunctionFormString(constituents);
            Console.WriteLine("Original Function: " + constituentsDisjunctionFormString);

            var minimalDisjunctionalNormalFunction = 
                PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(output);

            Console.WriteLine("Optimized Function: " + minimalDisjunctionalNormalFunction.ToString());

            uint input = uint.Parse(Console.ReadLine());
            Console.WriteLine("Output: " + minimalDisjunctionalNormalFunction.Evaluate(input));
            Console.ReadKey();

        }
    }
}

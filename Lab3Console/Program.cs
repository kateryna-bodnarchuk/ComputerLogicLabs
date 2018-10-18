using LogicAlgebra;
using LogicAlgebra.Core;
using LogicAlgebra.FunctionOptimization;
using LogicAlgebra.IntLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3Console
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] output = BitTools.GetOutputBool(KaterynaBodnarchukTask.KateBodnarchukCase);

            PrintWithTitle(
                "Original Function", LogicConvert.ToOrFunction(PositiveMcCluskeyMethod.GetConstituents(output)));

            IList<Implicant> minimalDisjunctionalNormalForm = 
                PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(output);

            PrintWithTitle(
                "Optimized optimized Function", LogicConvert.ToOrFunction(minimalDisjunctionalNormalForm));

            bool[] outputInversed = output.Select(i => !i).ToArray();
            PrintWithTitle(
                "Original Inversed Function",
                LogicConvert.ToOrFunction(PositiveMcCluskeyMethod.GetConstituents(outputInversed)));

            IBooleanFunction minimalDisjunctionalNormalFormInversed =
                LogicConvert.ToOrFunction(PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(outputInversed));
            PrintWithTitle(
                "Optimized optimized Inversed Function",
                minimalDisjunctionalNormalFormInversed);

            IBooleanFunction minimalDisjunctionalNormalFormDobleInversed = new NotFunction(minimalDisjunctionalNormalFormInversed);
            PrintWithTitle(
                "Optimized optimized Inversed Function Inversed",
                minimalDisjunctionalNormalFormDobleInversed);

            uint input = uint.Parse(Console.ReadLine());
            Console.WriteLine("Output: " + ImplicantDisjunctionNormalForm.Evaluate(minimalDisjunctionalNormalForm, input));
            Console.ReadKey();
        }

        static void PrintWithTitle(string title, IBooleanFunction function)
        {
            Console.WriteLine(title + ": " + function.GetFormulaString(UniversityTeacherFormatting.Instance));
        }

        class UniversityTeacherFormatting : IFunctionFormatting
        {
            public static UniversityTeacherFormatting Instance = new UniversityTeacherFormatting();

            public bool InverseBlockOrder => true;

            public string InputToString(int index) => "x" + (4 - index).ToString();
        }
    }
}

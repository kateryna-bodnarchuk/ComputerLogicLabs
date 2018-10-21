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

            PrintWithTitle("Original Function", LogicConvert.ToOrFunction(PositiveMcCluskeyMethod.GetConstituents(output)));

            IList<Implicant> minimalDisjunctionalNormalForm = 
                PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(output);

            PrintWithTitle("Optimized Function", LogicConvert.ToOrFunction(minimalDisjunctionalNormalForm));
            Console.WriteLine();

            bool[] outputInversed = output.Select(i => !i).ToArray();
            PrintWithTitle(
                "Original Inversed Function",
                LogicConvert.ToOrFunction(PositiveMcCluskeyMethod.GetConstituents(outputInversed)));

            IBooleanFunction minimalDisjunctionalNormalFormInversed =
                LogicConvert.ToOrFunction(PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(outputInversed));
            PrintWithTitle("Optimized Inversed Function", minimalDisjunctionalNormalFormInversed);
            Console.WriteLine();

            IBooleanFunction minimalDisjunctionalNormalFormDobleInversed = new NotFunction(minimalDisjunctionalNormalFormInversed);
            PrintWithTitle("Optimized Inversed Function Inversed", minimalDisjunctionalNormalFormDobleInversed);
            Console.WriteLine();

            IBooleanFunction andOfNotAndForm = RecursiveTransform.ExecuteRecursive(
                minimalDisjunctionalNormalFormDobleInversed, DeMorgansLaw.TryConvertNotOfOrToAndOfNot);
            PrintWithTitle("Not of not and", andOfNotAndForm);

            IBooleanFunction andOfOrForm = RecursiveTransform.ExecuteRecursive(
                andOfNotAndForm, DeMorgansLaw.TryConvertNotOfAndToOrOfNot);
            PrintWithTitle("And of or", andOfOrForm);

            IBooleanFunction doubleNotOfAndOfOrForm = NotFunction.DobleNot(andOfOrForm);
            IBooleanFunction notOfOrNotOfOrForm = RecursiveTransform.ExecuteRecursive(
                doubleNotOfAndOfOrForm, DeMorgansLaw.TryConvertNotOfAndToOrOfNot);
            PrintWithTitle("NotOfOr/NotOfOr", notOfOrNotOfOrForm);

            uint input = uint.Parse(Console.ReadLine());
            Console.WriteLine("Output: " + notOfOrNotOfOrForm.Evaluate(new UInt32EvaluationContext(input)));
            Console.ReadKey();
        }

        static void PrintWithTitle(string title, IBooleanFunction function)
        {
            Console.WriteLine(title + ": " + function.GetFormulaString(UniversityTeacherFormatting.Instance));
        }
    }
}

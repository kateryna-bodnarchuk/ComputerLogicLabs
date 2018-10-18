using LogicAlgebra;
using LogicAlgebra.FunctionOptimization;
using LogicAlgebra.IntLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lab2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            uint[] outputIntegers = KaterynaBodnarchukTask.YouTubeCase;
            bool[] output = BitTools.GetOutputBool(outputIntegers);
            Func<uint, bool> perfectDisjunctionNormalFunction =
                new PerfectDisjunctionNormalFormBinary(output).Evaluate;
            Func<uint, bool> minimalDisjunctionalNormalFunction =
                value => ImplicantDisjunctionNormalForm.Evaluate(
                    PositiveMcCluskeyMethod.GetImplicantDisjunctionNormalForm(output),
                    value
                );

            for (uint i = 0; i < BitTools.rowsCount; i++)
            {
                bool actual = output[i];
                Assert.AreEqual(actual, perfectDisjunctionNormalFunction(i));
                Assert.AreEqual(actual, minimalDisjunctionalNormalFunction(i));
            }
        }
    }
}

using LogicAlgebra.FunctionOptimization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lab1
{
    [TestClass] 
    public class UnitTest1
    {
        private static bool[,] GetTrueTable(bool[] outputs)
        {
            bool[,] data = new bool[BitTools.rowsCount, 5];
            if (outputs.Length != BitTools.rowsCount) throw new ArgumentException($"Outputs should have {BitTools.rowsCount} numbers.");

            for (uint row = 0; row < BitTools.rowsCount; row++)
            {
                for (int bitIndex = 0; bitIndex < BitTools.bitsCount; bitIndex++)
                {
                    bool rowBit = GetBit(row, bitIndex);
                    int column = BitTools.bitsCount - bitIndex - 1;
                    data[row, column] = rowBit;
                }
                data[row, 4] = outputs[row];
            }
            return data;
        }

        static bool GetBit(uint input, int bitIndex)
        {
            uint mask = (uint)1 << bitIndex;
            uint rowBitInt = input & mask;
            bool rowBit = rowBitInt > 0;
            return rowBit;
        }

        [TestMethod]
        public void TestTable()
        {
            bool[] outputsBool = BitTools.GetOutputBool(BitTools.KateBodnarchukCase);
            bool[,] tfable = GetTrueTable(outputsBool);
        }

        [TestMethod]
        public void TestPerfectDisjunctionNormalForm()
        {
            bool[] outputsBool = BitTools.GetOutputBool(BitTools.KateBodnarchukCase);
            var perfectDisjunctionNormalForm = new PerfectDisjunctionNormalFormBinary(outputsBool);
            var perfectConjunctionNormalForm = new PerfectConjunctionNormalFormBinary(outputsBool);
            for (uint input = 0; input < BitTools.rowsCount; input++)
            {
                bool expected = outputsBool[input];
                bool actualPerfectDisjunctionNormalForm = perfectDisjunctionNormalForm.Evaluate(input);
                bool actualPerfectConjunctionNormalForm = perfectConjunctionNormalForm.Evaluate(input);
                Assert.AreEqual(expected, actualPerfectDisjunctionNormalForm);
                Assert.AreEqual(expected, actualPerfectConjunctionNormalForm);
            }
        }
    }
}

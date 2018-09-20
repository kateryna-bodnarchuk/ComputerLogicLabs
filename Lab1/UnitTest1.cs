using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Lab1
{
    [TestClass] 
    public class UnitTest1
    {
        const int rowsCount = 16;
        const int bitsCount = 4;

        private static bool[,] GetTrueTable(bool[] outputs)
        {
            bool[,] data = new bool[rowsCount, 5];
            if (outputs.Length != rowsCount) throw new ArgumentException($"Outputs should have {rowsCount} numbers.");

            for (uint row = 0; row < rowsCount; row++)
            {
                for (int bitIndex = 0; bitIndex < bitsCount; bitIndex++)
                {
                    bool rowBit = GetBit(row, bitIndex);
                    int column = bitsCount - bitIndex - 1;
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

        public class PerfectDisjunctionNormalForm
        {
            private readonly List<uint> trueNumbers;
            public PerfectDisjunctionNormalForm(bool[] outputs)
            {
                trueNumbers = new List<uint>();
                for (uint i = 0; i < outputs.Length; i++)
                {
                    if (outputs[i] == true)
                    {
                        trueNumbers.Add(i);
                    }
                }
            }

            public bool Execute(uint input)
            {
                foreach (var conjunctionBlock in trueNumbers)
                {
                    if (ConjunctionBlock(conjunctionBlock, input))
                    {
                        return true;
                    }
                }
                return false;
            }

            private bool ConjunctionBlock(uint conjunctionBlock, uint input)
            {
                for (int bitIndex = 0; bitIndex < bitsCount; bitIndex++)
                {
                    bool inputBit = GetBit(input, bitIndex);
                    bool inverseBit = GetBit(conjunctionBlock, bitIndex);
                    bool inputBitAdjusted = inverseBit ? inputBit : !inputBit;
                    if (!inputBitAdjusted)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public class PerfectConjunctionNormalForm
        {
            private readonly List<uint> falseNumbers;
            public PerfectConjunctionNormalForm(bool[] outputs)
            {
                falseNumbers = new List<uint>();
                for (uint i = 0; i < outputs.Length; i++)
                {
                    if (outputs[i] == false)
                    {
                        falseNumbers.Add(i);
                    }
                }
            }
            public bool Execute(uint input)
            {
                foreach (var disjunctionBlock in falseNumbers)
                {
                    if (!DisjunctionBlock(disjunctionBlock, input))
                    {
                        return false;
                    }
                }
                return true;
            }

            private bool DisjunctionBlock(uint conjunctionBlock, uint input)
            {
                for (int bitIndex = 0; bitIndex < bitsCount; bitIndex++)
                {
                    bool inputBit = GetBit(input, bitIndex);
                    bool inverseBit = GetBit(conjunctionBlock, bitIndex);
                    bool inputBitAdjusted = inverseBit ? !inputBit : inputBit;
                    if (inputBitAdjusted)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static bool[] GetOutputBool()
        {
            uint[] outputsInt = new uint[] { 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0 };

            var outputsBool = new bool[outputsInt.Length];
            for (int i = 0; i < outputsInt.Length; i++)
            {
                outputsBool[i] = outputsInt[i] > 0;
            }
            return outputsBool;
        }

        [TestMethod]
        public void TestTable()
        {
            bool[] outputsBool = GetOutputBool();
            bool[,] tfable = GetTrueTable(outputsBool);
        }

        [TestMethod]
        public void TestPerfectDisjunctionNormalForm()
        {
            bool[] outputsBool = GetOutputBool();
            var perfectDisjunctionNormalForm = new PerfectDisjunctionNormalForm(outputsBool);
            var perfectConjunctionNormalForm = new PerfectConjunctionNormalForm(outputsBool);
            for (uint input = 0; input < rowsCount; input++)
            {
                bool expected = outputsBool[input];
                bool actualPerfectDisjunctionNormalForm = perfectDisjunctionNormalForm.Execute(input);
                bool actualPerfectConjunctionNormalForm = perfectConjunctionNormalForm.Execute(input);
                Assert.AreEqual(expected, actualPerfectDisjunctionNormalForm);
                Assert.AreEqual(expected, actualPerfectConjunctionNormalForm);
            }
        }
    }
}

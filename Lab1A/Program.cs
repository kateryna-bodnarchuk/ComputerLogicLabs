using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1A
{
    class Program
    {
        const int bitsCount = 4;

        static void Main(string[] args)
        {
            var outputsInt = new int[] {1,1,0,0,0,0,0,1,0,1,0,1,1,1,0,0 };
            var outputsBool = new bool[outputsInt.Length];
            for (int i = 0; i < outputsInt.Length; i++)
            {
                outputsBool[i] = outputsInt[i] > 0;
            }
            bool[,] table = GetTrueTable(outputsBool);

        }

        private static bool GetBit(int value, int bitIndex)
        {
            int mask = 1 << bitIndex;
            int rowBitInt = value & mask;
            bool rowBit = rowBitInt > 0;
            return rowBit;
        }

        private static bool[,] GetTrueTable(bool[] outputs)
        {
            const int rowsCount = 16;
            
            bool[,] data = new bool[rowsCount, 5];
            if (outputs.Length != rowsCount) throw new ArgumentException($"Outputs should have {rowsCount} numbers.");

            for (int row = 0; row < rowsCount; row++)
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

        private static BoolLogicFunction GetPerfectDisjunctionNormalForm(bool[] outputs)
        {
            return new PerfectDisjunctionNormalForm(outputs).Execute;
        }

        delegate bool BoolLogicFunction(int value);

        class PerfectDisjunctionNormalForm
        {
            private readonly List<int> trueNumbers;
            public PerfectDisjunctionNormalForm(bool[] outputs)
            {
                this.trueNumbers = new List<int>();
                for (int i = 0; i < outputs.Length; i++)
                {
                    if (outputs[i] == true)
                    {
                        this.trueNumbers.Add(i);
                    }
                }
            }

            public bool Execute(int value)
            {
                foreach (var conjunctionBlock in trueNumbers)
                {
                    for (int bitIndex = 0; bitIndex < bitsCount; bitIndex++)
                    {
                        bool valueBit = GetBit(value, bitIndex);
                        bool inversBit = GetBit(conjunctionBlock, bitIndex);
                        bool valueBitAdjusted = inversBit ? valueBit : !valueBit;
                        if (!valueBitAdjusted)
                        {
                            break;
                        }
                    }
                    return true;
                }

                return false;
            }
        }
    }
}

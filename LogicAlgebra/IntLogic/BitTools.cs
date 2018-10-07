using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.IntLogic
{
    public static class BitTools
    {
        public const int rowsCount = 16;
        public const int bitsCount = 4;

        public static bool GetBit(uint input, int bitIndex)
        {
            uint mask = (uint)1 << bitIndex;
            uint rowBitInt = input & mask;
            bool rowBit = rowBitInt > 0;
            return rowBit;
        }

        public static bool[] GetOutputBool(uint[] outputsInt)
        {
            var outputsBool = new bool[outputsInt.Length];
            for (int i = 0; i < outputsInt.Length; i++)
            {
                outputsBool[i] = outputsInt[i] > 0;
            }
            return outputsBool;
        }
    }
}

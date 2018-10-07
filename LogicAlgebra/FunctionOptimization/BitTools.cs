using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.FunctionOptimization
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

        public static uint[] KateBodnarchukCase = new uint[] { 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0 };
        /// <summary>
        /// https://www.youtube.com/watch?v=bcGRAcv1_64
        /// </summary>
        public static uint[] YouTubeCase = new uint[] { 0, 0, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1 };

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

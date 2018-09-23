using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionOptimization
{
    public class PerfectDisjunctionNormalFormBinary
    {
        private readonly List<uint> trueNumbers;
        public PerfectDisjunctionNormalFormBinary(bool[] outputs)
        {
            trueNumbers = GetTrueNumbers(outputs);
        }

        public static List<uint> GetTrueNumbers(bool[] outputs)
        {
            var trueNumbers = new List<uint>();
            for (uint i = 0; i < outputs.Length; i++)
            {
                if (outputs[i] == true)
                {
                    trueNumbers.Add(i);
                }
            }
            return trueNumbers;
        }

        public bool Evaluate(uint input)
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
            for (int bitIndex = 0; bitIndex < BitTools.bitsCount; bitIndex++)
            {
                bool inputBit = BitTools.GetBit(input, bitIndex);
                bool inverseBit = BitTools.GetBit(conjunctionBlock, bitIndex);
                bool inputBitAdjusted = inverseBit ? inputBit : !inputBit;
                if (!inputBitAdjusted)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

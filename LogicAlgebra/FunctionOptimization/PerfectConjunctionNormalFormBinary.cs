using LogicAlgebra.IntLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.FunctionOptimization
{
    public class PerfectConjunctionNormalFormBinary
    {
        private readonly List<uint> falseNumbers;
        public PerfectConjunctionNormalFormBinary(bool[] outputs)
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
        public bool Evaluate(uint input)
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
            for (int bitIndex = 0; bitIndex < BitTools.bitsCount; bitIndex++)
            {
                bool inputBit = BitTools.GetBit(input, bitIndex);
                bool inverseBit = BitTools.GetBit(conjunctionBlock, bitIndex);
                bool inputBitAdjusted = inverseBit ? !inputBit : inputBit;
                if (inputBitAdjusted)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

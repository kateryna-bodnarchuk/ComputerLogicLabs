using LogicAlgebra.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.IntLogic
{
    public sealed class UInt32EvaluationContext : IEvaluationContext
    {
        private readonly uint input;

        public UInt32EvaluationContext(uint input)
        {
            this.input = input;
        }

        public bool GetInput(int index) => BitTools.GetBit(input, index);
    }
}

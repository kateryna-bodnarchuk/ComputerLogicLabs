using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString()}")]
    public sealed class NotFunction : IBooleanFunction
    {
        public NotFunction(IBooleanFunction argument)
        {
            this.Argument = argument;
        }

        public IBooleanFunction Argument { get; }

        public bool Evaluate(IEvaluationContext context)
        {
            bool argumentValue = Argument.Evaluate(context);
            return !argumentValue;
        }

        public string GetFormulaString() => "!(" + Argument.GetFormulaString() + ")";
    }
}

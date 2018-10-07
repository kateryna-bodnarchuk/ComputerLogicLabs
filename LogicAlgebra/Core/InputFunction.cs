using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString()}")]
    public sealed class InputFunction : IBooleanFunction
    {
        public InputFunction(int index) { Index = index; }
        public int Index { get; }

        public bool Evaluate(IEvaluationContext context) => context.GetInput(Index);

        public string GetFormulaString() => "x" + Index.ToString();
    }
}

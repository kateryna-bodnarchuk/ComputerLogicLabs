using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString(null)}")]
    public sealed class InputFunction : IBooleanFunction
    {
        public InputFunction(int index) { Index = index; }
        public int Index { get; }

        /// <summary>
        /// Gets if function is input function or inversed input.
        /// </summary>
        public static bool TestInputInversable(IBooleanFunction function)
        {
            switch (function)
            {
                case InputFunction input: return true;
                case NotFunction not: return TestInputInversable(not.Argument);
                default: return false;
            }
        }

        public bool Evaluate(IEvaluationContext context) => context.GetInput(Index);

        public string GetFormulaString(IFunctionFormatting formatting) 
            => formatting == null ? Index.ToString() : formatting.InputToString(Index);
    }
}

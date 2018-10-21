using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    public sealed class FunctionFormattingDefault : IFunctionFormatting
    {
        public static FunctionFormattingDefault Instance = new FunctionFormattingDefault();

        public string InputToString(int index) => index.ToString();

        public IEnumerable<IBooleanFunction> OrderItems(IEnumerable<IBooleanFunction> items)
            => items;
    }
}

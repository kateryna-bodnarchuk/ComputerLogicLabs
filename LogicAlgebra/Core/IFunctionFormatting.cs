using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    public interface IFunctionFormatting
    {
        IEnumerable<IBooleanFunction> OrderItems(IEnumerable<IBooleanFunction> items);
        string InputToString(int index);
    }
}

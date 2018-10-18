using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    public interface IFunctionFormatting
    {
        bool InverseBlockOrder { get; }
        string InputToString(int index);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    class InputFunctionEqualityComparer : IEqualityComparer<InputFunction>
    {
        public static readonly InputFunctionEqualityComparer Instance = 
            new InputFunctionEqualityComparer();

        public bool Equals(InputFunction x, InputFunction y)
        {
            if (x is null) return y is null;
            else
            {
                if (y is null) return false;
                else return x.Index == y.Index;
            }
        }

        public int GetHashCode(InputFunction obj) => 0;
    }
}

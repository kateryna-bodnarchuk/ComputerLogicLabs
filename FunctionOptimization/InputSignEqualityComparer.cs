using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FunctionOptimization
{
    public sealed class InputSignEqualityComparer : IEqualityComparer<InputSign>
    {
        public bool Equals(InputSign x, InputSign y)
        {
            return x.Index == y.Index && x.IsInversed == y.IsInversed;
        }

        public int GetHashCode(InputSign obj)
        {
            return obj.Index.GetHashCode() ^ obj.IsInversed.GetHashCode();
        }
    }
}

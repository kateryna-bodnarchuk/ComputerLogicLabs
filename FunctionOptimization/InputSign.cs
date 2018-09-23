using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionOptimization
{
    public class InputSign
    {
        public InputSign(int index, bool isInversed)
        {
            Index = index;
            IsInversed = isInversed;
        }

        public int Index { get; }
        public bool IsInversed { get; }
    }
}

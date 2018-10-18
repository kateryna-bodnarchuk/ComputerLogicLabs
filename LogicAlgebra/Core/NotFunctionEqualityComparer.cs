using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    class NotFunctionEqualityComparer : IEqualityComparer<NotFunction>
    {
        public static readonly NotFunctionEqualityComparer Instande =
            new NotFunctionEqualityComparer();
             
        public bool Equals(NotFunction x, NotFunction y)
        {
            if (x is null) return y is null;
            else
            {
                if (y is null) return false;
                else return BooleanFunctionEqualityComparer.Instance.Equals(
                    x.Argument, y.Argument);
            }
        }

        public int GetHashCode(NotFunction obj) => 0;
    }
}

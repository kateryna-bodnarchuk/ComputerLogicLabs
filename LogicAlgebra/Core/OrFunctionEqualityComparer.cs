using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    class OrFunctionEqualityComparer : IEqualityComparer<OrFunction>
    {
        public static readonly OrFunctionEqualityComparer Instance =
            new OrFunctionEqualityComparer();

        public bool Equals(OrFunction x, OrFunction y)
        {
            if (x is null)
            {
                return y is null;
            }
            else
            {
                if (y is null)
                {
                    return false;
                }
                else
                {
                    return x.Items.SequenceEqual(
                        y.Items, BooleanFunctionEqualityComparer.Instance);
                }
            }
        }

        public int GetHashCode(OrFunction obj) => 0;
    }
}

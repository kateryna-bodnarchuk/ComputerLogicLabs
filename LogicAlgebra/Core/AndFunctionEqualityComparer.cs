using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    class AndFunctionEqualityComparer : IEqualityComparer<AndFunction>
    {
        public static readonly AndFunctionEqualityComparer Instance =
            new AndFunctionEqualityComparer();

        public bool Equals(AndFunction x, AndFunction y)
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

        public int GetHashCode(AndFunction obj) => 0;
    }
}

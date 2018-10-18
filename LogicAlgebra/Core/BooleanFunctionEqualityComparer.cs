using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    public class BooleanFunctionEqualityComparer : IEqualityComparer<IBooleanFunction>
    {
        public static readonly BooleanFunctionEqualityComparer Instance =
            new BooleanFunctionEqualityComparer();

        public bool Equals(IBooleanFunction x, IBooleanFunction y)
        {
            switch (y)
            {
                case InputFunction i:
                    return new InputFunctionEqualityComparer().Equals(
                        i, y as InputFunction);
                case NotFunction i:
                    return NotFunctionEqualityComparer.Instande.Equals(
                        i, x as NotFunction);
                case AndFunction i:
                    return AndFunctionEqualityComparer.Instance.Equals(
                        i, y as AndFunction);
                case OrFunction i:
                    return OrFunctionEqualityComparer.Instance.Equals(
                        i, x as OrFunction);
                default: throw new NotSupportedException();
            }
        }

        public int GetHashCode(IBooleanFunction obj) => 0;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    public sealed class UniversityTeacherFormatting : IFunctionFormatting
    {
        public static UniversityTeacherFormatting Instance = new UniversityTeacherFormatting();

        private UniversityTeacherFormatting() { }

        public IEnumerable<IBooleanFunction> OrderItems(IEnumerable<IBooleanFunction> items)
            => items.All(InputFunction.TestInputInversable) ? items.Reverse() : items;

        public string InputToString(int index) => "x" + (4 - index).ToString();
    }
}

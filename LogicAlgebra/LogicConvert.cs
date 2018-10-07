using LogicAlgebra.Core;
using LogicAlgebra.FunctionOptimization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra
{
    public static class LogicConvert
    {
        public static OrFunction ToOrFunction(IEnumerable<Implicant> implicants)
        {
            return new OrFunction(
                arguments: implicants.Select(ToAndFunciton));
        }

        private static AndFunction ToAndFunciton(Implicant implicant)
            => new AndFunction(
                implicant.Items.Select(GetInputSignFunction));

        private static IBooleanFunction GetInputSignFunction(InputSign inputSign)
        {
            IBooleanFunction inputFunction = new InputFunction(inputSign.Index);
            if (inputSign.IsInversed)
            {
                inputFunction = new NotFunction(inputFunction);
            }

            return inputFunction;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    /// <summary>
    /// De Morgan's laws
    /// https://en.wikipedia.org/wiki/De_Morgan%27s_laws
    /// </summary>
    public static class DeMorgansLaw
    {
        public static bool TryConvertOrToAnd(IBooleanFunction boolFunction, out IBooleanFunction result)
        {
            result = default(IBooleanFunction);

            bool isNotRoot = false;
            if (boolFunction is NotFunction)
            {
                isNotRoot = true;
                boolFunction = ((NotFunction)boolFunction).Argument;
            }
            
            if (boolFunction is OrFunction)
            {
                var orFunction = (OrFunction)boolFunction;
                var conjunctionOfInversions = new AndFunction(
                    orFunction.Arguments.Select(i => new NotFunction(i)));
                result = isNotRoot ? conjunctionOfInversions : (IBooleanFunction)new NotFunction(conjunctionOfInversions);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool TryConvertAndToOr(IBooleanFunction boolFunction, out IBooleanFunction result)
        {
            result = default(IBooleanFunction);

            bool isNotRoot = false;
            if (boolFunction is NotFunction)
            {
                isNotRoot = true;
                boolFunction = ((NotFunction)boolFunction).Argument;
            }

            if (boolFunction is OrFunction)
            {
                var orFunction = (OrFunction)boolFunction;
                var conjunctionOfInversions = new AndFunction(
                    orFunction.Arguments.Select(i => new NotFunction(i)));
                result = isNotRoot ? orFunction : (IBooleanFunction)new NotFunction(orFunction);
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace LogicAlgebra.Core
{
    public static class RecursiveTransform
    {
        public static IBooleanFunction ExecuteRecursive(IBooleanFunction booleanFunction, TryTransformHandler tryTransform)
        {
            switch (booleanFunction)
            {
                case InputFunction inputFunction: return inputFunction;
                case NotFunction notFunction:
                    var argumentTransformed = ExecuteRecursive(notFunction.Argument, tryTransform);
                    var internals = NotFunction.NotOptimized(argumentTransformed);
                    if (tryTransform(internals, out IBooleanFunction internalsFullyTransformed))
                    {
                        return internalsFullyTransformed;
                    }
                    else return internals;
                case AndFunction andFunction:
                    var andItemsTransformed = new List<IBooleanFunction>();
                    foreach (var item in andFunction.Items)
                    {
                        var itemTransformed = ExecuteRecursive(item, tryTransform);
                        andItemsTransformed.Add(itemTransformed);
                    }
                    return new AndFunction(andItemsTransformed);
                case OrFunction orFunction:
                    var orItemsTransformed = new List<IBooleanFunction>();
                    foreach (var item in orFunction.Items)
                    {
                        var itemTransformed = ExecuteRecursive(item, tryTransform);
                        orItemsTransformed.Add(itemTransformed);
                    }
                    return new OrFunction(orItemsTransformed);
                default: throw new NotSupportedException();
            }
        }
    }
}

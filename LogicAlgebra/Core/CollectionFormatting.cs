using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    public static class CollectionFormatting
    {
        public static IEnumerable<string> GetCollectionItemsPrepared(
            IEnumerable<IBooleanFunction> items,
            IFunctionFormatting formatting = null)
        {
            if (formatting == null)
            {
                formatting = FunctionFormattingDefault.Instance;
            }

            Func<IBooleanFunction, string> embrase = GetItemFormatter(items, formatting);

            var itemsOrdered = formatting == null ? items : formatting.OrderItems(items);

            foreach (IBooleanFunction item in itemsOrdered)
            {
                yield return embrase(item);
            }
        }

        public static Func<IBooleanFunction, string> GetItemFormatter(
            IEnumerable<IBooleanFunction> items,
            IFunctionFormatting formatting)
        {
            if (items.All(InputFunction.TestInputInversable))
            {
                return s => s.GetFormulaString(formatting);
            }
            else 
            {
                return delegate (IBooleanFunction item)
                {
                    string itemString = item.GetFormulaString(formatting);
                    if (item is NotFiniteNumberException) return itemString;
                    else return $"({itemString})";
                };
            }
        }
    }
}

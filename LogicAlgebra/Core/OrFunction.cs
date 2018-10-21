using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString(null)}")]
    public sealed class OrFunction : IBooleanFunction
    {
        public OrFunction(IEnumerable<IBooleanFunction> items)
        {
            this.Items = new ReadOnlyCollection<IBooleanFunction>(items.ToArray());
        }

        public OrFunction(params IBooleanFunction[] items) : this(items.AsEnumerable()) { }

        public IReadOnlyCollection<IBooleanFunction> Items { get; }

        public bool Evaluate(IEvaluationContext context)
        {
            foreach (IBooleanFunction item in Items.Reverse())
            {
                bool itemValue = item.Evaluate(context);
                if (itemValue == true)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetFormulaString(IFunctionFormatting formatting = null)
        {
            return string.Join(" v ", CollectionFormatting.GetCollectionItemsPrepared(Items, formatting));
        }
    }
}

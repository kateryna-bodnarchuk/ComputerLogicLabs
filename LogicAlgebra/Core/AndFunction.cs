using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString(null)}")]
    public sealed class AndFunction : IBooleanFunction
    {
        public AndFunction(IEnumerable<IBooleanFunction> items)
        {
            foreach (var item in items)
            {
                if (item == null) throw new ArgumentException();
            }
            this.Items = new ReadOnlyCollection<IBooleanFunction>(items.ToArray());
        }

        public AndFunction(params IBooleanFunction[] items) : this(items.AsEnumerable()) { }

        public IReadOnlyCollection<IBooleanFunction> Items { get; }

        public bool Evaluate(IEvaluationContext context)
        {
            foreach (IBooleanFunction item in Items)
            {
                bool itemValue = item.Evaluate(context);
                if (itemValue == false)
                {
                    return false;
                }
            }

            return true;
        }

        public string GetFormulaString(IFunctionFormatting formatting = null)
        {
            return string.Join(" ^ ", CollectionFormatting.GetCollectionItemsPrepared(Items, formatting));
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Linq;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString()}")]
    public sealed class OrFunction : IBooleanFunction
    {
        public OrFunction(IEnumerable<IBooleanFunction> arguments)
        {
            this.Arguments = new ReadOnlyCollection<IBooleanFunction>(arguments.ToArray());
        }

        public IReadOnlyCollection<IBooleanFunction> Arguments { get; }

        public bool Evaluate(IEvaluationContext context)
        {
            foreach (IBooleanFunction item in Arguments)
            {
                bool itemValue = item.Evaluate(context);
                if (itemValue == true)
                {
                    return true;
                }
            }

            return false;
        }

        public string GetFormulaString()
        {
            List<string> itemsInBrases = new List<string>();
            foreach (IBooleanFunction item in Arguments)
            {
                itemsInBrases.Add("(" + item.GetFormulaString() + ")");
            }

            return string.Join("v", itemsInBrases);
        }
    }
}

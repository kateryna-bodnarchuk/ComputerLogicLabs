﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LogicAlgebra.Core
{
    [DebuggerDisplay("{GetFormulaString(null)}")]
    public sealed class NotFunction : IBooleanFunction
    {
        public NotFunction(IBooleanFunction argument)
        {
            this.Argument = argument;
        }

        public static NotFunction DobleNot(IBooleanFunction argument)
            => new NotFunction(new NotFunction(argument));

        public IBooleanFunction Argument { get; }

        public static IBooleanFunction NotOptimized(IBooleanFunction function)
        {
            if (function is NotFunction) return ((NotFunction)function).Argument;
            else return new NotFunction(function);
        }

        public bool Evaluate(IEvaluationContext context)
        {
            bool argumentValue = Argument.Evaluate(context);
            return !argumentValue;
        }

        public string GetFormulaString(IFunctionFormatting formatting)
        {
            string argumentFormatted = Argument.GetFormulaString(formatting);
            string argumentEmbrased = InputFunction.TestInputInversable(Argument) ?
                argumentFormatted : 
                $"({argumentFormatted})";
            return "!" + argumentEmbrased;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LogicAlgebra.IntLogic;

namespace LogicAlgebra.FunctionOptimization
{
    /// <summary>
    /// Implements discunction between function implicants.
    /// </summary>
    public static class ImplicantDisjunctionNormalForm
    {
        public static bool Evaluate(IEnumerable<Implicant> implicants, uint input)
        {
            foreach (Implicant implicant in implicants)
            {
                if (EvaluateImplicant(input, implicant))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool EvaluateImplicant(uint input, Implicant implicant)
        {
            foreach (InputSign inputSign in implicant)
            {
                bool bit = BitTools.GetBit(input, bitIndex: inputSign.Index);
                if (inputSign.IsInversed)
                {
                    bit = !bit;
                }
                if (!bit)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

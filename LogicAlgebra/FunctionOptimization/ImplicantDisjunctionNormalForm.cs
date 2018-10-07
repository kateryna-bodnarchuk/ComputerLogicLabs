using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LogicAlgebra.FunctionOptimization
{
    /// <summary>
    /// Implements discunction between function implicants.
    /// </summary>
    public class ImplicantDisjunctionNormalForm
    {
        private readonly Implicant[] implicants;

        public ImplicantDisjunctionNormalForm(IEnumerable<Implicant> implicants)
        {
            this.implicants = implicants.ToArray();
        }

        public bool Evaluate(uint input)
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

        public override string ToString()
        {
            return Implicant.GetDisjunctionFormString(implicants);
        }
    }
}

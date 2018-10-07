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
    public class ImplicantDisjunctionNormalForm
    {
        public ImplicantDisjunctionNormalForm(IEnumerable<Implicant> implicants)
        {
            this.Implicants = implicants.ToArray();
        }

        public IReadOnlyList<Implicant> Implicants { get; }

        public bool Evaluate(uint input)
        {
            foreach (Implicant implicant in Implicants)
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
            return Implicant.GetDisjunctionFormString(Implicants);
        }
    }
}

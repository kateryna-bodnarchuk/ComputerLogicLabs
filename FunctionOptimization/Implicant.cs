using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections;

namespace FunctionOptimization
{
    /// <summary>
    /// Universal aggrigate to use for constinuents and implicants 
    /// because it contains list of inputs with their signs.
    /// </summary>
    /// <remarks>
    /// Items are sorded by index.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class Implicant : IReadOnlyCollection<InputSign>, IEquatable<Implicant>
    {
        private readonly InputSign[] list;
        private readonly HashSet<InputSign> indexSet;

        public Implicant(IEnumerable<InputSign> items)
        {
            this.list = items.OrderBy(i => i.Index).ToArray();
            this.indexSet = new HashSet<InputSign>(items, new InputSignEqualityComparer());
        }

        internal int GetPositiveBitsCount()
        {
            return this.Where(i => !i.IsInversed).Count();
        }

        /// <summary>
        /// Tests if two implicants have the same set of items 
        /// and difference is only in one item sign.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="commonImplicant">Common intersection of implicants only if returns true.</param>
        /// <param name="signDifferenceIndex">Single item index difference only if returns true.</param>
        /// <returns></returns>
        public static bool TestDifferenceInOneSign(
            Implicant a, 
            Implicant b, 
            out Implicant commonImplicant,
            out int signDifferenceIndex)
        {
            commonImplicant = null;
            signDifferenceIndex = -1;

            if (a.Count != b.Count)
            {
                return false;
            }

            var common = new List<InputSign>();
            List<int> signDifferenceIndexes = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                InputSign aItem = a.list[i];
                InputSign bItem = b.list[i];
                if (aItem.Index != bItem.Index)
                {
                    return false;
                }

                if (aItem.IsInversed == bItem.IsInversed)
                {
                    common.Add(aItem);
                }
                else
                {
                    signDifferenceIndexes.Add(aItem.Index);
                }
            }

            if (signDifferenceIndexes.Count == 0)
            {
                return false;
            }
            else if (signDifferenceIndexes.Count == 1)
            {
                commonImplicant = new Implicant(common);
                signDifferenceIndex = signDifferenceIndexes[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        internal bool IsSubsetOf(Implicant other)
        {
            return this.indexSet.IsSubsetOf(other.indexSet);
        }

        public IEnumerator<InputSign> GetEnumerator() => list.AsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private string DebuggerDisplay
        {
            get
            {
                var itemsForConjunction = this
                .OrderBy(i => i.Index)
                .Select(i => (i.IsInversed ? "!" : "") + i.Index.ToString()).ToArray();
                return string.Join('^', itemsForConjunction);
            }
        }

        public override string ToString() => DebuggerDisplay;

        public int Count => list.Length;

        public static string GetDisjunctionFormString(IEnumerable<Implicant> implicants)
        {
            return string.Join("v", implicants.Select(i => "(" + i.ToString() + ")"));
        }

        public bool Equals(Implicant other) => this.indexSet.SetEquals(other.indexSet);

        public override bool Equals(object obj) => Equals(obj as Implicant);

        public static bool operator ==(Implicant a, Implicant b) => a.Equals(b);

        public static bool operator !=(Implicant a, Implicant b) => !a.Equals(b);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LogicAlgebra.Core
{
    public sealed class DictionaryEvaluationContext : IEvaluationContext
    {
        private readonly Dictionary<int, bool> map;

        public DictionaryEvaluationContext(Dictionary<int, bool> map)
        {
            this.map = map;
        }

        public bool GetInput(int index)
        {
            return map[index];
        }
    }
}

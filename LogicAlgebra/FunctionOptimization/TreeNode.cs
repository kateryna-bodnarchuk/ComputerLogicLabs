using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogicAlgebra.FunctionOptimization
{
    /// <summary>
    /// Imutable tree node.
    /// </summary>
    public sealed class TreeNode<T>
    {
        public TreeNode(T value, IEnumerable<TreeNode<T>> children)
        {
            this.Value = value;
            this.Children = new ReadOnlyCollection<TreeNode<T>>(children.ToArray());
        }

        public T Value { get; }

        public IReadOnlyList<TreeNode<T>> Children { get; }
    }
}

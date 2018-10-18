namespace LogicAlgebra.Core
{
    public interface IBooleanFunction
    {
        bool Evaluate(IEvaluationContext context);
        string GetFormulaString(IFunctionFormatting formatting = null);
    }
}

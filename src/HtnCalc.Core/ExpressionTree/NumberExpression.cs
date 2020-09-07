namespace HtnCalc.Core.ExpressionTree
{
    public class NumberExpression : IExpressionNode
    {
        public decimal Value { get; }

        public NumberExpression(decimal value)
        {
            Value = value;
        }

        public decimal Calculate()
        {
            return Value;
        }
    }
}
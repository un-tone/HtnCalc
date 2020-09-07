namespace HtnCalc.Core.ExpressionTree
{
    public class SubtractionExpression : ArithmeticExpression
    {
        public SubtractionExpression(IExpressionNode left, IExpressionNode right)
            : base(left, right)
        {
        }

        public override decimal Calculate()
        {
            return Left.Calculate() - Right.Calculate();
        }
    }
}
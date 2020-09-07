namespace HtnCalc.Core.ExpressionTree
{
    public class DivisionExpression : ArithmeticExpression
    {
        public DivisionExpression(IExpressionNode left, IExpressionNode right)
            : base(left, right)
        {
        }

        public override decimal Calculate()
        {
            var rightValue = Right.Calculate();

            if (rightValue == 0M)
                throw new CalculationException(this, "Division by zero.");

            return Left.Calculate() / rightValue;
        }
    }
}
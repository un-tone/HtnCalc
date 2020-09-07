namespace HtnCalc.Core.ExpressionTree
{
    public class MultiplicationExpression : ArithmeticExpression
    {
        public MultiplicationExpression(IExpressionNode left, IExpressionNode right)
            : base(left, right)
        {
        }

        public override decimal Calculate()
        {
            return Left.Calculate() * Right.Calculate();
        }
    }
}
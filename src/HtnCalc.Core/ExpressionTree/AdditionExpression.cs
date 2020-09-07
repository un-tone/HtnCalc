namespace HtnCalc.Core.ExpressionTree
{
    public class AdditionExpression : ArithmeticExpression
    {
        public AdditionExpression(IExpressionNode left, IExpressionNode right)
            : base(left, right)
        {
        }

        public override decimal Calculate()
        {
            return Left.Calculate() + Right.Calculate();
        }
    }
}
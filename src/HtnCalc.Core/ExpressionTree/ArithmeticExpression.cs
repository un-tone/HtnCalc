namespace HtnCalc.Core.ExpressionTree
{
    public abstract class ArithmeticExpression : IExpressionNode
    {
        public IExpressionNode Left { get; set; }
        public IExpressionNode Right { get; set; }

        protected ArithmeticExpression(IExpressionNode left, IExpressionNode right)
        {
            Left = left;
            Right = right;
        }

        public abstract decimal Calculate();
    }
}
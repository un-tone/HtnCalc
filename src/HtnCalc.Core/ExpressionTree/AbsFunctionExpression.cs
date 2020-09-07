using System;

namespace HtnCalc.Core.ExpressionTree
{
    public class AbsFunctionExpression : IExpressionNode
    {
        public IExpressionNode ArgumentNode { get; }

        public AbsFunctionExpression(IExpressionNode argumentNode)
        {
            ArgumentNode = argumentNode;
        }

        public decimal Calculate()
        {
            return Math.Abs(ArgumentNode.Calculate());
        }
    }
}
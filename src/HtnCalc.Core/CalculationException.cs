using System;

namespace HtnCalc.Core
{
    public class CalculationException : Exception
    {
        public IExpressionNode ExpressionNode { get; }

        public CalculationException(IExpressionNode expressionNode, string message)
            : base(message)
        {
            ExpressionNode = expressionNode;
        }

        public override string ToString()
        {
            return $"{Message} Type {ExpressionNode.GetType().Name}.";
        }
    }
}
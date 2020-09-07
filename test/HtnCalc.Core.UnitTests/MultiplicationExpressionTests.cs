using HtnCalc.Core.ExpressionTree;
using Xunit;

namespace HtnCalc.Core.UnitTests
{
    public class MultiplicationExpressionTests
    {
        [Theory]
        [InlineData(3, 5, 15)]
        [InlineData(1.1756, 138.066, 162.3103896)]
        public void Calculate(decimal left, decimal right, decimal expected)
        {
            var expression = new MultiplicationExpression(new NumberExpression(left), new NumberExpression(right));
            var result = expression.Calculate();
            Assert.Equal(expected, result);
        }
    }
}
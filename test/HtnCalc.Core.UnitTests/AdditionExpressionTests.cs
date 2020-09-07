using HtnCalc.Core.ExpressionTree;
using Xunit;

namespace HtnCalc.Core.UnitTests
{
    public class AdditionExpressionTests
    {
        [Theory]
        [InlineData(3, 5, 8)]
        [InlineData(1.1756, 138.066, 139.2416)]
        public void Calculate(decimal left, decimal right, decimal expected)
        {
            var expression = new AdditionExpression(new NumberExpression(left), new NumberExpression(right));
            var result = expression.Calculate();
            Assert.Equal(expected, result);
        }
    }
}
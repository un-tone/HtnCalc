using HtnCalc.Core.ExpressionTree;
using Xunit;

namespace HtnCalc.Core.UnitTests
{
    public class SubtractionExpressionTests
    {
        [Theory]
        [InlineData(3, 5, -2)]
        [InlineData(555, 388, 167)]
        [InlineData(138.066, 1.1756, 136.8904)]
        public void Calculate(decimal left, decimal right, decimal expected)
        {
            var expression = new SubtractionExpression(new NumberExpression(left), new NumberExpression(right));
            var result = expression.Calculate();
            Assert.Equal(expected, result);
        }
    }
}
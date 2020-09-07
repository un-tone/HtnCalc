using HtnCalc.Core.ExpressionTree;
using Xunit;

namespace HtnCalc.Core.UnitTests
{
    public class DivisionExpressionTests
    {
        [Theory]
        [InlineData(27, 3, 9)]
        [InlineData(3, 5, 0.6)]
        [InlineData(110.5272, 18.06, 6.12)]
        public void Calculate(decimal left, decimal right, decimal expected)
        {
            var expression = new DivisionExpression(new NumberExpression(left), new NumberExpression(right));
            var result = expression.Calculate();
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Calculate_DivisionByZero_Exception()
        {
            var expression = new DivisionExpression(new NumberExpression(5), new NumberExpression(0));
            Assert.Throws<CalculationException>(() => expression.Calculate());
        }
    }
}
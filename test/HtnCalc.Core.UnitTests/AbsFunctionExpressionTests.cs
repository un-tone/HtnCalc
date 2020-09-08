using HtnCalc.Core.ExpressionTree;
using Xunit;

namespace HtnCalc.Core.UnitTests
{
    public class AbsFunctionExpressionTests
    {
        [Theory]
        [InlineData(3, 3)]
        [InlineData(-1.1756, 1.1756)]
        public void Calculate(decimal arg, decimal expected)
        {
            var expression = new AbsFunctionExpression(new NumberExpression(arg));
            var result = expression.Calculate();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 5, 8)]
        [InlineData(1.1756, 138.066, 139.2416)]
        public void Calculate_Compound(decimal left, decimal right, decimal expected)
        {
            var expression = new AbsFunctionExpression(new AdditionExpression(new NumberExpression(left), new NumberExpression(right)));
            var result = expression.Calculate();
            Assert.Equal(expected, result);
        }
    }
}
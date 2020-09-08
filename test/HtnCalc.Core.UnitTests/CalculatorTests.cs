using HtnCalc.Core.ExpressionTree;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;
using Xunit;

namespace HtnCalc.Core.UnitTests
{
    public class CalculatorTests
    {
        readonly Calculator _calculator = new Calculator(new ExpressionParser(), new PermutationsBuilder(), new ExpressionTreeBuilder());

        [Theory]
        [InlineData("1 + 2", 3)]
        [InlineData("12 * 7", 84)]
        [InlineData("48 / 6", 8)]
        [InlineData("63 - 3", 60)]
        [InlineData("1", 1)]
        [InlineData("((1 + 1))", 2)]
        [InlineData("(1) + 1", 2)]
        [InlineData("1/2/3", 1.0 / 2.0 / 3.0)]
        [InlineData("(3 + 3) * 2", 12)]
        [InlineData("17 * (33 + 1) * 1", 578)]
        [InlineData("(((((67-7+6)))))", 54)]
        [InlineData("(4) + abs(5) + (1 + 1)", 11)]
        [InlineData("abs(5)", 5)]
        [InlineData("((abs(5)))", 5)]
        [InlineData("0 / 8", 0)]
        [InlineData("10 - 15", -5)]
        public void Calculate(string input, decimal output)
        {
            var result = _calculator.Calculate(input);

            result = decimal.Round(result, 15);
            Assert.Equal(output, result);
        }

        [Theory]
        [InlineData("5 / 0")]
        public void Calculate_CalculationException(string input)
        {
            Assert.Throws<CalculationException>(() => _calculator.Calculate(input));
        }

        [Theory]
        [InlineData("5 * (0")]
        [InlineData("10 + 3 -")]
        [InlineData("5 ** 3")]
        [InlineData("1 + abs((1+2)")]
        public void Calculate_ParsingException(string input)
        {
            Assert.Throws<ParsingException>(() => _calculator.Calculate(input));
        }
    }
}
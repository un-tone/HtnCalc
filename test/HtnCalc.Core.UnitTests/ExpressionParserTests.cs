using System.Linq;
using HtnCalc.Core.Parsing;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class ExpressionParserTests
    {
        [Fact]
        public void Tokenize_Spaces()
        {
            var parser = new ExpressionParser();
            var tokens = parser.Tokenize("   ");
            Empty(tokens);
        }

        [Theory]
        [InlineData("+", ArithmeticOperationType.Addition)]
        [InlineData("-", ArithmeticOperationType.Subtraction)]
        [InlineData("*", ArithmeticOperationType.Multiplication)]
        [InlineData("/", ArithmeticOperationType.Division)]
        public void Tokenize_SimpleAddition(string arithmeticOperator, ArithmeticOperationType operationType)
        {
            var parser = new ExpressionParser();

            var tokens = parser.Tokenize($"1 {arithmeticOperator} 2").ToList();

            Equal(3, tokens.Count);
            IsType<ArithmeticOperatorToken>(tokens[1]);
            Equal(operationType, ((ArithmeticOperatorToken) tokens[1]).OperationType);
        }

        [Theory]
        [InlineData("5", "7", 5, 7)]
        [InlineData("5.007", "7", 5.007, 7)]
        [InlineData("1.12", "3.06", 1.12, 3.06)]
        public void Tokenize_Numbers(string num1, string num2, decimal numValue1, decimal numValue2)
        {
            var parser = new ExpressionParser();

            var tokens = parser.Tokenize($"{num1} + {num2}").ToList();

            Equal(3, tokens.Count);
            IsType<NumberToken>(tokens[0]);
            IsType<NumberToken>(tokens[2]);
            Equal(numValue1, ((NumberToken) tokens[0]).GetValue());
            Equal(numValue2, ((NumberToken) tokens[2]).GetValue());
        }

        [Fact]
        public void Tokenize_Brackets()
        {
            var parser = new ExpressionParser();

            var tokens = parser.Tokenize("(1 + 3)").ToList();

            Equal(5, tokens.Count);
            IsType<OpenBracketToken>(tokens[0]);
            IsType<CloseBracketToken>(tokens[4]);
        }

        [Fact]
        public void Tokenize_Unsupported()
        {
            var parser = new ExpressionParser();

            var tokens = parser.Tokenize(" test1 + 3 + 7test te55st ^&%* &^%$%").ToList();

            Equal(13, tokens.Count);
            IsType<UnknownToken>(tokens[0]);
            IsType<NumberToken>(tokens[1]);
            IsType<ArithmeticOperatorToken>(tokens[2]);
            IsType<NumberToken>(tokens[3]);
            IsType<ArithmeticOperatorToken>(tokens[4]);
            IsType<NumberToken>(tokens[5]);
            IsType<UnknownToken>(tokens[6]);
            IsType<UnknownToken>(tokens[7]);
            IsType<NumberToken>(tokens[8]);
            IsType<UnknownToken>(tokens[9]);
            IsType<UnknownToken>(tokens[10]);
            IsType<ArithmeticOperatorToken>(tokens[11]);
            IsType<UnknownToken>(tokens[12]);
        }

        [Theory]
        [InlineData("abs(5)", 4, 0)]
        [InlineData("1 + abs(5) * 2", 8, 2)]
        [InlineData("(abs(5))", 6, 1)]
        public void Tokenize_AbsFunction(string input, int expectedTokenCount, int expectedAbsIndex)
        {
            var parser = new ExpressionParser();

            var tokens = parser.Tokenize(input).ToList();

            Equal(expectedTokenCount, tokens.Count);
            IsType<AbsFunctionToken>(tokens[expectedAbsIndex]);
        }
    }
}
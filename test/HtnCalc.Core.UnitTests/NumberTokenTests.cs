using HtnCalc.Core.Parsing;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class NumberTokenTests
    {
        [Theory]
        [InlineData("1", 1)]
        [InlineData("999", 999)]
        [InlineData("00999", 999)]
        [InlineData("1.07", 1.07)]
        [InlineData("29.16988", 29.16988)]
        public void GetValue(string inputNumber, decimal expected)
        {
            var token = GetNumberToken(inputNumber, 3);
            var result = token.GetValue();
            Equal(expected, result);
        }

        [Fact]
        public void GetStartPosition()
        {
            var term = GetNumberToken("55", 3);
            var result = term.StartPosition;
            Equal(3, result);
        }

        [Theory]
        [InlineData("999", 3)]
        [InlineData("29.16988", 8)]
        public void GetEndPosition(string inputNumber, int expected)
        {
            var term = GetNumberToken(inputNumber, 1);
            var result = term.EndPosition;
            Equal(expected, result);
        }

        private NumberToken GetNumberToken(string input, int position)
        {
            var token = new NumberToken(input[0], position);
            for (int i = 1; i < input.Length; i++)
                token.Append(input[i]);
            return token;
        }
    }
}
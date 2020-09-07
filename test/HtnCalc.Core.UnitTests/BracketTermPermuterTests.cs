using System.Collections.Generic;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class BracketTermPermuterTests
    {
        [Fact]
        public void Permute_AllInBrackets()
        {
            var permuter = new BracketTermPermuter();
            var list = new LinkedList<ITerm>(new ITerm[]
            {
                new OpenBracketToken(1),
                new NumberToken('3', 2),
                new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 3),
                new NumberToken('5', 4),
                new CloseBracketToken(5)
            });

            var result = permuter.Permute(list);

            NotNull(result);
            Equal(3, result.Terms.Count);
            Equal(3, result.Terms.Count);
            Equal(2, result.StartPosition);
            Equal(4, result.EndPosition);
        }

        [Fact]
        public void Permute_LeftOperandBracket()
        {
            var permuter = new BracketTermPermuter();
            var list = new LinkedList<ITerm>(new ITerm[]
            {
                new OpenBracketToken(1),
                new NumberToken('3', 2),
                new CloseBracketToken(3),
                new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 5),
                new NumberToken('5', 4),
            });

            var result = permuter.Permute(list);

            NotNull(result);
            Equal(3, result.Terms.Count);
            var firstNode = result.Terms.First;
            IsType<NumberToken>(firstNode?.Value);
            IsType<ArithmeticOperatorToken>(firstNode.Next?.Value);
            IsType<NumberToken>(firstNode.Next?.Next?.Value);
            Equal(2, firstNode.Value.StartPosition);
            Equal(4, firstNode.Next?.Next?.Value.EndPosition);
        }
    }
}
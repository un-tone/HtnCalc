using System.Collections.Generic;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class AddSubTermPermuterTests
    {
        [Fact]
        public void Permute()
        {
            var permuter = new AddSubTermPermuter();
            var list = new LinkedList<ITerm>(new ITerm[]
            {
                new NumberToken('3', 1),
                new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 3),
                new NumberToken('5', 5),
                new ArithmeticOperatorToken(ArithmeticOperationType.Subtraction, 6),
                new NumberToken('8', 7),
            });

            var result = permuter.Permute(list);

            IsType<CompoundTerm>(result);
            Equal(3, result.Terms.Count);
            IsType<NumberToken>(result.Terms.First?.Value);
            IsType<ArithmeticOperatorToken>(result.Terms.First?.Next?.Value);
            IsType<CompoundTerm>(result.Terms.First?.Next?.Next?.Value);
            var compoundTerm = (CompoundTerm) result.Terms.First?.Next?.Next?.Value;
            Equal(3, result.Terms.Count);
            IsType<NumberToken>(compoundTerm.Terms.First?.Value);
            IsType<ArithmeticOperatorToken>(compoundTerm.Terms.First?.Next?.Value);
            IsType<NumberToken>(compoundTerm.Terms.First?.Next?.Next?.Value);
        }
    }
}
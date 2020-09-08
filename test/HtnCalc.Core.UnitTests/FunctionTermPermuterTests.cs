using System.Collections.Generic;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class FunctionTermPermuterTests
    {
        [Fact]
        public void Permute()
        {
            var permuter = new FunctionTermPermuter();
            var list = new LinkedList<ITerm>(new ITerm[]
            {
                new NumberToken('2', 1),
                new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 2),
                new AbsFunctionToken(4),
                new OpenBracketToken(8),
                new NumberToken('8', 9),
                new CloseBracketToken(12),
            });

            var result = permuter.Permute(list);

            IsType<CompoundTerm>(result);
            Equal(3, result.Terms.Count);
            IsType<NumberToken>(result.Terms.First?.Value);
            IsType<ArithmeticOperatorToken>(result.Terms.First?.Next?.Value);
            IsType<CompoundTerm>(result.Terms.First?.Next?.Next?.Value);
            var compoundTerm = (CompoundTerm) result.Terms.First?.Next?.Next?.Value;
            Equal(4, compoundTerm.Terms.Count);
            IsType<AbsFunctionToken>(compoundTerm.Terms.First?.Value);
            IsType<OpenBracketToken>(compoundTerm.Terms.First?.Next?.Value);
            IsType<NumberToken>(compoundTerm.Terms.First?.Next?.Next?.Value);
            IsType<CloseBracketToken>(compoundTerm.Terms.First?.Next?.Next?.Next?.Value);
        }
    }
}
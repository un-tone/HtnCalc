using System.Collections.Generic;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class CompoundTermTests
    {
        [Fact]
        public void GetStartPosition()
        {
            var term = CreateDefaultTerm();
            var result = term.StartPosition;
            Equal(3, result);
        }

        [Fact]
        public void GetStartPosition_Empty()
        {
            var term = new CompoundTerm();
            var result = term.StartPosition;
            Equal(-1, result);
        }

        [Fact]
        public void GetEndPosition()
        {
            var term = CreateDefaultTerm();
            var result = term.EndPosition;
            Equal(9, result);
        }

        [Fact]
        public void GetEndPosition_Empty()
        {
            var term = new CompoundTerm();
            var result = term.EndPosition;
            Equal(-1, result);
        }

        [Fact]
        public void Add()
        {
            var term = new CompoundTerm(new LinkedList<ITerm>(new ITerm[] {new NumberToken('3', 1)}));

            var token = new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 3);
            term.Add(token);

            Equal(2, term.Terms.Count);
            Equal(token, term.Terms.Last?.Value);
        }

        [Fact]
        public void Add_RemoveLast()
        {
            var term = new CompoundTerm(new LinkedList<ITerm>(new ITerm[] {new NumberToken('3', 1)}));

            var token = new CompoundTerm();
            term.Add(token, removeLast: true);

            Single(term.Terms);
            Equal(token, term.Terms.Last?.Value);
        }

        private static CompoundTerm CreateDefaultTerm()
        {
            var number1 = new NumberToken('1', 3);
            number1.Append('5');
            var number2 = new NumberToken('3', 8);
            number2.Append('3');

            return new CompoundTerm(new LinkedList<ITerm>(new ITerm[]
            {
                number1,
                new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 6),
                number2
            }));
        }
    }
}
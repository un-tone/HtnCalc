using System.Collections.Generic;
using HtnCalc.Core.ExpressionTree;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;
using Xunit;
using static Xunit.Assert;

namespace HtnCalc.Core.UnitTests
{
    public class ExpressionTreeBuilderTests
    {
        [Fact]
        public void BuildExpressionTree_SimpleArithmetic()
        {
            var builder = new ExpressionTreeBuilder();
            var term = new CompoundTerm(new LinkedList<ITerm>(new ITerm[]
            {
                new NumberToken('3', 1),
                new ArithmeticOperatorToken(ArithmeticOperationType.Addition, 2),
                new NumberToken('5', 3)
            }));

            var result = builder.BuildExpressionTree(term);

            IsType<AdditionExpression>(result);
            var additionExpression = (AdditionExpression) result;
            IsType<NumberExpression>(additionExpression.Left);
            IsType<NumberExpression>(additionExpression.Right);
        }

        [Fact]
        public void BuildExpressionTree_SimpleNumber()
        {
            var builder = new ExpressionTreeBuilder();
            var term = new NumberToken('3', 1);

            var result = builder.BuildExpressionTree(term);

            IsType<NumberExpression>(result);
        }
    }
}
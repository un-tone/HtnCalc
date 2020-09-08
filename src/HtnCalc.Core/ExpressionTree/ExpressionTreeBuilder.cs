using System;
using System.Collections.Generic;
using HtnCalc.Core.Parsing;
using HtnCalc.Core.Permutation;

namespace HtnCalc.Core.ExpressionTree
{
    public class ExpressionTreeBuilder : IExpressionTreeBuilder
    {
        public IExpressionNode BuildExpressionTree(ITerm term)
        {
            if (term is CompoundTerm compoundTerm)
            {
                var current = compoundTerm.Terms.First;

                if (current.Value is FunctionToken functionToken)
                {
                    if (compoundTerm.Terms.Count == 4 &&
                        current.Next?.Value is OpenBracketToken &&
                        compoundTerm.Terms.Last.Value is CloseBracketToken)
                    {
                        if (functionToken is AbsFunctionToken)
                        {
                            return new AbsFunctionExpression(BuildExpressionNode(compoundTerm.Terms.Last.Previous!));
                        }

                        throw new ParsingException(functionToken, "Unsuported function.");
                    }

                    throw new ParsingException(term, "Invalid function semantic.");
                }

                while (current != null)
                {
                    if (current.Value is ArithmeticOperatorToken)
                    {
                        return BuildExpressionNode(current);
                    }

                    current = current.Next;
                }
            }

            return BuildLiteralExpressionNode(term);
        }

        private IExpressionNode BuildExpressionNode(LinkedListNode<ITerm> termNode)
        {
            ITerm term = termNode.Value;
            return term switch
            {
                ArithmeticOperatorToken arithmeticOperationToken => BuildArithmeticExpression(termNode, arithmeticOperationToken.OperationType),
                CompoundTerm compoundTerm => BuildExpressionTree(compoundTerm),
                _ => BuildLiteralExpressionNode(term)
            };
        }

        private IExpressionNode BuildLiteralExpressionNode(ITerm term)
        {
            return term switch
            {
                NumberToken numberToken => new NumberExpression(numberToken.GetValue()),
                UnknownToken unknownToken => throw new ParsingException(unknownToken, $"Unknown token: {unknownToken.GetValue()}."),
                _ => throw new ParsingException(term, $"Not supported operation at position {term.StartPosition}.")
            };
        }

        private IExpressionNode BuildArithmeticExpression(LinkedListNode<ITerm> operatorNode, ArithmeticOperationType operationType)
        {
            if (operatorNode.Previous == null)
                throw new ParsingException(operatorNode.Value, "Left operand cannot be undefined.");

            if (operatorNode.Next == null)
                throw new ParsingException(operatorNode.Value, "Right operand cannot be undefined.");

            var leftExpression = BuildExpressionTree(operatorNode.Previous.Value);
            var rightExpression = BuildExpressionTree(operatorNode.Next.Value);
            return operationType switch
            {
                ArithmeticOperationType.Addition => new AdditionExpression(leftExpression, rightExpression),
                ArithmeticOperationType.Subtraction => new SubtractionExpression(leftExpression, rightExpression),
                ArithmeticOperationType.Multiplication => new MultiplicationExpression(leftExpression, rightExpression),
                ArithmeticOperationType.Division => new DivisionExpression(leftExpression, rightExpression),
                _ => throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null)
            };
        }
    }
}
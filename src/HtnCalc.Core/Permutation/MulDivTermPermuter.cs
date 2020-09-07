using System;
using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core.Permutation
{
    public class MulDivTermPermuter : ITermPermuter
    {
        public CompoundTerm Permute(in LinkedList<ITerm> sourceTerms)
        {
            return Permute(sourceTerms.First).RemoveCompoundRedundancy();
        }

        protected CompoundTerm Permute(LinkedListNode<ITerm>? current)
        {
            var result = new CompoundTerm();

            while (current != null)
            {
                switch (current.Value)
                {
                    case CompoundTerm compoundTerm:
                        var newTerm = Permute(compoundTerm.Terms.First);
                        result.Add(newTerm);
                        break;
                    case ArithmeticOperatorToken {OperationType: ArithmeticOperationType.Multiplication}:
                    case ArithmeticOperatorToken {OperationType: ArithmeticOperationType.Division}:
                    {
                        if (current.Next is null)
                            throw new ParsingException(current.Value, "No right operand for arithmetic operation.");

                        result.Add(BuildArithmeticCompoundTerm(result.Terms.Last.Value, current), removeLast: true);
                        current = current.Next;
                        break;
                    }
                    default:
                        result.Add(current.Value);
                        break;
                }

                current = current.Next;
            }

            return result;
        }

        private static CompoundTerm BuildArithmeticCompoundTerm(ITerm leftTerm, LinkedListNode<ITerm> current)
        {
            var newTerm = new CompoundTerm();
            newTerm.Add(leftTerm);
            newTerm.Add(current.Value);
            newTerm.Add(current.Next!.Value);
            return newTerm;
        }
    }
}
using System;
using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core.Permutation
{
    public class AddSubTermPermuter : ITermPermuter
    {
        public CompoundTerm Permute(in LinkedList<ITerm> sourceTerms)
        {
            var first = sourceTerms.First;
            return Permute(ref first).RemoveCompoundRedundancy();
        }

        protected CompoundTerm Permute(ref LinkedListNode<ITerm>? current)
        {
            var result = new CompoundTerm();

            while (current != null)
            {
                switch (current.Value)
                {
                    case CompoundTerm compoundTerm:
                    {
                        var first = compoundTerm.Terms.First;
                        var newTerm = Permute(ref first);
                        result.Add(newTerm);
                        current = current.Next;
                        break;
                    }
                    case ArithmeticOperatorToken {OperationType: ArithmeticOperationType.Addition}:
                    case ArithmeticOperatorToken {OperationType: ArithmeticOperationType.Subtraction}:
                    {
                        if (current.Next is null)
                            throw new ParsingException(current.Value, "No right operand for arithmetic operation.");

                        var newTerm = BuildArithmeticCompoundTerm(current);
                        result.Add(newTerm, removeLast: true);
                        current = null;
                        break;
                    }
                    default:
                        result.Add(current.Value);
                        current = current.Next;
                        break;
                }
            }

            return result.RemoveCompoundRedundancy();
        }

        private CompoundTerm BuildArithmeticCompoundTerm(LinkedListNode<ITerm> current)
        {
            var next = current.Next;
            var newRightTerm = Permute(ref next).RemoveRedundancy();

            var newTerm = new CompoundTerm();
            newTerm.Add(current.Previous!.Value);
            newTerm.Add(current.Value);
            newTerm.Add(newRightTerm);
            return newTerm;
        }
    }
}
using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core.Permutation
{
    public class FunctionTermPermuter : ITermPermuter
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
                        break;
                    }
                    case FunctionToken functionToken:
                        if (current.Next?.Value is OpenBracketToken)
                        {
                            result.Add(BuildFunctionCompoundTerm(ref current));
                        }
                        else
                        {
                            result.Add(functionToken);
                        }

                        break;
                    default:
                        result.Add(current.Value);
                        break;
                }

                current = current?.Next;
            }

            return result;
        }

        private CompoundTerm BuildFunctionCompoundTerm(ref LinkedListNode<ITerm>? current)
        {
            var term = new CompoundTerm();

            while (current != null)
            {
                term.Add(current.Value);

                if (current.Value is CloseBracketToken)
                    break;

                current = current.Next;
            }

            return term;
        }
    }
}
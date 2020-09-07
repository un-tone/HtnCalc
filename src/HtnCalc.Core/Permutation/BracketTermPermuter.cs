using System;
using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core.Permutation
{
    public class BracketTermPermuter : ITermPermuter
    {
        public CompoundTerm Permute(in LinkedList<ITerm> sourceTerms)
        {
            var first = sourceTerms.First;
            return Permute(ref first).RemoveCompoundRedundancy();
        }

        protected CompoundTerm Permute(ref LinkedListNode<ITerm>? current)
        {
            var result = new CompoundTerm();
            bool isFunctionStarted = false;

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
                        result.Add(current.Value);
                        if (current.Next?.Value is OpenBracketToken)
                        {
                            isFunctionStarted = true;
                        }

                        break;
                    case OpenBracketToken openBracketToken:
                    {
                        if (isFunctionStarted)
                        {
                            result.Add(current.Value);
                        }

                        current = current.Next;
                        var newTerm = Permute(ref current).RemoveRedundancy();

                        result.Add(newTerm);

                        if (!(current?.Value is CloseBracketToken))
                            throw new ParsingException(openBracketToken, "No closing bracket.");

                        if (isFunctionStarted)
                        {
                            result.Add(current.Value);
                            isFunctionStarted = false;
                        }

                        break;
                    }
                    case CloseBracketToken _:
                        return result;
                    default:
                        result.Add(current.Value);
                        break;
                }

                current = current.Next;
            }

            return result;
        }
    }
}
using System.Collections.Generic;
using HtnCalc.Core.Permutation;

namespace HtnCalc.Core
{
    public interface ITermPermuter
    {
        CompoundTerm Permute(in LinkedList<ITerm> sourceTerms);
    }
}
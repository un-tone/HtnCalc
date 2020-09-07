using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core.Permutation
{
    public class PermutationsBuilder : IPermutationsBuilder
    {
        private readonly ITermPermuter[] _termPermuters =
        {
            new BracketTermPermuter(),
            new FunctionTermPermuter(),
            new MulDivTermPermuter(),
            new AddSubTermPermuter()
        };

        public ITerm BuildTermTree(IEnumerable<Token> tokens)
        {
            var rootTerm = new CompoundTerm(new LinkedList<ITerm>(tokens));
            foreach (var termFilter in _termPermuters)
            {
                rootTerm = termFilter.Permute(rootTerm.Terms);
            }

            return rootTerm.RemoveRedundancy();
        }
    }
}
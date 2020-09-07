using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core
{
    public interface IPermutationsBuilder
    {
        ITerm BuildTermTree(IEnumerable<Token> tokens);
    }
}
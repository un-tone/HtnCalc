using System.Collections.Generic;
using HtnCalc.Core.Parsing;

namespace HtnCalc.Core
{
    public interface IExpressionParser
    {
        IEnumerable<Token> Tokenize(string sourceExpression);
    }
}
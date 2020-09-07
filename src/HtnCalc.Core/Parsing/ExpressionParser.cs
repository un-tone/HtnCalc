using System.Collections.Generic;

namespace HtnCalc.Core.Parsing
{
    public class ExpressionParser : IExpressionParser
    {
        public IEnumerable<Token> Tokenize(string sourceExpression)
        {
            Token? lastToken = null;
            int position = 0;
            foreach (char element in sourceExpression)
            {
                position++;
                Token? token = element switch
                {
                    '(' => new OpenBracketToken(position),
                    ')' => new CloseBracketToken(position),
                    '+' => new ArithmeticOperatorToken(ArithmeticOperationType.Addition, position),
                    '-' => new ArithmeticOperatorToken(ArithmeticOperationType.Subtraction, position),
                    '*' => new ArithmeticOperatorToken(ArithmeticOperationType.Multiplication, position),
                    '/' => new ArithmeticOperatorToken(ArithmeticOperationType.Division, position),
                    var ch when char.IsDigit(ch) || ch == '.' => lastToken is NumberToken numberToken ? numberToken.Append(ch) : new NumberToken(ch, position),
                    ' ' => null,
                    _ => lastToken is UnknownToken unknownToken ? unknownToken.Append(element) : new UnknownToken(element, position)
                };

                if (token == null || token is OpenBracketToken)
                {
                    if (lastToken is UnknownToken unknownToken &&
                        unknownToken.GetValue().ToLowerInvariant() == "abs")
                    {
                        lastToken = new AbsFunctionToken(unknownToken.StartPosition);
                    }
                }

                if (token == null) // space symbol - skip it - return completed token
                {
                    if (lastToken != null)
                    {
                        yield return lastToken;
                    }

                    lastToken = null;
                }
                else if (token != lastToken) // new token started - return completed token
                {
                    if (lastToken != null)
                    {
                        yield return lastToken;
                    }

                    lastToken = token;
                }
            }

            if (lastToken != null) // the latest token
            {
                yield return lastToken;
            }
        }
    }
}
using System.Globalization;
using System.Linq;

namespace HtnCalc.Core.Parsing
{
    public class NumberToken : LiteralToken
    {
        public NumberToken(char ch, int position)
            : base(ch, position)
        {
        }

        public decimal GetValue()
        {
            return decimal.Parse(new string(Chars.ToArray()), new CultureInfo("en-US"));
        }
    }
}
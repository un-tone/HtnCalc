using System.Collections.Generic;
using System.Linq;

namespace HtnCalc.Core.Parsing
{
    public abstract class LiteralToken : Token
    {
        protected IList<char> Chars { get; }

        public override int EndPosition => StartPosition + Chars.Count - 1;

        protected LiteralToken(char ch, int position)
            : base(position)
        {
            Chars = new List<char> {ch};
        }

        public LiteralToken Append(in char ch)
        {
            Chars.Add(ch);
            return this;
        }

        public override string ToString()
        {
            return new string(Chars.ToArray());
        }
    }
}
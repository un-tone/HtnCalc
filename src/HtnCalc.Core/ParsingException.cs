using System;

namespace HtnCalc.Core
{
    public class ParsingException : Exception
    {
        public ITerm InvalidTerm { get; }

        public ParsingException(ITerm invalidTerm, string message)
            : base(message)
        {
            InvalidTerm = invalidTerm;
        }

        public override string ToString()
        {
            return $"{Message} Position {InvalidTerm.GetWholePosition()}.";
        }
    }
}
namespace HtnCalc.Core.Parsing
{
    public class AbsFunctionToken : FunctionToken
    {
        public override int EndPosition => StartPosition + 2;

        public AbsFunctionToken(int position)
            : base(position)
        {
        }

        public override string ToString()
        {
            return "Abs function";
        }
    }
}
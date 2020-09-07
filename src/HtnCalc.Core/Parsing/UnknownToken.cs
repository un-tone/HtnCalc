namespace HtnCalc.Core.Parsing
{
    public class UnknownToken : LiteralToken
    {
        public UnknownToken(char ch, int position)
            : base(ch, position)
        {
        }

        public string GetValue()
        {
            return ToString();
        }
    }
}
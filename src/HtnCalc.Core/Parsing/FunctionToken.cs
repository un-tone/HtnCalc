namespace HtnCalc.Core.Parsing
{
    public abstract class FunctionToken : Token
    {
        protected FunctionToken(int position)
            : base(position)
        {
        }
    }
}
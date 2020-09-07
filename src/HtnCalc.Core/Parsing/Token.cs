namespace HtnCalc.Core.Parsing
{
    public abstract class Token : ITerm
    {
        public int StartPosition { get; }
        public virtual int EndPosition { get; }

        protected Token(int position)
        {
            StartPosition = position;
            EndPosition = position;
        }
    }
}
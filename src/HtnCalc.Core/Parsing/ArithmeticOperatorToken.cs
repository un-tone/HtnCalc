namespace HtnCalc.Core.Parsing
{
    public class ArithmeticOperatorToken : Token
    {
        public ArithmeticOperationType OperationType { get; }

        public ArithmeticOperatorToken(ArithmeticOperationType operationType, int position)
            : base(position)
        {
            OperationType = operationType;
        }
    }
}
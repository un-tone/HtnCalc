namespace HtnCalc.Core
{
    public class Calculator : ICalculator

    {
        public IExpressionParser ExpressionParser { get; }
        public IPermutationsBuilder PermutationsBuilder { get; }
        public IExpressionTreeBuilder ExpressionTreeBuilder { get; }

        public Calculator(
            IExpressionParser expressionParser,
            IPermutationsBuilder permutationsBuilder,
            IExpressionTreeBuilder expressionTreeBuilder)
        {
            ExpressionParser = expressionParser;
            PermutationsBuilder = permutationsBuilder;
            ExpressionTreeBuilder = expressionTreeBuilder;
        }

        public decimal Calculate(string input)
        {
            var tokens = ExpressionParser.Tokenize(input);
            var rootTerm = PermutationsBuilder.BuildTermTree(tokens);
            var calcExpression = ExpressionTreeBuilder.BuildExpressionTree(rootTerm);

            return calcExpression.Calculate();
        }
    }
}
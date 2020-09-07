namespace HtnCalc.Core
{
    public interface IExpressionTreeBuilder
    {
        IExpressionNode BuildExpressionTree(ITerm term);
    }
}
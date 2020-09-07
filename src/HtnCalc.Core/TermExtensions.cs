using HtnCalc.Core.Permutation;

namespace HtnCalc.Core
{
    public static class TermExtensions
    {
        public static ITerm RemoveRedundancy(this CompoundTerm compoundTerm)
        {
            return compoundTerm.Terms.Count == 1 ? compoundTerm.Terms.First.Value : compoundTerm;
        }

        public static CompoundTerm RemoveCompoundRedundancy(this CompoundTerm compoundTerm)
        {
            return compoundTerm.Terms.Count == 1 && compoundTerm.Terms.First.Value is CompoundTerm next
                ? next
                : compoundTerm;
        }

        public static string GetWholePosition(this ITerm term)
        {
            var start = term.StartPosition;
            var end = term.EndPosition;
            return start == end ? start.ToString() : $"{start}-{end}";
        }
    }
}
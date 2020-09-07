using System.Collections.Generic;

namespace HtnCalc.Core.Permutation
{
    public class CompoundTerm : ITerm
    {
        public LinkedList<ITerm> Terms { get; }

        public int StartPosition => Terms.First?.Value.StartPosition ?? -1;
        public int EndPosition => Terms.Last?.Value.EndPosition ?? -1;

        public CompoundTerm()
        {
            Terms = new LinkedList<ITerm>();
        }

        public CompoundTerm(LinkedList<ITerm> terms)
        {
            Terms = terms;
        }

        public void Add(ITerm term, bool removeLast = false)
        {
            if (removeLast)
            {
                Terms.RemoveLast();
            }

            Terms.AddLast(term);
        }
    }
}
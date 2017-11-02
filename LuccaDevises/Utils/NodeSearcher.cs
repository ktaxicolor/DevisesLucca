using System.Collections.Generic;

namespace LuccaDevises.Utils
{
    public static class NodeSearcher
    {
        public static ExchangeRateNode SearchBreadthFirst(ExchangeRateNode treeRoot, Currency goalCurrency)
        {
            if (treeRoot == null)
            { return null; }

            Queue<ExchangeRateNode> queue = new Queue<ExchangeRateNode>();
            queue.Enqueue(treeRoot);
            treeRoot.MarkAsVisited();

            while (queue.Count > 0)
            {
                var nextNode = queue.Dequeue();
                if (nextNode.Data.FinalCurrency.Equals(goalCurrency))
                {
                    return nextNode;
                }

                foreach (ExchangeRateNode sibling in nextNode.Siblings)
                {
                    if (sibling.IsNotVisitedYet())
                    {
                        queue.Enqueue(sibling);
                        sibling.MarkAsVisited();
                    }
                }
                foreach (ExchangeRateNode child in nextNode.Children)
                {
                    if (child.IsNotVisitedYet())
                    {
                        queue.Enqueue(child);
                        child.MarkAsVisited();
                    }
                }
            }
            return treeRoot;

        }
    }
}

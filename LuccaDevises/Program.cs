using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Get data from file
            string filepath = args[0];
            FileParser parser = new FileParser(filepath);
            var data = parser.GetFileData(parser.ReadFile());
            ExchangeRateNode exchangeRatestree = EschangeRateNodeBuilder.BuildFromFileData(data);

            // Look for quickest currency exchange
            // data.GoalCurrency, Amount
            SearchBreadthFirst(exchangeRatestree, data);


            //TODO : breadth first search
            //TODO : tests

            Console.ReadKey();
        }

        private double SearchBreadthFirst(ExchangeRateNode treeRoot, FileData data)
        {
            double amountFinal = 0;
            if(treeRoot == null)
            { return amountFinal; }
            Queue<ExchangeRateNode> queue = new Queue<ExchangeRateNode>();
            queue.Enqueue(treeRoot);

            treeRoot.markAsVisited();

            while (queue.Count >0)
            {
                var nextNode = queue.Dequeue();

                // do stuff 
                if(nextNode.Data.FinalCurrency.Equals(data.GoalCurrency))
                {
                    return amountFinal;
                }
                Console.WriteLine("current Node is : " + nextNode.Data.InitialCurrency.Trigram + " - " + nextNode.Data.FinalCurrency.Trigram);

                foreach(ExchangeRateNode sibling in nextNode.Siblings)
                {
                    if(sibling.isNotVisitedYet())
                    {
                        queue.Enqueue(sibling);
                        sibling.markAsVisited();
                    }
                }
                foreach(ExchangeRateNode child in nextNode.Children)
                {
                    if (child.isNotVisitedYet())
                    {
                        queue.Enqueue(child);
                        child.markAsVisited();
                    }
                }
            }

        }
    }
}

﻿using LuccaDevises.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Setup 
            var console = new ConsoleWriter();

            try {
                // Get data from file
                string filepath = args[0];
                FileParser parser = new FileParser(filepath);
                var data = parser.GetFileData(parser.ReadFile());
                ExchangeRateNode exchangeRatestree = ExchangeRateNodeBuilder.BuildFromFileData(data);

                // Look for quickest currency exchange
                ExchangeRateNode bestEndResultNode = SearchBreadthFirst(exchangeRatestree, data);

                // Return result
                if (bestEndResultNode == null || bestEndResultNode.Equals(exchangeRatestree))
                {
                    console.WriteErrorData(data);
                }
                else
                {
                    console.WriteResult(ComputeResultAmount(bestEndResultNode, data.Amount));
                }
            }
            catch (IndexOutOfRangeException iore)
            {
                console.WriteErrorArgs();
            }
            catch (DataFileFormatException dffe)
            {
                console.WriteError(dffe.Message);
            }
            catch (Exception e)
            {  
                console.WriteError(e.Message);
            }
            
            Console.ReadKey();
        }

        private static int ComputeResultAmount(ExchangeRateNode bestEndResultNode, int initialAmount)
        {
            ExchangeRateNode node = bestEndResultNode;
            double finalAmount = initialAmount;
            while(node.Root != null)
            {
                finalAmount = AccountingUtils.RoundRate(finalAmount * node.Data.Rate);
                node = node.Root;
            }
            return (int)Math.Round(finalAmount);
        }

        private static ExchangeRateNode SearchBreadthFirst(ExchangeRateNode treeRoot, FileData data)
        {
            if(treeRoot == null)
            { return null; }

            Queue<ExchangeRateNode> queue = new Queue<ExchangeRateNode>();
            queue.Enqueue(treeRoot);
            treeRoot.MarkAsVisited();

            while (queue.Count >0)
            {
                var nextNode = queue.Dequeue();
                if (nextNode.Data.FinalCurrency.Equals(data.GoalCurrency))
                {
                    return nextNode;
                }
                
                foreach(ExchangeRateNode sibling in nextNode.Siblings)
                {
                    if(sibling.IsNotVisitedYet())
                    {
                        queue.Enqueue(sibling);
                        sibling.MarkAsVisited();
                    }
                }
                foreach(ExchangeRateNode child in nextNode.Children)
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

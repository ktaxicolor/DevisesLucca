using LuccaDevises.Utils;
using System;

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
                ExchangeRateNode exchangeRatesTree = ExchangeRateNodeBuilder.BuildFromFileData(data);

                // Look for quickest currency exchange
                ExchangeRateNode bestEndResultNode = NodeSearcher.SearchBreadthFirst(exchangeRatesTree, data.GoalCurrency);

                // Return result
                if (bestEndResultNode != null && !bestEndResultNode.Equals(exchangeRatesTree))
                {
                    console.WriteResult(AccountingUtils.ComputeResultAmount(bestEndResultNode, data.Amount));
                }
                else
                {
                    console.WriteErrorData(data);
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
    }
}

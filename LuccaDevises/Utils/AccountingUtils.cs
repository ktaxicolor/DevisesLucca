using System;

namespace LuccaDevises.Utils
{
    public static class AccountingUtils
    {
        public static double RoundRate(double rate)
        {
            return Math.Round(rate, 4);
        }

        public static int ComputeResultAmount(ExchangeRateNode bestEndResultNode, int initialAmount)
        {
            ExchangeRateNode node = bestEndResultNode;
            double finalAmount = initialAmount;
            while (node.Root != null)
            {
                finalAmount = RoundRate(finalAmount * node.Data.Rate);
                node = node.Root;
            }
            return (int)Math.Round(finalAmount);
        }
    }
}

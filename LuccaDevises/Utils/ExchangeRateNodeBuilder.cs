using System.Collections.Generic;

namespace LuccaDevises
{
    public static class ExchangeRateNodeBuilder
    {
        public static ExchangeRateNode BuildFromFileData(FileData data)
        {
            ExchangeRateNode root = BuildTree(new ExchangeRateNode(null, new ExchangeRate(0, new Currency(), data.StartingCurrency)), data.ExchangeRates);

            return root;
        }

        private static ExchangeRateNode BuildTree(ExchangeRateNode root, List<ExchangeRate> availableExchRates)
        {
            ExchangeRate currentExch;
            for (int exchIndex = availableExchRates.Count - 1; exchIndex >= 0; exchIndex--)
            {
                currentExch = availableExchRates[exchIndex];

                if (currentExch.InitialCurrency.Equals(root.Data.FinalCurrency))
                {
                    root.AddChild(new ExchangeRateNode(root, currentExch));
                    availableExchRates.RemoveAt(exchIndex);
                }
                else if (currentExch.FinalCurrency.Equals(root.Data.FinalCurrency))
                {
                    root.AddChild(new ExchangeRateNode(root, currentExch.InverseRate()));
                    availableExchRates.RemoveAt(exchIndex);
                }
            }

            if (root.Children != null)
            {
                foreach (ExchangeRateNode exchNode in root.Children)
                {
                    BuildTree(exchNode, availableExchRates);
                }
            }

            return root;
        }
    }
}

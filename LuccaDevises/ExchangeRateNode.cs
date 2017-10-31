using System;
using System.Collections.Generic;

namespace LuccaDevises
{
    public static class EschangeRateNodeBuilder
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
                    root.AddSon(new ExchangeRateNode(root, currentExch));
                    availableExchRates.RemoveAt(exchIndex);
                }
                else if (currentExch.FinalCurrency.Equals(root.Data.FinalCurrency))
                {
                    root.AddSon(new ExchangeRateNode(root, currentExch.InverseRate()));
                    availableExchRates.RemoveAt(exchIndex);
                }
            }

            if (root.Children != null) {
                foreach (ExchangeRateNode exchNode in root.Children)
                {
                    BuildTree(exchNode, availableExchRates);
                }
            }

            return root;
        }
    }

    public class ExchangeRateNode
    {
        public ExchangeRateNode Root { get; set; }
        public List<ExchangeRateNode> Children { get; set; }
        public ExchangeRate Data { get; set; }
        public List<ExchangeRateNode> Siblings { get
            {
                var siblings = new List<ExchangeRateNode>();
                if (Root != null)
                {
                    foreach (ExchangeRateNode exchNode in Root.Children)
                    {
                        if (exchNode != this)
                        {
                            siblings.Add(exchNode);
                        }
                    }
                }
                return siblings;
            }
        }
        private bool isVisited;

        public ExchangeRateNode(ExchangeRateNode pRoot, ExchangeRate pData)
        {
            Root = pRoot;
            Data = pData;
            isVisited = false;
            Children = new List<ExchangeRateNode>();
        }

        public void AddSon(ExchangeRateNode exchangeRateNode)
        {
            if (exchangeRateNode != null)
            {
                if (Children == null)
                {
                    Children = new List<ExchangeRateNode>();
                }
                Children.Add(exchangeRateNode);
            }
        }

        internal bool IsNotVisitedYet()
        {
            return !isVisited;
        }

        internal void MarkAsVisited()
        {
            isVisited = true;
        }
    }

    public class ExchangeRate
    {
        private double rate;

        public double Rate {
            get { return rate; }
            set { rate = Math.Round(value, 4); }
        }
        public Currency InitialCurrency { get; set; }
        public Currency FinalCurrency { get; set; }

        public ExchangeRate(double pRate, Currency pInitialCurrency, Currency pFinalCurrency )
        {
            Rate = pRate;
            InitialCurrency = pInitialCurrency;
            FinalCurrency = pFinalCurrency;
        }

        public ExchangeRate InverseRate ()
        {
            Rate = 1 / Rate;
            var tmpFinalCur = FinalCurrency;
            FinalCurrency = InitialCurrency;
            InitialCurrency = tmpFinalCur;
            return this;
        }

    }

    public class Currency
    {
        private const string TRIGRAM_NONE = "---";
        private string trigram;
        public string Trigram
        {
            set { if (value != null && value.Length == 3) { trigram = value; } }
            get { return trigram.ToUpperInvariant(); }
        }
        public Currency(string pTrigram)
        {
            Trigram = pTrigram;
        }

        public Currency()
        {
            Trigram = TRIGRAM_NONE;
        }

        public bool Equals(Currency cur)
        {
            return Trigram.Equals(cur.Trigram);
        }
    }
}

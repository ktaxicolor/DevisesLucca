using System;
using System.Collections.Generic;

namespace LuccaDevises
{

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


}

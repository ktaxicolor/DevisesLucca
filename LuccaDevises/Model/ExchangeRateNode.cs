using System.Collections.Generic;

namespace LuccaDevises
{

    public class ExchangeRateNode
    {
        public ExchangeRateNode Root { get; set; }
        public List<ExchangeRateNode> Children { get; private set; }
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

        public void AddChild(ExchangeRateNode exchangeRateNode)
        {
            if (exchangeRateNode != null && Children != null)
            {
                Children.Add(exchangeRateNode);               
            }
        }

        public bool IsNotVisitedYet()
        {
            return !isVisited;
        }

        public void MarkAsVisited()
        {
            isVisited = true;
        }
    }


}

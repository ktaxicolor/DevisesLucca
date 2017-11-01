using LuccaDevises;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevisesTests
{
    public static class TestConst
    {
        public const string VALID_TRIGRAM = "VAL";
        public const string VALID_TRIGRAM_1 = "VA1";
        public const string INVALID_TRIGRAM = "INVA";

        public const double RATE = 0.42;
    }

    public static class TestHelper
    {

        public static ExchangeRateNode GetExchangeRateNode()
        {
            return new ExchangeRateNode(GetRootExchangeRateNode(), GetExchangeRate());
        }

        public static ExchangeRateNode GetChildExchangeRateNode(ExchangeRateNode parent)
        {
            return new ExchangeRateNode(parent, GetExchangeRate());
        }

        public static ExchangeRateNode GetRootExchangeRateNode()
        {
            return new ExchangeRateNode(null, GetExchangeRate());
        }

        public static ExchangeRate GetExchangeRate()
        {
            return new ExchangeRate(TestConst.RATE, GetCurrency(TestConst.VALID_TRIGRAM), GetCurrency(TestConst.VALID_TRIGRAM_1));
        }

        public static Currency GetCurrency(string trigram)
        {
            return new Currency(trigram);
        }

    }
}

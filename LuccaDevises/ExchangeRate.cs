using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public class ExchangeRate
    {
        private double rate;

        public double Rate
        {
            get { return rate; }
            set { rate = Math.Round(value, 4); }
        }
        public Currency InitialCurrency { get; set; }
        public Currency FinalCurrency { get; set; }

        public ExchangeRate(double pRate, Currency pInitialCurrency, Currency pFinalCurrency)
        {
            Rate = pRate;
            InitialCurrency = pInitialCurrency;
            FinalCurrency = pFinalCurrency;
        }

        public ExchangeRate InverseRate()
        {
            Rate = 1 / Rate;
            var tmpFinalCur = FinalCurrency;
            FinalCurrency = InitialCurrency;
            InitialCurrency = tmpFinalCur;
            return this;
        }

    }
}

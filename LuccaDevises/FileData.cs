using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public struct FileData
    {
        public int Amount { get; set; }
        public int ExchangeRateCount { get; set; }
        public Currency StartingCurrency { get; set; }
        public Currency GoalCurrency { get; set; }
        public List<ExchangeRate> ExchangeRates { get; set; }
    }
}

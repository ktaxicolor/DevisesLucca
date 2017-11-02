using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Utils
{
    public static class AccountingUtils
    {
        public static double RoundRate(double rate)
        {
            return Math.Round(rate, 4);
        }
    }
}

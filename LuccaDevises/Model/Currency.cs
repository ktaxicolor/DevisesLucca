using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public class Currency
    {
        private const string TRIGRAM_NONE = "---";
        private const string ERR_TRIGRAM = "Currency is not a trigram : ";
        private string trigram;
        public string Trigram
        {
            private set { if (value != null && value.Length == 3) { trigram = value; } else { throw new FormatException(ERR_TRIGRAM+ value); } }
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

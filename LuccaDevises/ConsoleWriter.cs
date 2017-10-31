using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises
{
    public class ConsoleWriter
    {
        private const String DASH = " - ";
        private const String ERR_HEADER = "Error : ";
        private const String ERR_DATA = "No currency found to do the rate exchange from ";
        private const String ERR_EMPTY_ARGS = "File path is missing.\n"+USAGE;
        private const String USAGE = "Usage -> LuccaDevises [path_to_the_data_file]";


        public void WriteErrorData(FileData data)
        {
            WriteError(ERR_DATA + data.StartingCurrency.Trigram + DASH + data.GoalCurrency.Trigram);
        }
        public void WriteError(String errMsg)
        {
            Console.WriteLine(ERR_HEADER + errMsg);
        }
        public void WriteResult(int result)
        {
            Console.WriteLine(result);
        }

        internal void WriteErrorArgs()
        {
            WriteError(ERR_EMPTY_ARGS);
        }

    }
}

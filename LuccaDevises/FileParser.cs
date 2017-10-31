using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LuccaDevises
{
    public class FileParser
    {
        private const char SEPARATOR = ';';
        private readonly string filePath;

        public FileParser(string pFilePath)
        {
            if (pFilePath != null && !String.IsNullOrEmpty(pFilePath))
            {
                filePath = pFilePath;
            }
            else
            {
                throw new DataFileFormatException(FormatExMessages.FILE_PATH_INVALID);
            }
        }

        public List<string> ReadFile()
        {
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line.Trim());
                }
            }
            return lines;
        }

        public FileData GetFileData(List<string> lines)
        {
            FileData data = new FileData();

            if(lines!=null && lines.Count() > 2)
            {
                //first line :
                string[] firstLine = lines[0].Split(SEPARATOR);
                if (firstLine != null && firstLine.Length == 3)
                {
                    data.Amount = int.Parse(firstLine[1]);
                    data.StartingCurrency = new Currency(firstLine[0]);
                    data.GoalCurrency = new Currency(firstLine[2]);
                }
                else { throw new DataFileFormatException(FormatExMessages.INVAL_FIRST_LINE); }

                //second line : 
                int exchangeRatesCount;
                if (int.TryParse(lines[1], out exchangeRatesCount))
                {
                    data.ExchangeRateCount = exchangeRatesCount;
                }
                else { throw new DataFileFormatException(FormatExMessages.INVAL_SECOND_LINE); }

                //rest of the lines : 
                data.ExchangeRates = new List<ExchangeRate>();
                for (int lineIndex = 2; lineIndex < lines.Count; lineIndex++)
                {
                    string[] currentLine = lines[lineIndex].Split(SEPARATOR);
                    if (currentLine != null && currentLine.Length == 3)
                    {
                        Currency initCurrency = new Currency(currentLine[0]);
                        Currency finalCurrency = new Currency(currentLine[1]);
                        double rate = double.Parse(currentLine[2], CultureInfo.InvariantCulture);

                        data.ExchangeRates.Add(new ExchangeRate(rate, initCurrency, finalCurrency));
                    }
                    else { throw new DataFileFormatException(FormatExMessages.INVAL_LINE+lineIndex); }

                }
            }
            else
            { throw new DataFileFormatException(FormatExMessages.FILE_EMPTY);}
            
            return data;
        }
    }

    public struct FormatExMessages
    {
        public const string FILE_PATH_INVALID = "Path to file is empty or invalid";
        public const string FILE_EMPTY = "File is empty!";
        public const string INVAL_FIRST_LINE = "First line format is invalid";
        public const string INVAL_SECOND_LINE = "Second line format is invalid";
        public const string INVAL_LINE = "Invalid format at line ";
    }

    public class DataFileFormatException : Exception
    {
        public DataFileFormatException(string message) : base(message)
        {
        }
    }
}

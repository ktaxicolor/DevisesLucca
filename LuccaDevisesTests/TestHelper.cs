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
        public const string VALID_TRIGRAM_2 = "VA2";
        public const string VALID_TRIGRAM_3 = "VA3";
        public const string STARTING_TRIGRAM = "TOP";
        public const string GOAL_TRIGRAM = "END";
        public const string INVALID_TRIGRAM = "INVA";

        public const double RATE = 0.4242;
        public const double RATE_SIX_DECIMALS = 0.123456;
        public const double RATE1 = 1.1111;
        public const double RATE2 = 2.2222;
        public const double RATE3 = 3.3333;

        public const int STARTING_AMOUNT = 1000;

        public const string PRINT_PARENT = "parent : ";
        public const string PRINT_INIT_CURR = "initCurrency : ";
        public const string PRINT_FINAL_CURR = "finalCurrency : ";
        public const string PRINT_RATE = "rate : ";
        public const string PRINT_CHILDREN_COUNT = "children count : ";
        public const string PRINT_NULL = "NULL";
        public const string PRINT_DELIM = "; ";
        public const string PRINT_NEW_LINE = "\n";



    }

    public static class TestHelper
    {

        public static ExchangeRateNode GetExchangeRateNode()
        {
            return new ExchangeRateNode(GetRootExchangeRateNode(), GetExchangeRate());
        }

        public static ExchangeRateNode GetExchangeRateNode(Currency initialCurr, Currency finalCurr)
        {
            return new ExchangeRateNode(GetRootExchangeRateNode(), GetExchangeRate(TestConst.RATE, initialCurr, finalCurr));
        }

        public static ExchangeRateNode GetChildExchangeRateNode(ExchangeRateNode parent)
        {
            return new ExchangeRateNode(parent, GetExchangeRate());
        }

        public static ExchangeRateNode GetChildExchangeRateNode(ExchangeRateNode parent, Currency initialCurr, Currency finalCurr)
        {
            return new ExchangeRateNode(parent, GetExchangeRate(TestConst.RATE, initialCurr, finalCurr));
        }


        public static ExchangeRateNode GetRootExchangeRateNode()
        {
            return new ExchangeRateNode(null, GetExchangeRate(0, new Currency(), GetCurrency(TestConst.STARTING_TRIGRAM)));
        }

        public static ExchangeRate GetExchangeRate()
        {
            return new ExchangeRate(TestConst.RATE, GetCurrency(TestConst.VALID_TRIGRAM), GetCurrency(TestConst.VALID_TRIGRAM_1));
        }

        public static ExchangeRate GetExchangeRate(double rate, Currency initCurr, Currency finalCurr)
        {
            return new ExchangeRate(rate, initCurr, finalCurr);
        }

        public static Currency GetCurrency(string trigram)
        {
            return new Currency(trigram);
        }

        public static FileData GetFileData()
        {
            var data = new FileData
            {
                Amount = TestConst.STARTING_AMOUNT,
                ExchangeRates = GetListExchangeRates(),
                ExchangeRateCount = GetListExchangeRates().Count,
                StartingCurrency = GetCurrency(TestConst.STARTING_TRIGRAM),
                GoalCurrency = GetCurrency(TestConst.GOAL_TRIGRAM)
            };

            return data;
        }

        public static List<ExchangeRate> GetListExchangeRates()
        {
            var list = new List<ExchangeRate>
            {
                GetExchangeRate(TestConst.RATE1, GetCurrency(TestConst.STARTING_TRIGRAM), GetCurrency(TestConst.VALID_TRIGRAM_1)),
                GetExchangeRate(TestConst.RATE1, GetCurrency(TestConst.VALID_TRIGRAM_1), GetCurrency(TestConst.VALID_TRIGRAM_2)),
                GetExchangeRate(TestConst.RATE2, GetCurrency(TestConst.GOAL_TRIGRAM), GetCurrency(TestConst.VALID_TRIGRAM_2)),
                GetExchangeRate(TestConst.RATE3, GetCurrency(TestConst.VALID_TRIGRAM_1), GetCurrency(TestConst.GOAL_TRIGRAM))
            };

            return list;
        }

        public static ExchangeRateNode GetABestNode()
        {
            var root = GetRootExchangeRateNode();
            root.AddChild(GetChildExchangeRateNode(root, GetCurrency(TestConst.STARTING_TRIGRAM), GetCurrency(TestConst.VALID_TRIGRAM_1)));
            var firstChild = root.Children.First();
            firstChild.AddChild(GetChildExchangeRateNode(firstChild, GetCurrency(TestConst.VALID_TRIGRAM_1), GetCurrency(TestConst.GOAL_TRIGRAM)));
            return firstChild.Children.First();
        }

        public static string PrintExchangeNodeTree(ExchangeRateNode root, string printedTree)
        {
            printedTree += PrintNodeInformations(root) + TestConst.PRINT_NEW_LINE;
            foreach(ExchangeRateNode child in root.Children)
            {
                if(child.Children.Count > 0)
                {
                    printedTree = PrintExchangeNodeTree(child, printedTree);
                }
                else
                {
                    printedTree += PrintNodeInformations(child) + TestConst.PRINT_NEW_LINE;
                }
            }
            return printedTree;
        }
        public static string PrintExchangeNodeTree(ExchangeRateNode root)
        {
            return PrintExchangeNodeTree(root, String.Empty);
        }

            public static string PrintNodeInformations(ExchangeRateNode node)
        {
            return TestConst.PRINT_PARENT + (node.Root == null ? TestConst .PRINT_NULL : PrintNodeDataCurrencies(node.Root.Data)) + TestConst.PRINT_DELIM
                    + TestConst.PRINT_INIT_CURR + node.Data.InitialCurrency.Trigram + TestConst.PRINT_DELIM
                    + TestConst.PRINT_FINAL_CURR + node.Data.FinalCurrency.Trigram + TestConst.PRINT_DELIM
                    + TestConst.PRINT_RATE + node.Data.Rate + TestConst.PRINT_DELIM
                    + TestConst.PRINT_CHILDREN_COUNT + node.Children.Count + TestConst.PRINT_DELIM;
        }

        public static string PrintNodeDataCurrencies(ExchangeRate data)
        {
            return data.InitialCurrency.Trigram + "-" + data.FinalCurrency.Trigram;
        }


    }
}

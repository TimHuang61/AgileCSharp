using System;
using System.Collections.Generic;

namespace Demo_SPR
{
    public class SimpleTradeParser : ITradeParser
    {
        private static float LotSize = 100000f;

        public List<TradeRecord> Parse(List<string> tradeData)
        {
            var trades = new List<TradeRecord>();
            var lineCount = 1;
            foreach (var line in tradeData)
            {
                var fields = line.Split(',');
                if (!ValidateTradeData(fields, lineCount))
                {
                    continue;
                }

                trades.Add(ConverTradDataToTradeRecord(fields));
                lineCount++;
            }

            return trades;
        }

        private TradeRecord ConverTradDataToTradeRecord(string[] fields)
        {
            var sourceCurrencyCode = fields[0].Substring(0, 3);
            var destinationCurrencyCode = fields[0].Substring(3, 3);
            var tradeAmount = Int32.Parse(fields[1]);
            var tradePrice = Decimal.Parse(fields[2]);

            return new TradeRecord
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = tradeAmount / LotSize,
                Price = tradePrice
            };
        }

        private bool ValidateTradeData(string[] fields, int lineCount)
        {
            if (fields.Length != 3)
            {
                Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.", lineCount, fields.Length);

                return false;
            }

            if (fields[0].Length != 6)
            {
                Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'", lineCount, fields[0]);

                return false;
            }

            int tradeAmount;
            if (!Int32.TryParse(fields[1], out tradeAmount))
            {
                Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);

                return false;
            }

            decimal tradePrice;
            if (!Decimal.TryParse(fields[2], out tradePrice))
            {
                Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);

                return false;
            }

            return true;
        }
    }
}
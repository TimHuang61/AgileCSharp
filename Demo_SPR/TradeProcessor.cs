using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Demo_SPR
{
    public class TradeProcessor
    {
        private static float LotSize = 100000f;

        public void ProcessTrades(Stream stream)
        {
            // read rows
            var tradeData = ReadTradeData(stream);
            var trades = ParseTrades(tradeData);
            StoreTrades(trades);
        }

        private static void StoreTrades(List<TradeRecord> trades)
        {
            using (var connection = new SqlConnection("Data Source=(local);Initial Catalog=TradeDatabase;Integrated Security=True;"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in trades)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                connection.Close();
            }

            Console.WriteLine("INFO: {0} trades processed", trades.Count);
        }

        private List<TradeRecord> ParseTrades(List<string> tradeData)
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
            var tradeAmount = int.Parse(fields[1]);
            var tradePrice = decimal.Parse(fields[2]);

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
            if (!int.TryParse(fields[1], out tradeAmount))
            {
                Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", lineCount, fields[1]);

                return false;
            }

            decimal tradePrice;
            if (!decimal.TryParse(fields[2], out tradePrice))
            {
                Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", lineCount, fields[2]);

                return false;
            }

            return true;
        }

        private List<string> ReadTradeData(Stream stream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines;
        }
    }
}

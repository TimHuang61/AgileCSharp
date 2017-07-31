﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Demo_SPR
{
    public class TradeProcessor
    {
        private readonly StreamTradeDataProvider streamTradeDataProvider = new StreamTradeDataProvider();
        private readonly SimpleTradeParser simpleTradeParser = new SimpleTradeParser();

        public void ProcessTrades(Stream stream)
        {
            // read rows
            var tradeData = streamTradeDataProvider.GetTradeData(stream);
            var trades = simpleTradeParser.Parse(tradeData);
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
    }
}

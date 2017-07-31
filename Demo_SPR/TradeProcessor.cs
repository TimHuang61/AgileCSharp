using System.Collections.Generic;
using System.IO;

namespace Demo_SPR
{
    public class TradeProcessor
    {
        private readonly StreamTradeDataProvider streamTradeDataProvider = new StreamTradeDataProvider();
        private readonly SimpleTradeParser simpleTradeParser = new SimpleTradeParser();
        private readonly AdoNetTradeStorage adoNetTradeStorage = new AdoNetTradeStorage();

        public void ProcessTrades(Stream stream)
        {
            // read rows
            var tradeData = streamTradeDataProvider.GetTradeData(stream);
            var trades = simpleTradeParser.Parse(tradeData);
            adoNetTradeStorage.Persist(trades);
        }
    }
}

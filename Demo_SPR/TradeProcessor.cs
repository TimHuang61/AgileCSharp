using System.Collections.Generic;
using System.IO;

namespace Demo_SPR
{
    public class TradeProcessor
    {
        // refactor: use Dependency Injection to this constructor
        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;

        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        public void ProcessTrades(Stream stream)
        {
            // read rows
            var tradeData = tradeDataProvider.GetTradeData(stream);
            var trades = tradeParser.Parse(tradeData);
            tradeStorage.Persist(trades);
        }
    }
}

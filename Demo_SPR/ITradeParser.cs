using System.Collections.Generic;

namespace Demo_SPR
{
    public interface ITradeParser
    {
        List<TradeRecord> Parse(List<string> tradeData);
    }
}
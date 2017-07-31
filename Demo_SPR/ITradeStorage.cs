using System.Collections.Generic;

namespace Demo_SPR
{
    public interface ITradeStorage
    {
        void Persist(List<TradeRecord> trades);
    }
}
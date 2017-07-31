using System.Collections.Generic;
using System.IO;

namespace Demo_SPR
{
    public interface ITradeDataProvider
    {
        List<string> GetTradeData(Stream stream);
    }
}
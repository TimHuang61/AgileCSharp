using System;
using System.Collections.Generic;
using System.IO;

namespace Demo_SPR
{
    public class StreamTradeDataProvider : ITradeDataProvider
    {
        public List<string> GetTradeData(Stream stream)
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
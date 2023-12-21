using System;

namespace CoinTrackApi.Domain
{
    public class CoinPrice
    {
        public string Symbol { get; set; }
        public string Currency { get; set; }
        public double Rate { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

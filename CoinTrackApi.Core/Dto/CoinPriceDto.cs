using System;

namespace CoinTrackApi.Application.Dto
{
    public class CoinPriceDto
    {
        public string Symbol { get; set; }
        public string Currency { get; set; }
        public double Rate { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

namespace CoinTrackApi.Domain
{
    public class CoinSetting
    {
        public CoinSetting(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}
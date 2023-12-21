using CoinTrackApi.Application;
using CoinTrackApi.Application.Interfaces;
using CoinTrackApi.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinTrackApi.Infrastructure.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        // Can use in-memory db context but for this simple case YAGNI
        private readonly List<Coin> _coins = new List<Coin>()
        {
            new Coin { Symbol = Constants.Ripple.Symbol, Name = Constants.Ripple.Name },
            new Coin { Symbol = Constants.Bitcoin.Symbol, Name = Constants.Bitcoin.Name },
            new Coin { Symbol = Constants.Ethereum.Symbol, Name = Constants.Ethereum.Name },
        };

        public async Task<Coin> Get(string symbol)
        {
            var coin = _coins.Find(c => string.Equals(symbol, c.Symbol, StringComparison.OrdinalIgnoreCase));
            return await Task.FromResult(coin);
        }

        public async Task<IEnumerable<Coin>> GetAll()
        {
            return await Task.FromResult(_coins.AsReadOnly());
        }
    }
}
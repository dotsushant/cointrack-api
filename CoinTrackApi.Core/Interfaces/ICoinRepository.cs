using CoinTrackApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinTrackApi.Application.Interfaces
{
    public interface ICoinRepository
    {
        Task<Coin> Get(string symbol);
        Task<IEnumerable<Coin>> GetAll();
    }
}

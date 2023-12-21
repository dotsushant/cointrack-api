using CoinTrackApi.Domain;
using System.Threading.Tasks;

namespace CoinTrackApi.Application.Interfaces
{
    public interface ICoinService
    {
        Task<CoinPrice> GetCoinPrice(string symbol);
    }
}
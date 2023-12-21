using CoinTrackApi.Domain;
using System.Threading.Tasks;

namespace CoinTrackApi.Application.Interfaces
{
    public interface ISettingRepository
    {
        Task<CoinSetting> GetCoinSetting();
        Task UpdateCoinSetting(CoinSetting coinSetting);
    }
}

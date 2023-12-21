using CoinTrackApi.Application;
using CoinTrackApi.Application.Interfaces;
using CoinTrackApi.Domain;
using System;
using System.Threading.Tasks;

namespace CoinTrackApi.Infrastructure.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly ICoinRepository _coinRepository;
        private CoinSetting _coinSetting = new CoinSetting(Constants.Bitcoin.Symbol);

        public SettingRepository(ICoinRepository coinRepository)
        {
            _coinRepository = coinRepository;
        }

        public async Task<CoinSetting> GetCoinSetting()
        {
            return await Task.FromResult(_coinSetting);
        }

        public async Task UpdateCoinSetting(CoinSetting coinSetting)
        {
            var coin = await _coinRepository.Get(coinSetting.Symbol);

            if (coin is null)
            {
                throw new Exception(Resource.FailedToUpdateCoinSetting);
            }

            _coinSetting = new CoinSetting(coin.Symbol);
        }
    }
}
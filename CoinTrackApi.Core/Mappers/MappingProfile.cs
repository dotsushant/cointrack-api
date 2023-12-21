using AutoMapper;
using CoinTrackApi.Application.Dto;
using CoinTrackApi.Domain;

namespace CoinTrackApi.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coin, CoinDto>();
            CreateMap<CoinPrice, CoinPriceDto>();
            CreateMap<CoinSetting, CoinSettingDto>();
        }
    }
}

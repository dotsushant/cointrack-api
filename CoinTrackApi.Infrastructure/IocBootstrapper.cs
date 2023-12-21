using CoinTrackApi.Application.Interfaces;
using CoinTrackApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoinTrackApi.Infrastructure
{
    public static class IocBootstrapper
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<ICoinRepository, CoinRepository>();
            services.AddSingleton<ISettingRepository, SettingRepository>();
        }
    }
}
using CoinTrackApi.Application.Interfaces;
using CoinTrackApi.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoinTrackApi.Application
{
    public static class IocBootstrapper
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<ICoinService, CoinService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
using CoinTrackApi.Application.Interfaces;
using CoinTrackApi.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinTrackApi.Application.Services
{
    public class CoinService : ICoinService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CoinService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<CoinPrice> GetCoinPrice(string code)
        {
            var response = await _httpClient.GetAsync($"{_configuration["CoinPriceUrlBase"]}{code}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var jsonContent = JsonConvert.DeserializeObject<dynamic>(content);
            
            return new CoinPrice
            {
                Symbol = code,
                Currency = jsonContent.sell,
                Rate = jsonContent.rate,
                Timestamp = jsonContent.timestamp,
                Value = jsonContent.ask
            };
        }
    }
}
using MediatR;
using System;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CoinTrackApi.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using CoinTrackApi.Application.Dto;
using CoinTrackApi.Core;

namespace CoinTrackApi.Application.Commands
{
    public class GetCoinPriceCommand : IRequest<CommandResult<CoinPriceDto>>
    {
        public GetCoinPriceCommand(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }

    public class GetCoinPriceCommandHandler : IRequestHandler<GetCoinPriceCommand,
        CommandResult<CoinPriceDto>>
    {
        private readonly ILogger<GetCoinPriceCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoinService _coinService;
        private readonly ICoinRepository _coinRepository;

        public GetCoinPriceCommandHandler(IMapper mapper,
            ILogger<GetCoinPriceCommandHandler> logger, ICoinService coinService, ICoinRepository coinRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _coinService = coinService;
            _coinRepository = coinRepository;
        }

        public async Task<CommandResult<CoinPriceDto>> Handle(GetCoinPriceCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var coin = await _coinRepository.Get(request.Symbol);

                if (coin is null)
                {
                    throw new Exception(Resource.FailedToRetrieveCoin);
                }

                var coinPrice = await _coinService.GetCoinPrice(coin.Symbol);
                var coinPriceDto = _mapper.Map<CoinPriceDto>(coinPrice);
                return new CommandResult<CoinPriceDto>(coinPriceDto);
            }
            catch (Exception e)
            {
                var failureReason = Resource.FailedToRetrieveCoinPrice;
                _logger.LogError(e, failureReason);
                return new CommandResult<CoinPriceDto>(failureReason);
            }
        }
    }
}
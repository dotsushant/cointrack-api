using MediatR;
using System;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CoinTrackApi.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using CoinDtos = System.Collections.Generic.List<CoinTrackApi.Application.Dto.CoinDto>;
using CoinTrackApi.Core;

namespace CoinTrackApi.Application.Commands
{
    public class GetCoinsCommand : IRequest<CommandResult<CoinDtos>> { }

    public class GetCoinsCommandHandler : IRequestHandler<GetCoinsCommand,
        CommandResult<CoinDtos>>
    {
        private readonly ILogger<GetCoinsCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ICoinRepository _coinRepository;

        public GetCoinsCommandHandler(IMapper mapper,
            ILogger<GetCoinsCommandHandler> logger, ICoinRepository coinRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _coinRepository = coinRepository;
        }

        public async Task<CommandResult<CoinDtos>> Handle(GetCoinsCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var coins = await _coinRepository.GetAll();
                var coinDtos = _mapper.Map<CoinDtos>(coins);
                return new CommandResult<CoinDtos>(coinDtos);
            }
            catch (Exception e)
            {
                var failureReason = Resource.FailedToRetrieveCoins;
                _logger.LogError(e, failureReason);
                return new CommandResult<CoinDtos>(failureReason);
            }
        }
    }
}
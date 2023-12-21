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
    public class GetCoinSettingCommand : IRequest<CommandResult<CoinSettingDto>>
    {
    }

    public class GetCoinSettingCommandHandler : IRequestHandler<GetCoinSettingCommand,
        CommandResult<CoinSettingDto>>
    {
        private readonly ILogger<GetCoinSettingCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISettingRepository _settingsRepository;

        public GetCoinSettingCommandHandler(IMapper mapper,
            ILogger<GetCoinSettingCommandHandler> logger, ISettingRepository settingsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _settingsRepository = settingsRepository;
        }

        public async Task<CommandResult<CoinSettingDto>> Handle(GetCoinSettingCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var coinSetting = await _settingsRepository.GetCoinSetting();
                var coinSettingDto = _mapper.Map<CoinSettingDto>(coinSetting);
                return new CommandResult<CoinSettingDto>(coinSettingDto);
            }
            catch (Exception e)
            {
                var failureReason = Resource.FailedToRetrieveCoinSetting;
                _logger.LogError(e, failureReason);
                return new CommandResult<CoinSettingDto>(failureReason);
            }
        }
    }
}
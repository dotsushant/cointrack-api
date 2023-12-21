using MediatR;
using System;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CoinTrackApi.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using CoinTrackApi.Application.Dto;
using CoinTrackApi.Domain;
using CoinTrackApi.Core;

namespace CoinTrackApi.Application.Commands
{
    public class UpdateCoinSettingCommand : IRequest<CommandResult<bool>>
    {
        public UpdateCoinSettingCommand(CoinSettingDto coinSettingDto)
        {
            CoinSetting = coinSettingDto;
        }

        public CoinSettingDto CoinSetting
        {
            get;
        }
    }

    public class UpdateSettingsCommandHandler : IRequestHandler<UpdateCoinSettingCommand,
        CommandResult<bool>>
    {
        private readonly ILogger<UpdateSettingsCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ISettingRepository _settingsRepository;

        public UpdateSettingsCommandHandler(IMapper mapper,
            ILogger<UpdateSettingsCommandHandler> logger, ISettingRepository settingsRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _settingsRepository = settingsRepository;
        }

        public async Task<CommandResult<bool>> Handle(UpdateCoinSettingCommand request,
            CancellationToken cancellationToken)
        {
            string failureReason;
            Exception failureException;

            try
            {
                await _settingsRepository.UpdateCoinSetting(new CoinSetting(request.CoinSetting.Symbol));
                return new CommandResult<bool>(true);
            }
            catch (Exception e)
            {
                failureException = e;
                failureReason = Resource.FailedToUpdateCoinSetting;
                _logger.LogError(failureException, failureReason);
                return new CommandResult<bool>(failureReason);
            }
        }
    }
}
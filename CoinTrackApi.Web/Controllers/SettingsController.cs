using CoinTrackApi.Application.Commands;
using CoinTrackApi.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoinTrackApi.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class SettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("settings/coin")]
        public async Task<IActionResult> GetCoinSetting()
        {
            var commandResult = await _mediator.Send(new GetCoinSettingCommand());
            return commandResult
                ? Ok(commandResult.Result)
                : StatusCode(500, new { commandResult.FailureReason });
        }


        [HttpPut("settings/coin")]
        public async Task<IActionResult> UpdateCoinSetting([FromBody] CoinSettingDto coinSettingDto)
        {
            var commandResult = await _mediator.Send(new UpdateCoinSettingCommand(coinSettingDto));
            return commandResult
                ? Ok(commandResult.Result)
                : StatusCode(500, new { commandResult.FailureReason });
        }
    }
}
using CoinTrackApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoinTrackApi.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class CoinsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoinsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("coins")]
        public async Task<IActionResult> GetCoins()
        {
            var commandResult = await _mediator.Send(new GetCoinsCommand());
            return commandResult
                ? Ok(commandResult.Result)
                : StatusCode(500, new { commandResult.FailureReason });
        }
    }
}
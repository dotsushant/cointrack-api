using CoinTrackApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoinTrackApi.Web.Controllers
{
    [ApiController]
    [Route("api")]
    public class PricesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PricesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("prices/{symbol}")]
        public async Task<IActionResult> GetCoinPrice(string symbol)
        {
            var commandResult = await _mediator.Send(new GetCoinPriceCommand(symbol));
            return commandResult
                ? Ok(commandResult.Result)
                : StatusCode(500, new { commandResult.FailureReason });
        }
    }
}
using Basket.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hangfire;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceControlController : ControllerBase
    {
        private readonly IPriceControlService _controlService;

        public PriceControlController(IPriceControlService controlService)
        {
            _controlService = controlService;
        }

        [HttpGet]
        [Route("id")]
        public async Task<ActionResult> GetPricesSeller(int id, CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate($"price-control-seller-{id}", () => _controlService.GetPricesSeller(id, cancellationToken), Cron.Daily);

            return Ok($"Price control started (for seller with id ={id} )!");
        }

        [HttpGet]
        public async Task<ActionResult> GetPricesSellers(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate("price-control-sellers", () => _controlService.GetPricesSellers(cancellationToken), Cron.Daily);

            return Ok($"Price control started (for all sellers)!");
        }
    }
}

using Catalog.Application.CQRS.ControlSeller.Query.ControlAllSellersQuery;
using Catalog.Application.CQRS.ControlSeller.Query.ControlByIdQuery;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlCountWigwamsController : BaseController
    {
        public ControlCountWigwamsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult> ControlSellers(CancellationToken cancellationToken)
        {
            var query = new ControlAllSellersQuery();
            RecurringJob.AddOrUpdate($"count-control-wigwams-for-all-sellers", () => Mediator.Send(query, cancellationToken), Cron.Daily);

            return Ok("Control started for all sellers!");
        }

        [HttpGet]
        [Route("id")]
        public async Task<ActionResult> ControlSeller(int id, CancellationToken cancellationToken)
        {
            var query = new ControlByIdSellerQuery(id);
            RecurringJob.AddOrUpdate($"count-control-wigwams-for-seller-{id}", () => Mediator.Send(query, cancellationToken), Cron.Daily);

            return Ok($"Control started for seller with id = {id}!");
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalog.Application.CQRS.Wigwam.Command.CreateCommand;
using Catalog.Application.CQRS.Wigwam.Command.DeleteCommand;
using Catalog.Application.CQRS.Wigwam.Command.UpdateCommand;
using Catalog.Application.CQRS.Wigwam.Query.GetAllQuery;
using Catalog.Application.CQRS.Wigwam.Query.GetByIdQuery;
using Catalog.Application.DTO;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WigwamsController : BaseController
    {
        public WigwamsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseWigwamModel>>> GetWigwamsInfos(CancellationToken cancellationToken)
        {
            var query = new GetAllQuery();
            var res = await Mediator.Send(query, cancellationToken);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseWigwamModel>> GetWigwamsInfo([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetWigwamByIdQuery(id);
            var result = await Mediator.Send(query, cancellationToken);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseWigwamModel>> CreateAsync([FromBody] CreateWigwamCommand model, CancellationToken cancellationToken)
        {
            var res = await Mediator.Send(model, cancellationToken);

            return Created(" ", res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteWigwamsInfo([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new DeleteWigwamByIdCommand(id);
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseWigwamModel>> UpdateWigwamsInfo([FromRoute] int id, [FromBody] UpdateWigwamCommand model, CancellationToken cancellationToken)
        {
            model.Id = id;
            var res = await Mediator.Send(model, cancellationToken);

            return Ok(res);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Catalog.Application.CQRS.Seller.Command.CreateCommand;
using Catalog.Application.CQRS.Seller.Command.DeleteCommand;
using Catalog.Application.CQRS.Seller.Command.UpdateCommand;
using Catalog.Application.CQRS.Seller.Query.GetAllQuery;
using Catalog.Application.CQRS.Seller.Query.GetByIdQuery;
using Catalog.Application.DTO;


namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerInfoController : BaseController
    {
        public SellerInfoController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseSellerModel>>> GetSellerInfos(CancellationToken cancellationToken)
        {
            var query = new GetAllSellerQuery();
            var res = await Mediator.Send(query, cancellationToken);

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseSellerModel>> GetSellerInfo([FromRoute] int id, CancellationToken cancellationToken)
        {
            var query = new GetByIdSellerQueryHandler(id);
            var result = await Mediator.Send(query, cancellationToken);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<ResponseSellerModel>> CreateAsync([FromBody] CreateSellerCommand model, 
            CancellationToken cancellationToken)
        {
            var res = await Mediator.Send(model, cancellationToken);

            return Created(" ", res);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteWigwamsInfo([FromRoute] int id, 
            CancellationToken cancellationToken)
        {
            var command = new DeleteSellerCommand(id);
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseSellerModel>> UpdateWigwamsInfo([FromRoute] int id, 
            [FromBody] UpdateSellerCommand model, CancellationToken cancellationToken)
        {
            model.Id = id;

            var res = await Mediator.Send(model, cancellationToken);

            return Ok(res);
        }
    }
}

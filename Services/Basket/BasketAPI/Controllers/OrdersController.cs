using Basket.Application.Interfaces;
using Basket.Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewOrderModel>>> GetOrders(CancellationToken cancellationToken)
        {
            var respose = await _service.GetAllAsync(cancellationToken);

            return Ok(respose);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewOrderModel>> GetOrder([FromRoute] int id, CancellationToken cancellationToken)
        {
            var respose = await _service.GetAsync(id, cancellationToken);

            return Ok(respose);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult<ViewOrderModel>> CreateAsync([FromBody] CreateOrderModel model, CancellationToken cancellationToken)
        {
            var respose = await _service.CreateAsync(model, cancellationToken);

            return Ok(respose);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<ActionResult> DeleteOrder([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _service.DeleteAsync(id, cancellationToken);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ViewOrderModel>> UpdateOrder([FromRoute] int id, [FromBody] UpdateOrderModel model, CancellationToken cancellationToken)
        {
            if (id != model.Id)
            {
                return BadRequest("Different Id entered!");
            }

            var respose = await _service.UpdateAsync(model, cancellationToken);

            return Ok(respose);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Ordering.API.Application.Database.Models;
using Ordering.API.Application.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetAsync(cancellationToken);
            return Ok(orders);
        }

        [HttpGet("{id:length(24)}", Name = "GetOrder")]
        public async Task<ActionResult<Order>> Get(string id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Create(Order order, CancellationToken cancellationToken)
        {
            await _orderService.CreateAsync(order, cancellationToken);

            return CreatedAtRoute("GetOrder", new { id = order.Id.ToString() }, order);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Order orderIn, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            // Update the Order ID to prevent the error
            // After applying the update, the (immutable) field '_id' was found to have been altered to _id: null
            orderIn.Id = id;
            await _orderService.UpdateAsync(id, orderIn, cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetAsync(id, cancellationToken);

            if (order == null)
            {
                return NotFound();
            }

            await _orderService.RemoveAsync(order.Id, cancellationToken);

            return NoContent();
        }
    }
}

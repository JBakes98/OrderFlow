using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Models;
using OrderFlow.Services.Handlers;

namespace OrderFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDynamoDBContext _context;
        private readonly IOrderHandler<Order> _createHandler;

        public OrderController(IDynamoDBContext context,
            IOrderHandler<Order> createHandler)
        {
            _context = context;
            _createHandler = createHandler;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var conditions = new List<ScanCondition>();
            var results = await _context.ScanAsync<Order>(conditions).GetRemainingAsync();

            return results;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            var order = await _context.LoadAsync<Order>(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
        
        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order request, CancellationToken cancellationToken)
        {
            var result = await _createHandler.HandleAsync(request, cancellationToken);

            if (!result.IsT0) 
                return BadRequest();
            
            var order = result.AsT0;
            
            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */

        // DELETE: api/Order/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool OrderExists(Guid id)
        {
            return _context.LoadAsync<Order>(id) == null;
        }
    }
}

using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Models;
using OrderFlow.Services.Handlers;

namespace OrderFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDynamoDBContext _context;
        private readonly IHandler<CreateOrder, Order> _createHandler;
        private readonly IHandler<Guid, Order> _getHandler;

        public OrderController(IDynamoDBContext context,
            IHandler<CreateOrder, Order> createHandler,
            IHandler<Guid, Order> getHandler)
        {
            _context = context;
            _createHandler = createHandler;
            _getHandler = getHandler;
        }

        // GET: api/Order
        [HttpGet]
        [Authorize("read:orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder(
            [FromQuery(Name = "instrument")] string instrumentId,
            [FromQuery(Name = "order-date")] DateTime orderDate
            )
        {
            var scanCondition = new ScanCondition("InstrumentId", ScanOperator.Equal, instrumentId.ToString());
            var conditions = new List<ScanCondition>() { scanCondition };
            var results = await _context.ScanAsync<Order>(conditions).GetRemainingAsync();

            return results;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _getHandler.HandleAsync(id, cancellationToken);

            return GetOrderResponse(result);
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostOrder(CreateOrder request, CancellationToken cancellationToken)
        {
            var result = await _createHandler.HandleAsync(request, cancellationToken);

            return CreateOrderResponse(result);
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

        private IActionResult CreateOrderResponse(OneOf<Order, Error> result)
        {
            return result.Match<IActionResult>(
                order => new ObjectResult(order),
                error => new ObjectResult(error));
        }

        private IActionResult GetOrderResponse(OneOf<Order, Error> result)
        {
            return result.Match<IActionResult>(
                order => new ObjectResult(order),
                error => new ObjectResult(error));
        }
    }
}

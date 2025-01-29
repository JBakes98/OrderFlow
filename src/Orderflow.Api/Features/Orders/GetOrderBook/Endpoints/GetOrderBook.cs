using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Orders.Common.Interfaces;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.GetOrderBook.Contracts;

namespace Orderflow.Features.Orders.GetOrderBook.Endpoints;

public static class GetOrderBook
{
    public static async Task<Results<Ok<GetOrderBookResponse>, ProblemHttpResult>> Handle(
        IOrderBookManager orderBookManager,
        IMapper<Order, OrderBooksOrderResponse> orderToResponseMapper,
        Guid id)
    {
        var orderBook = orderBookManager.GetOrderBook(id);
        var (bids, asks) = orderBook.GetOrderBook();

        var bidsResponse = bids.Select(orderToResponseMapper.Map);
        var asksResponse = asks.Select(orderToResponseMapper.Map);

        var response = new GetOrderBookResponse
        {
            Bids = bidsResponse,
            Asks = asksResponse
        };

        return TypedResults.Ok(response);
    }
}
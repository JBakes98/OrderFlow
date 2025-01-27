using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;
using Orderflow.Features.Orders.GetOrder.Contracts;
using Orderflow.Features.Orders.ListOrders.Services;

namespace Orderflow.Features.Orders.ListOrders.Endpoints;

public static class ListOrders
{
    public static async Task<Results<Ok<IEnumerable<GetOrderResponse>>, ProblemHttpResult>> Handle(
        IListOrdersService listOrdersService,
        IMapper<Order, GetOrderResponse> orderToResponseMapper)
    {
        var result = await listOrdersService.ListOrders();

        if (result.TryPickT1(out var error, out var orders))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orders.Select(orderToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
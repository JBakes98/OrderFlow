using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Extensions;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Order.Endpoints;

public static class ListOrder
{
    public static async Task<Results<Ok<IEnumerable<GetOrderResponse>>, ProblemHttpResult>> Handle(
        IOrderService orderService,
        IMapper<Domain.Models.Order, GetOrderResponse> orderToResponseMapper)
    {
        var result = await orderService.RetrieveOrders();

        if (result.TryPickT1(out var error, out var orders))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orders.Select(orderToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
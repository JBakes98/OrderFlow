using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.GetOrder.Contracts;
using Orderflow.Features.Orders.GetOrder.Services;

namespace Orderflow.Features.Orders.GetOrder.Endpoints;

public static class GetOrder
{
    public static async Task<Results<Ok<GetOrderResponse>, ProblemHttpResult>> Handle(
        IGetOrderService getOrderService,
        IMapper<Order, GetOrderResponse> orderToResponseMapper,
        string id)
    {
        var result = await getOrderService.GetOrder(id);

        if (result.TryPickT1(out var error, out var order))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orderToResponseMapper.Map(order);

        return TypedResults.Ok(response);
    }
}
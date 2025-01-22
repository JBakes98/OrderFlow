using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Order.GetOrder.Services;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Order.Endpoints;

public static class GetOrder
{
    public static async Task<Results<Ok<GetOrderResponse>, ProblemHttpResult>> Handle(
        IGetOrderService getOrderService,
        IMapper<Domain.Models.Order, GetOrderResponse> orderToResponseMapper,
        string id)
    {
        var result = await getOrderService.GetOrder(id);

        if (result.TryPickT1(out var error, out var order))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orderToResponseMapper.Map(order);

        return TypedResults.Ok(response);
    }
}
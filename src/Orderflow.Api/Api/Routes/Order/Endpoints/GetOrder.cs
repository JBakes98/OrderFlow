using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Extensions;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Order.Endpoints;

public static class GetOrder
{
    public static async Task<Results<Ok<GetOrderResponse>, ProblemHttpResult>> Handle(
        IOrderService orderService,
        IMapper<Domain.Models.Order, GetOrderResponse> orderToResponseMapper,
        string id)
    {
        var result = await orderService.RetrieveOrder(id);

        if (result.TryPickT1(out var error, out var order))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orderToResponseMapper.Map(order);

        return TypedResults.Ok(response);
    }
}
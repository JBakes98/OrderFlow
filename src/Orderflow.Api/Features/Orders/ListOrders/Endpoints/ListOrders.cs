using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Order.ListOrders.Services;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Order.Endpoints;

public static class ListOrders
{
    public static async Task<Results<Ok<IEnumerable<GetOrderResponse>>, ProblemHttpResult>> Handle(
        IListOrdersService listOrdersService,
        IMapper<Domain.Models.Order, GetOrderResponse> orderToResponseMapper)
    {
        var result = await listOrdersService.ListOrders();

        if (result.TryPickT1(out var error, out var orders))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orders.Select(orderToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
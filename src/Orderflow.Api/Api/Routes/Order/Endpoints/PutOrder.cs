using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Commands;
using Orderflow.Mappers;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Order.Endpoints;

public static class PutOrder
{
    public static async Task<Results<Ok<GetOrderResponse>, ProblemHttpResult>> Handle(
        IOrderService orderService,
        IValidator<PutOrderRequest> validator,
        IMapper<PutOrderRequest, OrderUpdateCommand> putOrderRequestToOrderUpdateCommandMapper,
        IMapper<Domain.Models.Order, GetOrderResponse> orderToOrderResponseMapper,
        [FromBody] PutOrderRequest request,
        string id)
    {
        request.SetOrderId(id);

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return TypedResults.Problem(string.Join(",", validationResult.Errors),
                statusCode: (int)HttpStatusCode.BadRequest);

        var command = putOrderRequestToOrderUpdateCommandMapper.Map(request);

        var result = await orderService.UpdateOrder(command);

        if (result.TryPickT1(out var error, out var order))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes));

        var response = orderToOrderResponseMapper.Map(order);

        return TypedResults.Ok(response);
    }
}
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Mappers;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Order.Endpoints;

public static class PostOrder
{
    public static async Task<Results<Ok<GetOrderResponse>, ProblemHttpResult>> Handle(
        IValidator<PostOrderRequest> validator,
        IOrderService orderService,
        IMapper<PostOrderRequest, Domain.Models.Order> postOrderRequestToOrderMapper,
        IMapper<Domain.Models.Order, GetOrderResponse> orderToOrderResponseMapper,
        [FromBody] PostOrderRequest orderRequest)
    {
        var validationResult = await validator.ValidateAsync(orderRequest);
        if (!validationResult.IsValid)
            return TypedResults.Problem(string.Join(",", validationResult.Errors),
                statusCode: (int)HttpStatusCode.BadRequest);

        var order = postOrderRequestToOrderMapper.Map(orderRequest);

        var result = await orderService.CreateOrder(order);

        if (result.TryPickT1(out var error, out _))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orderToOrderResponseMapper.Map(order);

        return TypedResults.Ok(response);
    }
}
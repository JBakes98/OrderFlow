using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Extensions;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.CreateOrder.Contracts;
using Orderflow.Features.Orders.CreateOrder.Services;
using Orderflow.Features.Orders.GetOrder.Contracts;

namespace Orderflow.Features.Orders.CreateOrder.Endpoints;

public static class PostOrder
{
    public static async Task<Results<Created<GetOrderResponse>, ProblemHttpResult>> Handle(
        HttpContext context,
        IValidator<PostOrderRequest> validator,
        ICreateOrderService createOrderService,
        IMapper<PostOrderRequest, Order> postOrderRequestToOrderMapper,
        IMapper<Order, GetOrderResponse> orderToOrderResponseMapper,
        [FromBody] PostOrderRequest orderRequest)
    {
        var validationResult = await validator.ValidateAsync(orderRequest);
        if (!validationResult.IsValid)
            return TypedResults.Problem(string.Join(",", validationResult.Errors),
                statusCode: (int)HttpStatusCode.BadRequest);

        var order = postOrderRequestToOrderMapper.Map(orderRequest);

        var result = await createOrderService.CreateOrder(order);

        if (result.TryPickT1(out var error, out _))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = orderToOrderResponseMapper.Map(order);

        var uri = UriExtensions.GenerateUri(context, "orders", response.Id);
        return TypedResults.Created(uri, response);
    }
}
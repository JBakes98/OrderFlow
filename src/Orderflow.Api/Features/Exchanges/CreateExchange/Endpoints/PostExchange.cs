using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Extensions;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Exchange.Endpoints;

public static class PostExchange
{
    public static async Task<Results<Created<GetExchangeResponse>, ProblemHttpResult>> Handle(
        HttpContext context,
        IValidator<PostExchangeRequest> validator,
        ICreateExchangeService createExchangeService,
        IMapper<PostExchangeRequest, Domain.Models.Exchange> postExchangeToExchangeMapper,
        IMapper<Domain.Models.Exchange, GetExchangeResponse> exchangeToGetExchangeResponseMapper,
        [FromBody] PostExchangeRequest exchangeRequest)
    {
        var validationResult = await validator.ValidateAsync(exchangeRequest);
        if (!validationResult.IsValid)
            return TypedResults.Problem(string.Join(",", validationResult.Errors),
                statusCode: (int)HttpStatusCode.BadRequest);

        var exchange = postExchangeToExchangeMapper.Map(exchangeRequest);

        var result = await createExchangeService.CreateExchange(exchange);

        if (result.TryPickT1(out var error, out _))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes));

        var response = exchangeToGetExchangeResponseMapper.Map(exchange);

        var uri = UriExtensions.GenerateUri(context, "exchanges", response.Id);
        return TypedResults.Created(uri, response);
    }
}
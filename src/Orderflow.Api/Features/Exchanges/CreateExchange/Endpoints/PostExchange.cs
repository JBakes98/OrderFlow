using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Extensions;
using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.CreateExchange.Contracts;
using Orderflow.Features.Exchanges.CreateExchange.Services;
using Orderflow.Features.Exchanges.GetExchange.Contracts;

namespace Orderflow.Features.Exchanges.CreateExchange.Endpoints;

public static class PostExchange
{
    public static async Task<Results<Created<GetExchangeResponse>, ProblemHttpResult>> Handle(
        HttpContext context,
        IValidator<PostExchangeRequest> validator,
        ICreateExchangeService createExchangeService,
        IMapper<PostExchangeRequest, Features.Exchanges.Common.Exchange> postExchangeToExchangeMapper,
        IMapper<Features.Exchanges.Common.Exchange, GetExchangeResponse> exchangeToGetExchangeResponseMapper,
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
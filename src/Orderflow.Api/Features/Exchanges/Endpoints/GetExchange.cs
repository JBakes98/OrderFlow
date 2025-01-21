using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Exchange.Endpoints;

public static class GetExchange
{
    public static async Task<Results<Ok<GetExchangeResponse>, ProblemHttpResult>> Handle(
        IExchangeService exchangeService,
        IMapper<Domain.Models.Exchange, GetExchangeResponse> exchangeToResponseMapper,
        Guid id)
    {
        var result = await exchangeService.GetExchangeById(id);

        if (result.TryPickT1(out var error, out var exchange))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = exchangeToResponseMapper.Map(exchange);

        return TypedResults.Ok(response);
    }
}
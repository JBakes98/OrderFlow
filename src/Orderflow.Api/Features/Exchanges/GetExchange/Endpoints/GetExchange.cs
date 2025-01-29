using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.GetExchange.Contracts;
using Orderflow.Features.Exchanges.GetExchange.Services;

namespace Orderflow.Features.Exchanges.GetExchange.Endpoints;

public static class GetExchange
{
    public static async Task<Results<Ok<GetExchangeResponse>, ProblemHttpResult>> Handle(
        IGetExchangeService getExchangeService,
        IMapper<Exchange, GetExchangeResponse> exchangeToResponseMapper,
        Guid id)
    {
        var result = await getExchangeService.GetExchangeById(id);

        if (result.TryPickT1(out var error, out var exchange))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = exchangeToResponseMapper.Map(exchange);

        return TypedResults.Ok(response);
    }
}
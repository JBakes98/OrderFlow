using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.GetExchange.Contracts;
using Orderflow.Features.Exchanges.ListExchanges.Services;

namespace Orderflow.Features.Exchanges.ListExchanges.Endpoints;

public static class ListExchange
{
    public static async Task<Results<Ok<IEnumerable<GetExchangeResponse>>, ProblemHttpResult>> Handle(
        IListExchangesService listExchangesService,
        IMapper<Exchange, GetExchangeResponse> exchangeToResponseMapper)
    {
        var result = await listExchangesService.ListExchanges();

        if (result.TryPickT1(out var error, out var exchanges))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = exchanges.Select(exchangeToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
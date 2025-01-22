using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Features.Exchanges.GetExchange.Services;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Exchange.Endpoints;

public static class ListExchange
{
    public static async Task<Results<Ok<IEnumerable<GetExchangeResponse>>, ProblemHttpResult>> Handle(
        IGetExchangeService createExchangeService,
        IMapper<Domain.Models.Exchange, GetExchangeResponse> exchangeToResponseMapper)
    {
        var result = await createExchangeService.GetExchanges();

        if (result.TryPickT1(out var error, out var exchanges))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = exchanges.Select(exchangeToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
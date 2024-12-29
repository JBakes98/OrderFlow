using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Mappers;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Instrument.Endpoints;

public static class ListInstrument
{
    public static async Task<Results<Ok<IEnumerable<GetInstrumentResponse>>, ProblemHttpResult>> Handle(
        IInstrumentService instrumentService,
        IMapper<Domain.Models.Instrument, GetInstrumentResponse> instrumentToResponseMapper)
    {
        var result = await instrumentService.RetrieveInstruments();

        if (result.TryPickT1(out var error, out var instruments))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instruments.Select(instrumentToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
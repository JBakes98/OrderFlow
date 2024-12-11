using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Mappers;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Instrument.Endpoints;

public static class GetInstrument
{
    public static async Task<Results<Ok<GetInstrumentResponse>, ProblemHttpResult>> Handle(
        IInstrumentService instrumentService,
        IMapper<Domain.Models.Instrument, GetInstrumentResponse> instrumentToResponseMapper,
        string id)
    {
        var result = await instrumentService.RetrieveInstrument(id);

        if (result.TryPickT1(out var error, out var instrument))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instrumentToResponseMapper.Map(instrument);

        return TypedResults.Ok(response);
    }
}
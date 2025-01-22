using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Api.Routes.Instrument.GetInstrument.Services;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Mappers;
using Orderflow.Services;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Instrument.Endpoints;

public static class GetInstrument
{
    public static async Task<Results<Ok<GetInstrumentResponse>, ProblemHttpResult>> Handle(
        IGetInstrumentService getInstrumentService,
        IMapper<Domain.Models.Instrument, GetInstrumentResponse> instrumentToResponseMapper,
        Guid id)
    {
        var result = await getInstrumentService.GetInstrument(id);

        if (result.TryPickT1(out var error, out var instrument))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instrumentToResponseMapper.Map(instrument);

        return TypedResults.Ok(response);
    }
}
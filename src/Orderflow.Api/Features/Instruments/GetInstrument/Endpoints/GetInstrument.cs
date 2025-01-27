using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.GetInstrument.Contracts;
using Orderflow.Features.Instruments.GetInstrument.Services;

namespace Orderflow.Features.Instruments.GetInstrument.Endpoints;

public static class GetInstrument
{
    public static async Task<Results<Ok<GetInstrumentResponse>, ProblemHttpResult>> Handle(
        IGetInstrumentService getInstrumentService,
        IMapper<Instrument, GetInstrumentResponse> instrumentToResponseMapper,
        Guid id)
    {
        var result = await getInstrumentService.GetInstrument(id);

        if (result.TryPickT1(out var error, out var instrument))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instrumentToResponseMapper.Map(instrument);

        return TypedResults.Ok(response);
    }
}
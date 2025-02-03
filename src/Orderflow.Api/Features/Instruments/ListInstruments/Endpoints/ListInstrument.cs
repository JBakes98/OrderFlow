using Microsoft.AspNetCore.Http.HttpResults;
using Orderflow.Common.Mappers;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.GetInstrument.Contracts;
using Orderflow.Features.Instruments.ListInstruments.Services;

namespace Orderflow.Features.Instruments.ListInstruments.Endpoints;

public static class ListInstrument
{
    public static async Task<Results<Ok<IEnumerable<GetInstrumentResponse>>, ProblemHttpResult>> Handle(
        IListInstrumentsService listInstrumentsService,
        IMapper<Instrument, GetInstrumentResponse> instrumentToResponseMapper)
    {
        var result = await listInstrumentsService.ListInstruments();

        if (result.TryPickT1(out var error, out var instruments))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instruments.Select(instrumentToResponseMapper.Map);

        return TypedResults.Ok(response);
    }
}
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Mappers;
using Orderflow.Services;

namespace Orderflow.Api.Routes.Instrument.Endpoints;

public static class PostInstrument
{
    public static async Task<Results<Ok<GetInstrumentResponse>, ProblemHttpResult>> Handle(
        IInstrumentService instrumentService,
        IMapper<PostInstrumentRequest, Domain.Models.Instrument> postInstrumentRequestToInstrumentMapper,
        IMapper<Domain.Models.Instrument, GetInstrumentResponse> instrumentToGetInstrumentResponseMapper,
        [FromBody] PostInstrumentRequest instrumentRequest)
    {
        var instrument = postInstrumentRequestToInstrumentMapper.Map(instrumentRequest);

        var result = await instrumentService.CreateInstrument(instrument);

        if (result.TryPickT1(out var error, out _))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instrumentToGetInstrumentResponseMapper.Map(instrument);

        return TypedResults.Ok(response);
    }
}
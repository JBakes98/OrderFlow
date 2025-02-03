using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Common.Extensions;
using Orderflow.Common.Mappers;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.CreateInstrument.Contracts;
using Orderflow.Features.Instruments.CreateInstrument.Services;
using Orderflow.Features.Instruments.GetInstrument.Contracts;

namespace Orderflow.Features.Instruments.CreateInstrument.Endpoints;

public static class PostInstrument
{
    public static async Task<Results<Created<GetInstrumentResponse>, ProblemHttpResult>> Handle(
        HttpContext context,
        ICreateInstrumentService createInstrumentService,
        IMapper<PostInstrumentRequest, Instrument> postInstrumentRequestToInstrumentMapper,
        IMapper<Instrument, GetInstrumentResponse> instrumentToGetInstrumentResponseMapper,
        [FromBody] PostInstrumentRequest instrumentRequest)
    {
        var instrument = postInstrumentRequestToInstrumentMapper.Map(instrumentRequest);

        var result = await createInstrumentService.CreateInstrument(instrument);

        if (result.TryPickT1(out var error, out _))
            return TypedResults.Problem(string.Join(",", error.ErrorCodes), statusCode: (int)error.ErrorType);

        var response = instrumentToGetInstrumentResponseMapper.Map(instrument);

        var uri = UriExtensions.GenerateUri(context, "instruments", response.Id);
        return TypedResults.Created(uri, response);
    }
}
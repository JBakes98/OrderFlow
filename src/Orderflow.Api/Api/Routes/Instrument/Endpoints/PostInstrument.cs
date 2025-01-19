using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Extensions;
using Orderflow.Mappers;
using Orderflow.Services;
using Orderflow.Services.Interfaces;

namespace Orderflow.Api.Routes.Instrument.Endpoints;

public static class PostInstrument
{
    public static async Task<Results<Created<GetInstrumentResponse>, ProblemHttpResult>> Handle(
        HttpContext context,
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

        var uri = UriExtensions.GenerateUri(context, "instruments", response.Id);
        return TypedResults.Created(uri, response);
    }
}
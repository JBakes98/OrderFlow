// using System.Net;
// using FluentValidation;
// using FluentValidation.Results;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Moq;
// using OneOf;
// using Orderflow.Api.Routes.Exchange.Endpoints;
// using Orderflow.Api.Routes.Exchange.Models;
// using Orderflow.Domain.Models;
// using Orderflow.Features.Exchanges.Common;
// using Orderflow.Mappers;
// using Orderflow.Services.Interfaces;
//
// namespace Orderflow.Api.Unit.Tests.Features.Exchanges.CreateExchange.Endpoints;
//
// public class PostExchangeTests
// {
//     [Fact]
//     public async Task Handle_ShouldReturnCreated_WhenRequestIsValid()
//     {
//         // Arrange
//         var context = new DefaultHttpContext();
//         var validatorMock = new Mock<IValidator<PostExchangeRequest>>();
//         var createExchangeServiceMock = new Mock<ICreateExchangeService>();
//         var postExchangeToExchangeMapperMock = new Mock<IMapper<PostExchangeRequest, Exchange>>();
//         var exchangeToGetExchangeResponseMapperMock = new Mock<IMapper<Exchange, GetExchangeResponse>>();
//
//         var exchangeRequest = new PostExchangeRequest("Test Exchange", "TEST", "TST", "US");
//         var exchange = new Exchange(Guid.NewGuid(), "Test Exchange", "TEST", "TST", "US");
//         var exchangeResponse = new GetExchangeResponse(exchange.Id.ToString(), exchange.Name, exchange.Abbreviation, exchange.Mic, exchange.Region);
//
//         validatorMock.Setup(v => v.ValidateAsync(exchangeRequest, default))
//             .ReturnsAsync(new ValidationResult());
//
//         postExchangeToExchangeMapperMock.Setup(m => m.Map(exchangeRequest))
//             .Returns(exchange);
//
//         createExchangeServiceMock.Setup(s => s.CreateExchange(exchange))
//             .ReturnsAsync(OneOf<Exchange, Error>.FromT0(exchange));
//
//         exchangeToGetExchangeResponseMapperMock.Setup(m => m.Map(exchange))
//             .Returns(exchangeResponse);
//
//         context.Request.Scheme = "http";
//         context.Request.Host = new HostString("localhost");
//
//         // Act
//         var result = await PostExchange.Handle(
//             context,
//             validatorMock.Object,
//             createExchangeServiceMock.Object,
//             postExchangeToExchangeMapperMock.Object,
//             exchangeToGetExchangeResponseMapperMock.Object,
//             exchangeRequest);
//
//         // Assert
//         Assert.IsType<Created<GetExchangeResponse>>(result);
//         var createdResult = result as Created<GetExchangeResponse>;
//         Assert.NotNull(createdResult);
//         Assert.Equal(exchangeResponse, createdResult.Value);
//     }
//
//     [Fact]
//     public async Task Handle_ShouldReturnBadRequest_WhenValidationFails()
//     {
//         // Arrange
//         var context = new DefaultHttpContext();
//         var validatorMock = new Mock<IValidator<PostExchangeRequest>>();
//         var createExchangeServiceMock = new Mock<ICreateExchangeService>();
//         var postExchangeToExchangeMapperMock = new Mock<IMapper<PostExchangeRequest, Exchange>>();
//         var exchangeToGetExchangeResponseMapperMock = new Mock<IMapper<Exchange, GetExchangeResponse>>();
//
//         var exchangeRequest = new PostExchangeRequest("Test Exchange", "TEST", "TST", "US");
//
//         validatorMock.Setup(v => v.ValidateAsync(exchangeRequest, default))
//             .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("Property", "Error message") }));
//
//         // Act
//         var result = await PostExchange.Handle(
//             context,
//             validatorMock.Object,
//             createExchangeServiceMock.Object,
//             postExchangeToExchangeMapperMock.Object,
//             exchangeToGetExchangeResponseMapperMock.Object,
//             exchangeRequest);
//
//         // Assert
//         Assert.IsType<ProblemHttpResult>(result);
//         var problemResult = result.Result;
//         Assert.NotNull(problemResult);
//         Assert.Equal((int)HttpStatusCode.BadRequest, problemResult.);
//     }
//
//     [Fact]
//     public async Task Handle_ShouldReturnProblem_WhenCreateExchangeFails()
//     {
//         // Arrange
//         var context = new DefaultHttpContext();
//         var validatorMock = new Mock<IValidator<PostExchangeRequest>>();
//         var createExchangeServiceMock = new Mock<ICreateExchangeService>();
//         var postExchangeToExchangeMapperMock = new Mock<IMapper<PostExchangeRequest, Exchange>>();
//         var exchangeToGetExchangeResponseMapperMock = new Mock<IMapper<Exchange, GetExchangeResponse>>();
//
//         var exchangeRequest = new PostExchangeRequest("Test Exchange", "TEST", "TST", "US");
//         var exchange = new Exchange(Guid.NewGuid(), "Test Exchange", "TEST", "TST", "US");
//         var error = new Error(HttpStatusCode.InternalServerError,"ErrorCode");
//
//         validatorMock.Setup(v => v.ValidateAsync(exchangeRequest, default))
//             .ReturnsAsync(new ValidationResult());
//
//         postExchangeToExchangeMapperMock.Setup(m => m.Map(exchangeRequest))
//             .Returns(exchange);
//
//         createExchangeServiceMock.Setup(s => s.CreateExchange(exchange))
//             .ReturnsAsync(OneOf<Exchange, Error>.FromT1(error));
//
//         // Act
//         var result = await PostExchange.Handle(
//             context,
//             validatorMock.Object,
//             createExchangeServiceMock.Object,
//             postExchangeToExchangeMapperMock.Object,
//             exchangeToGetExchangeResponseMapperMock.Object,
//             exchangeRequest);
//
//         // Assert
//         Assert.IsType<ProblemHttpResult>(result);
//         var problemResult = (ProblemHttpResult)result;
//         Assert.NotNull(problemResult);
//         Assert.Contains("ErrorCode", problemResult.Detail);
//     }
// }


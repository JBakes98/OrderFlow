using System.Net;
using AutoFixture.Xunit2;
using Moq;
using Moq.Protected;
using OrderFlow.Contracts.Responses.AlphaVantage;
using OrderFlow.Extensions;
using OrderFlow.Options;
using OrderFlow.Services.AlphaVantage;
using HttpClient = System.Net.Http.HttpClient;

namespace OrderFlow.Api.Unit.Tests.Services.AlphaVantage;

public class AlphaVantageServiceTests
{
    [Theory, AutoMoqData]
    public async void Should_Get_StockQuote_From_AlphaVantage(
        [Frozen] Mock<IHttpClientFactory> httpClientFactoryMock,
        [Frozen] Mock<HttpMessageHandler> httpMessageHandlerMock,
        [Frozen] AlphaVantageOptions options,
        AlphaVantageService sut
    )
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(
                "{\n    \"Global Quote\": {\n        \"01. symbol\": \"IBM\",\n        \"02. open\": \"170.8500\",\n        \"03. high\": \"173.4600\",\n        \"04. low\": \"170.5300\",\n        \"05. price\": \"172.9500\",\n        \"06. volume\": \"4193459\",\n        \"07. latest trading day\": \"2024-06-28\",\n        \"08. previous close\": \"170.8500\",\n        \"09. change\": \"2.1000\",\n        \"10. change percent\": \"1.2291%\"\n    }\n}")
        };

        httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://www.alphavantage.co/")
            });

        var quoteResponse = await sut.GetStockQuote("AAPL");

        Assert.True(quoteResponse.IsT0);
    }
}
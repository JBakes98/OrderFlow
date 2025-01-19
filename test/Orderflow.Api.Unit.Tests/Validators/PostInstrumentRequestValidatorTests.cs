using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Validators;

namespace Orderflow.Api.Unit.Tests.Validators;

public class PostInstrumentRequestValidatorTests
{
    private readonly PostInstrumentRequestValidator _sut = new();

    [Fact]
    public void Validate_ShouldReturnValid_WhenRequestIsValid()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "AAPL",
            name: "Apple Inc.",
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenExchangeIdIsEmpty()
    {
        var request = new PostInstrumentRequest(
            exchangeId: string.Empty,
            ticker: "AAPL",
            name: "Apple Inc.",
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("ExchangeId required", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenExchangeIdIsInvalid()
    {
        var request = new PostInstrumentRequest(
            exchangeId: "invalid-guid",
            ticker: "AAPL",
            name: "Apple Inc.",
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("InstrumentId invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenTickerIsEmpty()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: string.Empty,
            name: "Apple Inc.",
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Ticker required", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenTickerIsTooShort()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "A",
            name: "Apple Inc.",
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Ticker invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenTickerIsTooLong()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "AAPL123",
            name: "Apple Inc.",
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Ticker invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenNameIsEmpty()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "AAPL",
            name: string.Empty,
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Name required", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenNameIsTooLong()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "AAPL",
            name: new string('A', 257), // 257 chars
            sector: "Technology");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Name invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenSectorIsEmpty()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "AAPL",
            name: "Apple Inc.",
            sector: string.Empty);

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Sector required", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenSectorIsTooLong()
    {
        var request = new PostInstrumentRequest(
            exchangeId: Guid.NewGuid().ToString(),
            ticker: "AAPL",
            name: "Apple Inc.",
            sector: new string('A', 257)); // 257 chars

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains("Sector invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
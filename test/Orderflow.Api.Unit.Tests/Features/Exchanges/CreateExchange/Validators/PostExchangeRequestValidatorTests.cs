using Orderflow.Features.Exchanges.CreateExchange.Contracts;
using Orderflow.Features.Exchanges.CreateExchange.Validators;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.CreateExchange.Validators;

public class PostExchangeRequestValidatorTests
{
    private readonly PostExchangeRequestValidator _sut = new();

    [Fact]
    public void Validate_ShouldHaveError_WhenNameIsEmpty()
    {
        // Arrange
        var request = new PostExchangeRequest(name: "", abbreviation: "AB", mic: "ABCD", region: "NA");

        // Act
        var result = _sut.Validate(request);

        // Assert
        var error = result.Errors.FirstOrDefault(x => x.PropertyName == "Name");
        Assert.NotNull(error);
        Assert.Equal("Name required", error.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenAbbreviationIsInvalid()
    {
        // Arrange with too short abbreviation
        var request = new PostExchangeRequest(name: "Exchange", abbreviation: "A", mic: "ABCD", region: "NA");

        // Act
        var result = _sut.Validate(request);

        // Assert for invalid abbreviation (too short)
        var error = result.Errors.FirstOrDefault(x => x.PropertyName == "Abbreviation");
        Assert.NotNull(error);
        Assert.Equal("Abbreviation invalid", error.ErrorMessage);

        // Arrange with too long abbreviation
        request = new PostExchangeRequest(name: "Exchange", abbreviation: new string('A', 51), mic: "ABCD",
            region: "NA");

        // Act
        result = _sut.Validate(request);

        // Assert for invalid abbreviation (too long)
        error = result.Errors.FirstOrDefault(x => x.PropertyName == "Abbreviation");
        Assert.NotNull(error);
        Assert.Equal("Abbreviation invalid", error.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenMicIsInvalid()
    {
        // Arrange with incorrect mic length
        var request = new PostExchangeRequest(name: "Exchange", abbreviation: "AB", mic: "123", region: "NA");

        // Act
        var result = _sut.Validate(request);

        // Assert for invalid mic length
        var error = result.Errors.FirstOrDefault(x => x.PropertyName == "Mic");
        Assert.NotNull(error);
        Assert.Equal("Mic invalid", error.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldHaveError_WhenRegionIsEmpty()
    {
        // Arrange with empty region
        var request = new PostExchangeRequest(name: "Exchange", abbreviation: "AB", mic: "ABCD", region: "");

        // Act
        var result = _sut.Validate(request);

        // Assert for missing region
        var error = result.Errors.FirstOrDefault(x => x.PropertyName == "Region");
        Assert.NotNull(error);
        Assert.Equal("Region required", error.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldNotHaveError_WhenRequestIsValid()
    {
        // Arrange with valid data
        var request =
            new PostExchangeRequest(name: "Exchange", abbreviation: "EX", mic: "ABCD", region: "North America");

        // Act
        var result = _sut.Validate(request);

        // Assert for no validation errors
        Assert.Empty(result.Errors);
    }
}
using AutoFixture.Xunit2;
using Orderflow.Features.Common.Enums;
using Orderflow.Features.Orders.CreateOrder.Contracts;
using Orderflow.Features.Orders.CreateOrder.Validators;

namespace Orderflow.Api.Unit.Tests.Features.Orders.CreateOrder.Validators;

public class PostOrderRequestValidatorTests
{
    private readonly PostOrderRequestValidator _sut = new();

    [Fact]
    public void Validate_ShouldReturnValid_WhenRequestIsValid()
    {
        var request = new PostOrderRequest(
            quantity: 20,
            instrumentId: Guid.NewGuid().ToString(),
            side: TradeSide.buy.ToString(),
            price: 232.12);

        var result = _sut.Validate(request);

        Assert.True(condition: result.IsValid);
        Assert.Empty(collection: result.Errors);
    }

    [Fact]
    public void Validate_ShouldReturnInvalid_WhenRequestIsInvalidWithErrors()
    {
        var request = new PostOrderRequest(
            quantity: -24,
            instrumentId: "invalid guid",
            side: "invalid tradeSide",
            price: 5353);

        var result = _sut.Validate(request);

        Assert.False(condition: result.IsValid);
        Assert.Equal(expected: 3, actual: result.Errors.Count);
        Assert.Contains("InstrumentId invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("Quantity invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("TradeSide invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }

    [Theory]
    [InlineAutoData(null, null, null)]
    [InlineAutoData("", null, "")]
    public void Validate_ShouldReturnInvalid_WhenRequestHasInvalidTypes(
        string type,
        int quantity,
        string instrument)
    {
        var request = new PostOrderRequest(
            quantity: quantity,
            instrumentId: instrument,
            side: type,
            price: 7567);

        var result = _sut.Validate(request);

        Assert.False(condition: result.IsValid);
        Assert.Equal(expected: 4, actual: result.Errors.Count);
        Assert.Contains("TradeSide required", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("InstrumentId required", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("Quantity required", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("Quantity invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
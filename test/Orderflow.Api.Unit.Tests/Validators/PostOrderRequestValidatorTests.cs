using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models.Enums;
using Orderflow.Validators;

namespace Orderflow.Api.Unit.Tests.Validators;

public class PostOrderRequestValidatorTests
{
    private readonly PostOrderRequestValidator _sut = new();

    [Fact]
    public void Should_return_valid_for_valid_request()
    {
        var request = new PostOrderRequest(
            quantity: 20,
            instrumentId: Guid.NewGuid().ToString(),
            type: OrderType.buy.ToString());

        var result = _sut.Validate(request);

        Assert.True(condition: result.IsValid);
        Assert.Empty(collection: result.Errors);
    }

    [Fact]
    public void Should_return_invalid_for_invalid_request_with_errors()
    {
        var request = new PostOrderRequest(
            quantity: -24,
            instrumentId: "invalid guid",
            type: "invalid type");

        var result = _sut.Validate(request);

        Assert.False(condition: result.IsValid);
        Assert.Equal(expected: 3, actual: result.Errors.Count);
        Assert.Contains("InstrumentId invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("Quantity invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("Type invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
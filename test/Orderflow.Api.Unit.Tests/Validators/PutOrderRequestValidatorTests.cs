using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models.Enums;
using Orderflow.Validators;

namespace Orderflow.Api.Unit.Tests.Validators;

public class PutOrderRequestValidatorTests
{
    private readonly PutOrderRequestValidator _sut = new();

    [Fact]
    public void Should_return_valid_for_valid_request()
    {
        var request = new PutOrderRequest(
            Guid.NewGuid().ToString(),
            OrderStatus.complete.ToString());

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Should_return_invalid_for_invalid_request()
    {
        var request = new PutOrderRequest(
            "wrong id",
            "invalid");

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains("Id invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
        Assert.Contains("Status invalid", result.Errors.Select(x => x.ErrorMessage).ToList());
    }
}
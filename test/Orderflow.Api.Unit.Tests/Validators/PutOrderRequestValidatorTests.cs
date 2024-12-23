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
            OrderStatus.complete);

        var result = _sut.Validate(request);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Should_return_invalid_for_invalid_id_with_error()
    {
        var request = new PutOrderRequest(
            "wrong id",
            OrderStatus.cancelled);

        var result = _sut.Validate(request);

        Assert.False(result.IsValid);
        Assert.Equal(1, result.Errors.Count);
        Assert.Equal("Id invalid", result.Errors?.FirstOrDefault()?.ErrorMessage);
    }
}
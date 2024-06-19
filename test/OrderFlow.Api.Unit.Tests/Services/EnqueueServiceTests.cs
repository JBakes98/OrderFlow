using System.Net;
using AutoFixture.Xunit2;
using Moq;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Models;
using OrderFlow.Repositories;
using OrderFlow.Services;

namespace OrderFlow.Api.Unit.Tests.Services;

public class EnqueueServiceTests
{
    [Theory, AutoMoqData]
    public async void Should_RetrieveEvent_If_Present(
        [Frozen] Mock<IEventRepository> mockRepository,
        Event @event,
        EnqueueService sut)
    {
        mockRepository.Setup(x =>
                x.GetByIdAsync(@event.Id))
            .ReturnsAsync(@event)
            .Verifiable();

        var result = await sut.RetrieveEvent(@event.Id, default);

        var retrievedEvent = result.AsT0;

        Assert.Equal(@event, retrievedEvent);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_If_EventNotFound(
        [Frozen] Mock<IEventRepository> mockRepository,
        EnqueueService sut,
        string id)
    {
        var expectedError = new Error(HttpStatusCode.UnprocessableEntity, ErrorCodes.EventNotFound);

        mockRepository.Setup(x =>
                x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.RetrieveEvent(id, default);

        var retrievedError = result.AsT1;

        Assert.Equal(expectedError, retrievedError);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_Return_NoError_If_Event_Published(
        [Frozen] Mock<IEventRepository> mockRepository,
        EnqueueService sut,
        Event @event)
    {
        mockRepository.Setup(x => x.InsertAsync(@event, default))
            .ReturnsAsync(@event)
            .Verifiable();

        var result = await sut.PublishEvent(@event, default);

        Assert.True(result == null);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_IfPublish_Fails(
        [Frozen] Mock<IEventRepository> mockRepository,
        Event @event,
        EnqueueService sut)
    {
        var expectedError = new Error(HttpStatusCode.InternalServerError, ErrorCodes.EventCouldNotBePublished);

        mockRepository.Setup(x => x.InsertAsync(@event, default))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.PublishEvent(@event, default);

        Assert.True(result?.ErrorType.Equals(expectedError.ErrorType));
        Assert.True(result?.ErrorCodes.FirstOrDefault()?.Equals(expectedError.ErrorCodes.FirstOrDefault()));
        mockRepository.Verify();
    }
}
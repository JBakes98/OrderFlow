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
    public async void Should_PublishEvent_To_Repo(
        [Frozen] Mock<IRepository<Event>> mockRepository,
        Event @event,
        EnqueueService sut)
    {
        mockRepository.Setup(x =>
                x.InsertAsync(@event, default))
            .ReturnsAsync(@event)
            .Verifiable();

        var result = await sut.PublishEvent(@event);

        Assert.True(result);
        mockRepository.Verify();
    }

    [Theory, AutoMoqData]
    public async void Should_ReturnError_If_Repo_Fails(
        [Frozen] Mock<IRepository<Event>> mockRepository,
        Event @event,
        EnqueueService sut)
    {
        var expectedError = new Error(HttpStatusCode.InternalServerError, ErrorCodes.InstrumentCouldNotBeCreated);

        mockRepository.Setup(x =>
                x.InsertAsync(@event, default))
            .ReturnsAsync(expectedError)
            .Verifiable();

        var result = await sut.PublishEvent(@event);

        Assert.False(result);
        mockRepository.Verify();
    }
}
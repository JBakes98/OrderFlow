using AutoFixture;
using Moq;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Instrument;
using Orderflow.Mappers;
using Orderflow.Services;
using Serilog;

namespace Orderflow.Api.Unit.Tests.Services;

public class InstrumentServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IInstrumentRepository> _mockRepository;
    private readonly Mock<IDiagnosticContext> _mockDiagnosticContext;
    private readonly Mock<IMapper<Instrument, InstrumentCreatedEvent>> _mockEventMapper;
    private readonly InstrumentService _instrumentService;

    public InstrumentServiceTests()
    {
        _fixture = new Fixture();
        _mockRepository = new Mock<IInstrumentRepository>();
        _mockDiagnosticContext = new Mock<IDiagnosticContext>();
        _mockEventMapper = new Mock<IMapper<Instrument, InstrumentCreatedEvent>>();

        _instrumentService = new InstrumentService(
            _mockRepository.Object,
            _mockDiagnosticContext.Object,
            _mockEventMapper.Object);
    }

    [Fact]
    public async Task RetrieveInstrument_ShouldReturnInstrument_WhenFound()
    {
        // Arrange
        var instrumentId = Guid.NewGuid();
        var instrument = _fixture.Build<Instrument>()
            .Create();

        _mockRepository.Setup(r => r.GetByIdAsync(instrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT0(instrument));

        // Act
        var result = await _instrumentService.RetrieveInstrument(instrumentId);

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(instrument, result.AsT0);
        _mockDiagnosticContext.Verify(d => d.Set("InstrumentEntity", instrument, true), Times.Once);
    }

    [Fact]
    public async Task RetrieveInstrument_ShouldReturnError_WhenNotFound()
    {
        // Arrange
        var instrumentId = Guid.NewGuid();
        var error = _fixture.Create<Error>();
        _mockRepository.Setup(r => r.GetByIdAsync(instrumentId))
            .ReturnsAsync(OneOf<Instrument, Error>.FromT1(error));

        // Act
        var result = await _instrumentService.RetrieveInstrument(instrumentId);

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);
    }

    [Fact]
    public async Task RetrieveInstruments_ShouldReturnInstruments_WhenFound()
    {
        // Arrange
        var instruments = _fixture.Create<List<Instrument>>();
        _mockRepository.Setup(r => r.QueryAsync())
            .ReturnsAsync(OneOf<IEnumerable<Instrument>, Error>.FromT0(instruments));

        // Act
        var result = await _instrumentService.RetrieveInstruments();

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(instruments, result.AsT0);
    }

    [Fact]
    public async Task RetrieveInstruments_ShouldReturnError_WhenRepositoryFails()
    {
        // Arrange
        var error = _fixture.Create<Error>();
        _mockRepository.Setup(r => r.QueryAsync())
            .ReturnsAsync(OneOf<IEnumerable<Instrument>, Error>.FromT1(error));

        // Act
        var result = await _instrumentService.RetrieveInstruments();

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);
    }

    [Fact]
    public async Task CreateInstrument_ShouldReturnInstrument_WhenSuccessful()
    {
        // Arrange
        var instrument = _fixture.Create<Instrument>();
        var instrumentEvent = _fixture.Create<InstrumentCreatedEvent>();

        _mockEventMapper.Setup(m => m.Map(instrument))
            .Returns(instrumentEvent);

        _mockRepository.Setup(r => r.InsertAsync(instrument, instrumentEvent))
            .ReturnsAsync((Error)null!);

        // Act
        var result = await _instrumentService.CreateInstrument(instrument);

        // Assert
        Assert.True(result.IsT0);
        Assert.Equal(instrument, result.AsT0);
    }

    [Fact]
    public async Task CreateInstrument_ShouldReturnError_WhenInsertFails()
    {
        // Arrange
        var instrument = _fixture.Create<Instrument>();
        var instrumentEvent = _fixture.Create<InstrumentCreatedEvent>();
        var error = _fixture.Create<Error>();

        _mockEventMapper.Setup(m => m.Map(instrument))
            .Returns(instrumentEvent);

        _mockRepository.Setup(r => r.InsertAsync(instrument, instrumentEvent))
            .ReturnsAsync(error);

        // Act
        var result = await _instrumentService.CreateInstrument(instrument);

        // Assert
        Assert.True(result.IsT1);
        Assert.Equal(error, result.AsT1);
    }
}
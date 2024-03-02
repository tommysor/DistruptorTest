using Backend.Features.MathFeature;
using Backend.Features.MathFeature.Events;
using Disruptor.Dsl;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Backend.Tests.Features.MathFeature;

public class NumbersProducedEventHandlerTests : IDisposable
{
    NumbersProducedEventHandler _sut;
    Disruptor<NumbersMultipliedEvent> _disruptor;

    public NumbersProducedEventHandlerTests()
    {
        _disruptor = new Disruptor<NumbersMultipliedEvent>(() => new NumbersMultipliedEvent(), 4);
        _disruptor.Start();
        ILogger<NumbersProducedEventHandler> _loggerMock = Substitute.For<ILogger<NumbersProducedEventHandler>>();
        _sut = new NumbersProducedEventHandler(_loggerMock, _disruptor.RingBuffer);
    }

    public void Dispose()
    {
        _disruptor.Shutdown();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ShouldMultiplyNumbers()
    {
        var input = new NumbersProducedEvent();
        input.Initialize(2, 3);
        _sut.OnEvent(input, 0, true);
        var actual = _disruptor.RingBuffer[0];
        Assert.Equal(6, actual.Number);
    }

    [Fact]
    public void ShouldThrowOnLargeNumber()
    {
        var input = new NumbersProducedEvent();
        input.Initialize(100, 11);
        Assert.Throws<ArithmeticException>(() => _sut.OnEvent(input, 0, true));
        Assert.Equal(-1, _disruptor.RingBuffer.Cursor);
    }
}

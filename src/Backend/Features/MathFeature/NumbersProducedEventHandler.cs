using Backend.Features.MathFeature.Events;
using Disruptor;

namespace Backend.Features.MathFeature;

public sealed class NumbersProducedEventHandler : IEventHandler<NumbersProducedEvent>
{
    private readonly ILogger<NumbersProducedEventHandler> _logger;
    private readonly RingBuffer<NumbersMultipliedEvent> _ringBuffer;

    public NumbersProducedEventHandler(ILogger<NumbersProducedEventHandler> logger, RingBuffer<NumbersMultipliedEvent> ringBuffer)
    {
        _logger = logger;
        _ringBuffer = ringBuffer;
    }

    public void OnEvent(NumbersProducedEvent data, long sequence, bool endOfBatch)
    {
        _logger.LogInformation("Evaluating numbers {A} and {B}", data.A, data.B);
        var result = data.A * data.B;
        if (result > 1000)
        {
            _logger.LogInformation("Large number detected: {Result}", result);
            throw new ArithmeticException("Large number detected");
        }
        using var scope = _ringBuffer.PublishEvent();
        var e = scope.Event();
        e.Initialize(data.Id, result);
    }
}

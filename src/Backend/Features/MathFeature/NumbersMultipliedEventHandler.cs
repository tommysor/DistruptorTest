using Backend.Features.MathFeature.Events;
using Disruptor;

namespace Backend.Features.MathFeature;

public sealed class NumbersMultipliedEventHandler : IEventHandler<NumbersMultipliedEvent>
{
    private readonly ILogger<NumbersMultipliedEventHandler> _logger;

    public NumbersMultipliedEventHandler(ILogger<NumbersMultipliedEventHandler> logger)
    {
        _logger = logger;
    }

    public void OnEvent(NumbersMultipliedEvent data, long sequence, bool endOfBatch)
    {
        _logger.LogInformation("Numbers multiplied: {Number}", data.Number);
    }
}

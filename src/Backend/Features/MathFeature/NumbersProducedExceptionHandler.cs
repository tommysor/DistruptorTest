using Backend.Features.MathFeature.Events;
using Disruptor;

namespace Backend.Features.MathFeature
{
    public sealed class NumbersProducedExceptionHandler : IExceptionHandler<NumbersProducedEvent>
    {
        private readonly ILogger<NumbersProducedExceptionHandler> _logger;

        public NumbersProducedExceptionHandler(ILogger<NumbersProducedExceptionHandler> logger)
        {
            _logger = logger;
        }

        public void HandleEventException(Exception ex, long sequence, NumbersProducedEvent evt)
        {
            _logger.LogError("Hello from NumbersProducedExceptionHandler");
        }

        public void HandleEventException(Exception ex, long sequence, EventBatch<NumbersProducedEvent> batch)
        {
        }

        public void HandleOnShutdownException(Exception ex)
        {
        }

        public void HandleOnStartException(Exception ex)
        {
        }

        public void HandleOnTimeoutException(Exception ex, long sequence)
        {
        }
    }
}

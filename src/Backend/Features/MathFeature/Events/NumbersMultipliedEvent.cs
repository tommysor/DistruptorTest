namespace Backend.Features.MathFeature.Events;

public sealed class NumbersMultipliedEvent
{
    public Guid Id { get; private set; }
    public Guid NumbersProducedEventId { get; private set; }
    public int Number { get; private set; }

    public void Initialize(Guid numbersProducedEventId, int number)
    {
        Id = Guid.NewGuid();
        NumbersProducedEventId = numbersProducedEventId;
        Number = number;
    }
}

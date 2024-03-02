namespace Backend.Features.MathFeature.Events;

public sealed class NumbersProducedEvent
{
    public Guid Id { get; private set; }
    public int A { get; private set; }
    public int B { get; private set; }

    public void Initialize(int a, int b)
    {
        Id = Guid.NewGuid();
        A = a;
        B = b;
    }
}

using Backend.Features.MathFeature.Events;

namespace Backend.Tests.Features.MathFeature.Events;

public class NumbersProducedEventTests
{
    [Fact]
    public static void ShouldGenerateId()
    {
        var e1 = new NumbersProducedEvent();
        e1.Initialize(1, 1);
        var e2 = new NumbersProducedEvent();
        e2.Initialize(1, 1);
        Assert.NotEqual(Guid.Empty, e1.Id);
        Assert.NotEqual(Guid.Empty, e2.Id);
        Assert.NotEqual(e1.Id, e2.Id);
    }
}

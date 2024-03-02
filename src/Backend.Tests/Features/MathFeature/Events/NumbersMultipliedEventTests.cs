using Backend.Features.MathFeature.Events;

namespace Backend.Tests.Features.MathFeature.Events;

public class NumbersMultipliedEventTests
{
    [Fact]
    public static void ShouldGenerateId()
    {
        var e1 = new NumbersMultipliedEvent();
        e1.Initialize(Guid.NewGuid(), 1);
        var e2 = new NumbersMultipliedEvent();
        e2.Initialize(Guid.NewGuid(), 1);
        Assert.NotEqual(Guid.Empty, e1.Id);
        Assert.NotEqual(e1.Id, e1.NumbersProducedEventId);
        Assert.NotEqual(Guid.Empty, e2.Id);
        Assert.NotEqual(e1.Id, e2.Id);
    }
}

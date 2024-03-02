using Backend;
using Backend.Features.MathFeature;
using Backend.Features.MathFeature.Events;
using Disruptor.Dsl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * Build
 */
var app = builder.Build();

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

var numbersMultipliedEventHandlerLogger = loggerFactory.CreateLogger<NumbersMultipliedEventHandler>();
var numbersMultipliedDisruptor = new Disruptor<NumbersMultipliedEvent>(() => new NumbersMultipliedEvent(), ringBufferSize: 1024);
numbersMultipliedDisruptor.HandleEventsWith(new NumbersMultipliedEventHandler(numbersMultipliedEventHandlerLogger));
var numbersMultipliedRingBuffer = numbersMultipliedDisruptor.RingBuffer;

var NumbersProducedEventHandlerLogger = loggerFactory.CreateLogger<NumbersProducedEventHandler>();
var numbersProducedDisruptor = new Disruptor<NumbersProducedEvent>(() => new NumbersProducedEvent(), ringBufferSize: 1024);
numbersProducedDisruptor.HandleEventsWith(new NumbersProducedEventHandler(NumbersProducedEventHandlerLogger, numbersMultipliedRingBuffer));
var numbersProducedRingBuffer = numbersProducedDisruptor.RingBuffer;

var NumbersProducedExceptionHandlerLogger = loggerFactory.CreateLogger<NumbersProducedExceptionHandler>();
numbersProducedDisruptor.SetDefaultExceptionHandler(new NumbersProducedExceptionHandler(NumbersProducedExceptionHandlerLogger));

numbersMultipliedDisruptor.Start();
numbersProducedDisruptor.Start();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () =>
{
    return "Hello World!";
})
.WithName("Hello")
.WithOpenApi();

_ = Task.Run(async () =>
{
    var random = new Random();
    while (true)
    {
        var delay = random.Next(1, 2000);
        await Task.Delay(delay);
        var a = random.Next(1, 50);
        var b = random.Next(1, 50);
        using var scope = numbersProducedRingBuffer.PublishEvent();
        var e = scope.Event();
        e.Initialize(a, b);
    }
});

/*
 * Run
 */
app.Run();

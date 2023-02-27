using MassTransit;
using Messages;
using Microsoft.Extensions.Logging;

namespace Consumer;

public class HelloWorldConsumer : IConsumer<DummyMessage>
{
    private readonly ILogger<DummyMessage> _log;

    public HelloWorldConsumer(ILogger<DummyMessage> log)
    {
        _log = log;
    }

    public Task Consume(ConsumeContext<DummyMessage> context)
    {
        _log.LogInformation("Message received: {value}", context.Message.Value);

        return Task.CompletedTask;
    }
}
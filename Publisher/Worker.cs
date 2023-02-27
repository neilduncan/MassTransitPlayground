using MassTransit;
using Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Publisher;

public class Worker : BackgroundService
{
    private readonly IBus _bus;
    private readonly ILogger<Worker> _log;

    public Worker(IBus bus, ILogger<Worker> log)
    {
        _bus = bus;
        _log = log;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _bus.Publish(new DummyMessage($"The time is {DateTime.Now:hh:mm:ss}"), stoppingToken);

            //var payload = new
            //{
            //    TestProperty = new { Nested = true }
            //};

            //await _bus.Publish(new CompleteNotification(new Guid("f1c0d500-a86f-4491-98d4-4e0e1e165a86"), payload), stoppingToken);

            _log.LogInformation("Published a message");

            await Task.Delay(2000, stoppingToken);
        }
    }
}
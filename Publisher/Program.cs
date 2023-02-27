using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("RabbitMQ");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string has not been configured. Check it's set in environment variables in Release environments, or your app secrets in development");
        }

        //MassTransit
        services.AddMassTransit(x =>
        {
            x.SetSnakeCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                //cfg.MessageTopology.SetEntityNameFormatter(new OneBasketEntityNameFormatter(new RabbitMqMessageNameFormatter()));
                cfg.Host(connectionString);
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddHostedService<Worker>();
    });

var app = host.Build();

await app.RunAsync();
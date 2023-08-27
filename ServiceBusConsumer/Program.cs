using Microsoft.Azure.ServiceBus;
using ServiceBusConsumer;
using Microsoft.Extensions.Configuration;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CalculatorConsumer>();
        var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

        services.AddSingleton<ISubscriptionClient>(x => 
            new SubscriptionClient(configuration["ServiceBus:CONNECTION_STRING"], configuration["ServiceBus:TopicName"], configuration["ServiceBus:SubscriptionName"]));
    })
    .Build();

await host.RunAsync();

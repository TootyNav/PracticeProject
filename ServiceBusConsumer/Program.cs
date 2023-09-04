
using Microsoft.Azure.ServiceBus;
using ServiceBusConsumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<CalculatorConsumer>();
builder.Services.AddSignalR();

var configuration = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

builder.Services.AddSingleton<ISubscriptionClient>(x =>
    new SubscriptionClient(
        configuration["ServiceBus:CONNECTION_STRING"],
        configuration["ServiceBus:TopicName"],
        configuration["ServiceBus:SubscriptionName"]));


var app = builder.Build();
app.Run();

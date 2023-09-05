
using Microsoft.Azure.ServiceBus;
using ServiceBusConsumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<CalculatorConsumer>();
builder.Services.AddSignalR();


builder.Services.AddSingleton<ISubscriptionClient>(x =>
    new SubscriptionClient(
        builder.Configuration["ServiceBus:CONNECTIONSTRING"],
        builder.Configuration["ServiceBus:TopicName"],
        builder.Configuration["ServiceBus:SubscriptionName"]));

var app = builder.Build();
app.Run();

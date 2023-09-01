using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;
using ServiceBusConsumer;
using WebApplication_mvc_test_ai.Hubs;

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

app.MapHub<ChatHub>("/chatHub");

app.Run();

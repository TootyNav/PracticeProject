using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using WebApplication_mvc_test_ai.Hubs;

namespace ServiceBusConsumer
{
    public class CalculatorConsumer : BackgroundService
    {
        private readonly ILogger<CalculatorConsumer> _logger;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IHubContext<ChatHub> _hubContext;

        public CalculatorConsumer(
            ILogger<CalculatorConsumer> logger,
            ISubscriptionClient subscriptionClient,
            IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _subscriptionClient = subscriptionClient;
            _hubContext = hubContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler((message, tokent) =>
            {
                var number = JsonConvert.DeserializeObject<int>(Encoding.UTF8.GetString(message.Body));
                var total = 150 + number;

                _logger.LogInformation("number is: {number} and we add 150 to get: {total}", number, total);
                _hubContext.Clients.All.SendAsync("ReceiveMessage", number + 150);

                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            }, new MessageHandlerOptions(args => Task.CompletedTask)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1,
            }) ;

            return Task.CompletedTask;
        }
    }
}
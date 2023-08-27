using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace ServiceBusConsumer
{
    public class CalculatorConsumer : BackgroundService
    {
        private readonly ILogger<CalculatorConsumer> _logger;
        private readonly ISubscriptionClient _subscriptionClient;

        public CalculatorConsumer(ILogger<CalculatorConsumer> logger, ISubscriptionClient subscriptionClient)
        {
            _logger = logger;
            _subscriptionClient = subscriptionClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler((message, tokent) =>
            {
                var number = JsonConvert.DeserializeObject<int>(Encoding.UTF8.GetString(message.Body));
                _logger.LogInformation("number is: {number}", number);
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
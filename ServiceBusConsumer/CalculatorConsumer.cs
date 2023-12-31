using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace ServiceBusConsumer
{
    public class CalculatorConsumer : BackgroundService
    {
        private readonly ILogger<CalculatorConsumer> _logger;
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IConfiguration Configuration;

        public CalculatorConsumer(
            ILogger<CalculatorConsumer> logger,
            ISubscriptionClient subscriptionClient)
        {
            _logger = logger;
            _subscriptionClient = subscriptionClient;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Release.json", optional: true, reloadOnChange: true).Build();

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var hubConnection = new HubConnectionBuilder().WithUrl(Configuration["SignalRUrl"]).Build();
            await hubConnection.StartAsync();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                message = user + message;
            });

            var total = 0;
            _subscriptionClient.RegisterMessageHandler(async (message, tokent) =>
            {
                var number = JsonConvert.DeserializeObject<int>(Encoding.UTF8.GetString(message.Body));
                total = 150 + number;

                _logger.LogInformation("number is: {number} and we add 150 to get: {total}", number, total);

                await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                await hubConnection.SendAsync("SendMessage", "Workerservice ", total.ToString());

            }, new MessageHandlerOptions(args => Task.CompletedTask)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1,
            });

            await Task.CompletedTask;

            if (stoppingToken.IsCancellationRequested)
            {
                await hubConnection.DisposeAsync();
            }
        }
    }
}
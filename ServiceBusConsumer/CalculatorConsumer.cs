using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "userStuff" ,"10");
                await Task.Delay(1000, stoppingToken);
            }
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    Random random = new Random();
        //    int randomNumber = random.Next(101);
        //    await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"Hello World {randomNumber}");

        //    _subscriptionClient.RegisterMessageHandler(async (message, tokent) =>
        //    {
        //        var number = JsonConvert.DeserializeObject<int>(Encoding.UTF8.GetString(message.Body));
        //        var total = 150 + number;

        //        _logger.LogInformation("number is: {number} and we add 150 to get: {total}", number, total);
        //        //_hubContext.Clients.All.SendAsync("ReceiveMessage","dd", (number + 150).ToString());

        //        await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

        //    }, new MessageHandlerOptions(args => Task.CompletedTask)
        //    {
        //        AutoComplete = false,
        //        MaxConcurrentCalls = 1,
        //    }) ;

        //    await Task.CompletedTask;
        //}


    }
}
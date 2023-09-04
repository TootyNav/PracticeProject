using Microsoft.AspNetCore.SignalR;
using WebApplication_mvc_test_ai.Hubs;

namespace ServiceBusConsumer
{
    public class CalculatorConsumer2 : BackgroundService
    {
        private readonly ILogger<CalculatorConsumer2> _logger;
        private readonly IHubContext<ChatHub> _hubContext;

        public CalculatorConsumer2(
            ILogger<CalculatorConsumer2> logger,
            IHubContext<ChatHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {Time}", DateTime.Now);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", DateTime.Now);
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
using Microsoft.AspNetCore.SignalR;

namespace WebApplication_mvc_test_ai.Services;

public class MyBackgroundService : BackgroundService
{
    private readonly IHubContext<MyHub> _hubContext;

    public MyBackgroundService(IHubContext<MyHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Do some processing here
            var message = "Hello World!";
            await _hubContext.Clients.All.SendAsync("UpdateMessage", message);

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}



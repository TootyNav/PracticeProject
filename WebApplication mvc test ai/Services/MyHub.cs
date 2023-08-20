
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebApplication_mvc_test_ai.Services;

public class MyHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}

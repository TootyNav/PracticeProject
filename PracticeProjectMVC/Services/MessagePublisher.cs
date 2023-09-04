using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace WebApplication_mvc_test_ai.Services
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IQueueClient _queueClient;

        public MessagePublisher(IQueueClient queueClient)
        {
            this._queueClient = queueClient;
        }

        public Task Publish<T>(T obj)
        {
            var objectAsText = JsonSerializer.Serialize(obj);
            var message = new Message(Encoding.UTF8.GetBytes(objectAsText));
            return _queueClient.SendAsync(message);
        }

        public Task Publish<T>(string raw) 
        {
            var message = new Message(Encoding.UTF8.GetBytes(raw));
            return _queueClient.SendAsync(message);
        }
    }
}

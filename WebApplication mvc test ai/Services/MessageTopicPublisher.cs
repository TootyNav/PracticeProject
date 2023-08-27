﻿using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;

namespace WebApplication_mvc_test_ai.Services
{
    public class MessageTopicPublisher : IMessagePublisher
    {
        private readonly ITopicClient _topicClient;
        private const string _filter = "CalculateNumber";

        public MessageTopicPublisher(ITopicClient topicClient)
        {
            this._topicClient = topicClient;
        }

        public Task Publish<T>(T obj)
        {
            var objectAsText = JsonSerializer.Serialize(obj);
            var message = new Message(Encoding.UTF8.GetBytes(objectAsText));
            message.UserProperties["MessageType"] = _filter;

            return _topicClient.SendAsync(message);
        }

        public Task Publish<T>(string raw) 
        {
            var message = new Message(Encoding.UTF8.GetBytes(raw));
            message.UserProperties["MessageType"] = _filter;

            return _topicClient.SendAsync(message);
        }
    }
}

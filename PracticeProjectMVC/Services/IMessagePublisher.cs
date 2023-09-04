namespace WebApplication_mvc_test_ai.Services
{
    public interface IMessagePublisher
    {
        public Task Publish<T>(T obj);
        public Task Publish<T>(string raw);
    }
}
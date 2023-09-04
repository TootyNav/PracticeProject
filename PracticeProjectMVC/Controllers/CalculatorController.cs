using Microsoft.AspNetCore.Mvc;
using WebApplication_mvc_test_ai.Services;

namespace WebApplication_mvc_test_ai.Controllers
{
    public class CalculatorController : Controller
    {
        private IMessagePublisher _messagePublisher;
        public CalculatorController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeNumber(int userNumber)
        {
            _messagePublisher.Publish(userNumber);

            return NoContent();
        }
    }
}

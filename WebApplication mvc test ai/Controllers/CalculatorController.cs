using Microsoft.AspNetCore.Mvc;
using WebApplication_mvc_test_ai.Services;

namespace WebApplication_mvc_test_ai.Controllers
{
    public class CalculatorController : Controller
    {
        private IMessagePublisher _massagePublisher;
        public CalculatorController(IMessagePublisher massagePublisher)
        {
            _massagePublisher = massagePublisher;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeNumber(int userNumber)
        {
            _massagePublisher.Publish(userNumber);

            return NoContent();
        }
    }
}

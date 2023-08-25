using Microsoft.AspNetCore.Mvc;

namespace WebApplication_mvc_test_ai.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChangeNumber(int userNumber)
        {


            return NoContent();
        }
    }
}

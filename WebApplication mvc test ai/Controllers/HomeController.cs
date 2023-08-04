using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication_mvc_test_ai.Models;
using Microsoft.ApplicationInsights.Extensibility;

namespace WebApplication_mvc_test_ai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryClient telemetryClient;

        public HomeController(ILogger<HomeController> logger, TelemetryConfiguration telemetryConfiguration)
        {
            _logger = logger;
            this.telemetryClient = new TelemetryClient(telemetryConfiguration);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Privacy2()
        {
            return View();
        }

        public IActionResult Privacy3()
        {
            return View();
        }

        public IActionResult Privacy4()
        {

            _logger.LogInformation("Privacy 4 before error.");

            // Add custom properties
            var telemetry = new TelemetryClient();

            var requestTelemetry = new RequestTelemetry();
            requestTelemetry.Properties.Add("MyCustomProperty", "MyCustomValueeee");
            telemetry.TrackRequest(requestTelemetry);

            throw new NotImplementedException();

            _logger.LogInformation("Privacy 4 after error.");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
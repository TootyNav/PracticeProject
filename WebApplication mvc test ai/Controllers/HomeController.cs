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
        private readonly TelemetryClient TelemetryClient;

        public HomeController(ILogger<HomeController> logger, TelemetryConfiguration telemetryConfiguration)
        {
            _logger = logger;
            TelemetryClient = new TelemetryClient(telemetryConfiguration);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("An example of a Information trace..");
            _logger.LogWarning("An example of a Warning trace..");
            _logger.LogError(new ArgumentNullException(), "An example of an Error level message");
            _logger.LogDebug("An example of an Error level message parameter name is:\"James\"");

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

        public IActionResult Privacy4(string parameterOne = "FakeParameter")
        {


            // Add custom properties
            var requestTelemetry = new RequestTelemetry();
            requestTelemetry.Properties.Add("MyCustomProperty", "MyCustomValueeee");
            TelemetryClient.TrackRequest(requestTelemetry);

            throw new NotImplementedException();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
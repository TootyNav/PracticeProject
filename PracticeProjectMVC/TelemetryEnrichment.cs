using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;

namespace WebApplication_mvc_test_ai
{
    public class TelemetryEnrichment : TelemetryInitializerBase
    {
        public TelemetryEnrichment(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        protected override void OnInitializeTelemetry(HttpContext platformContext, RequestTelemetry requestTelemetry,
                                                      ITelemetry telemetry)
        {
            telemetry.Context.User.AuthenticatedUserId =
                platformContext.User?.Identity.Name ?? "fake name";

        }
    }
}

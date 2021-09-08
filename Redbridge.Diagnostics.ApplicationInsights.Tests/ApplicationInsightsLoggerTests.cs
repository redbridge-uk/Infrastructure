using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using NUnit.Framework;

namespace Redbridge.Diagnostics.ApplicationInsights.Tests
{
    [TestFixture]
    public class ApplicationInsightsLoggerTests
    {
        [Test]
        public void Construct_ApplicationInsightsLogger_ExpectSuccess()
        {
            var logger = new ApplicationInsightsLogger<ApplicationInsightsLoggerTests>(new TelemetryClient(new TelemetryConfiguration()));
            Assert.IsNotNull(logger);
        }
    }
}
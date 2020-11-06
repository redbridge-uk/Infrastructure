using NUnit.Framework;

namespace Redbridge.Diagnostics.ApplicationInsights.Tests
{
    [TestFixture]
    public class ApplicationInsightsLoggerTests
    {
        [Test]
        public void Construct_ApplicationInsightsLogger_ExpectSuccess()
        {
            var logger = new ApplicationInsightsLogger();
            Assert.IsNotNull(logger);
        }
    }
}
namespace Redbridge.Diagnostics.ApplicationInsights
{
    public class ApplicationInsightsLoggerFactory : ILoggerFactory
    {
        public ILogger Create<T>()
        {
            return new ApplicationInsightsLogger();
        }
    }
}
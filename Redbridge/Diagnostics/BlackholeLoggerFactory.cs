namespace Redbridge.Diagnostics
{
    public class BlackholeLoggerFactory : ILoggerFactory
    {
        public ILogger Create<T>()
        {
            return new BlackholeLogger();
        }
    }
}
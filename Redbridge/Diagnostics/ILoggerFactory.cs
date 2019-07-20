namespace Redbridge.Diagnostics
{
    public interface ILoggerFactory
    {
        ILogger Create<T>();
    }
}
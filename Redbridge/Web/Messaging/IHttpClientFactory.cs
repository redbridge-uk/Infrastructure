using System.Net.Http;

namespace Redbridge.Web.Messaging
{
    public interface IHttpClientFactory
    {
        HttpClient Create();
    }
}
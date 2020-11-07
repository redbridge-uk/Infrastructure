using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Redbridge.Tests
{
    public class TestHttpModuleHandler : HttpMessageHandler
    {
        private readonly List<HttpRequestMessage> _sent = new List<HttpRequestMessage>();

        public TestHttpModuleHandler()
        {

        }

        public int SentMessageCount => _sent.Count;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _sent.Add(request);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        }
    }
}
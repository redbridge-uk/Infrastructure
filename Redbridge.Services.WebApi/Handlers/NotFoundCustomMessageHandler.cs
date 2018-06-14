using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Hosting;

namespace Redbridge.Services.WebApi.Handlers
{
	public class NotFoundCustomMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode != HttpStatusCode.NotFound) return response;
            request.Properties.Remove(HttpPropertyKeys.NoRouteMatched);
            var errorResponse = request.CreateResponse(response.StatusCode, "Page or resource not found.");
            return errorResponse;
        }
    }
}

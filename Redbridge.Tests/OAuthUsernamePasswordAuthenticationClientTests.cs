using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Redbridge.Identity.OAuthServer;
using Redbridge.Security;

namespace Redbridge.Tests
{
    [TestFixture]
    public class OAuthUsernamePasswordAuthenticationClientTests
    {
        [Test]
        public void CreateOAuthUserNamePasswordClientSetCredentialsKeepsRefreshTokenExpectSuccess()
        {
            var builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ClientId", "MyApp" },
                { "ClientSecret", "abcdef" },
                { "AuthorisationServiceUrl", "https://test-api-dev.azurewebsites.net" }
            });

			var mockLogger = new Mock<ILogger<OAuthUsernamePasswordAuthenticationClient>>();
            var mockClientFactory = new Mock<IHttpClientFactory>();
			var client = new OAuthUsernamePasswordAuthenticationClient(builder.Build(), mockLogger.Object, new HmacSha256HashingService(), mockClientFactory.Object);
			client.SetCredentials(new UserCredentials() { Username = "mytester@gmail.com", Password = "trousers", RefreshToken = "xyz" });
            Assert.IsTrue(client.CanRefresh);
		}
    }
}

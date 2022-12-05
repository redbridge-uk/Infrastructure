﻿using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Redbridge.Configuration;
using Redbridge.Identity.OAuthServer;
using Redbridge.Security;

namespace Redbridge.Tests
{
    [TestFixture()]
    public class OAuthUsernamePasswordAuthenticationClientTests
    {
        [Test()]
        public void CreateOAuthUserNamePasswordClientSetCredentialsKeepsRefreshTokenExpectSuccess()
		{
			var mockSettings = new Mock<IApplicationSettingsRepository>();
			mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("MyApp");
			mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("abcdef");
			mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("https://test-api-dev.azurewebsites.net"));
			var mockLogger = new Mock<ILogger<OAuthUsernamePasswordAuthenticationClient>>();
            var mockClientFactory = new Mock<IHttpClientFactory>();
			var client = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService(), mockClientFactory.Object);
			client.SetCredentials(new UserCredentials() { Username = "mytester@gmail.com", Password = "trousers", RefreshToken = "xyz" });
            Assert.IsTrue(client.CanRefresh);
		}
    }
}

using System;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using Redbridge.Configuration;
using Redbridge.Diagnostics;
using Redbridge.Identity;
using Redbridge.Identity.OAuthServer;
using Redbridge.Security;
using Redbridge.Threading;

namespace Redbridge.Tests
{
    [TestFixture()]
    public class OAuthUsernamePasswordAuthenticationClientTests
    {
        [Test()]
        public void CreateOAuthUserNamePasswordClientSetCredentialsKeepsRefreshTokenExpectSuccess()
		{
			var mockSettings = new Mock<IApplicationSettingsRepository>();
			mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("EasilogMobileApp");
			mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("E4092008CB5C45718718828DE56B39F3");
			mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("https://easilog-api-dev.azurewebsites.net"));
			var mockLogger = new Mock<ILogger>();
			var client = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService());
			client.SetCredentials(new UserCredentials() { Username = "binarysenator@gmail.com", Password = "meatloaf", RefreshToken = "xyz" });
            Assert.IsTrue(client.CanRefresh);
		}

        //[Test()]
        public void CreateOAuthUserNamePasswordClientExpectSuccess()
        {
            var mockSettings = new Mock<IApplicationSettingsRepository>();
            mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("EasilogMobileApp");
            mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("E4092008CB5C45718718828DE56B39F3");
            mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("https://easilog-api-dev.azurewebsites.net"));
            var mockLogger = new Mock<ILogger>();
            var client = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService());
            client.SetCredentials(new UserCredentials() { Username = "binarysenator@gmail.com", Password = "meatloaf" });
            client.BeginLoginAsync().WaitAndUnwrapException();
            Assert.AreEqual(client.CurrentConnectionStatus, ClientConnectionStatus.Connected);
        }

		//[Test()]
		public void CreateOAuthUserNamePasswordClientConnectAndDisconnectExpectSuccessAndDisconnectedClient()
		{
			var mockSettings = new Mock<IApplicationSettingsRepository>();
			mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("EasilogMobileApp");
			mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("E4092008CB5C45718718828DE56B39F3");
			//mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("http://192.168.1.114:29065"));
            mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("https://easilog-api-dev.azurewebsites.net"));
			var mockLogger = new Mock<ILogger>();
			var client = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService());
            client.SetCredentials(new UserCredentials() { Username = "binarysenator@gmail.com", Password = "meatloaf" });
			client.BeginLoginAsync().WaitAndUnwrapException();
			Assert.AreEqual(client.CurrentConnectionStatus, ClientConnectionStatus.Connected);
            client.LogoutAsync().Wait();
            Assert.AreEqual(client.CurrentConnectionStatus, ClientConnectionStatus.Disconnected);
            Assert.IsFalse(client.IsConnected);
		}

        //[Test()]
        public void CreateOAuthUserNamePasswordClientAccessProfileExpectSuccess()
		{
			var mockSettings = new Mock<IApplicationSettingsRepository>();
			mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("EasilogMobileApp");
			mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("E4092008CB5C45718718828DE56B39F3");
			mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("http://192.168.1.114:29065"));
            //mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("https://easilog-api-dev.azurewebsites.net"));
			var mockLogger = new Mock<ILogger>();
			var client = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService());
            client.SetCredentials(new UserCredentials() { Username = "binarysenator@gmail.com", Password = "meatloaf" });
			client.BeginLoginAsync().WaitAndUnwrapException();
			Assert.AreEqual(client.CurrentConnectionStatus, ClientConnectionStatus.Connected);

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.1.114:29065");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", client.AccessToken);
            var result = httpClient.GetAsync("authentication/profile/current").Result;
            Assert.IsNotNull(result);
		}

        //[Test()]
        public void CreateOAuthUserNamePasswordClientThenRefreshExpectSuccess()
		{
			var mockSettings = new Mock<IApplicationSettingsRepository>();
			mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("EasilogMobileApp");
			mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("E4092008CB5C45718718828DE56B39F3");
			mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("http://192.168.1.114:29065"));
			var mockLogger = new Mock<ILogger>();
			var client = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService());
                client.SetCredentials(new UserCredentials() { Username = "binarysenator@gmail.com", Password = "meatloaf" });
			client.BeginLoginAsync().WaitAndUnwrapException();
			Assert.AreEqual(client.CurrentConnectionStatus, ClientConnectionStatus.Connected);
            var bearerToken = client.AccessToken;
            var savedSettings = client.SaveAsync().Result;

            var refreshOnlyClient = new OAuthUsernamePasswordAuthenticationClient(mockSettings.Object, mockLogger.Object, new HmacSha256HashingService());
            refreshOnlyClient.LoadAsync(savedSettings).Wait();
			refreshOnlyClient.BeginLoginAsync().WaitAndUnwrapException();
			Assert.AreEqual(refreshOnlyClient.CurrentConnectionStatus, ClientConnectionStatus.Connected);
            var refreshedBearerToken = client.AccessToken;
            Assert.AreEqual(bearerToken, refreshedBearerToken);
		}
    }
}

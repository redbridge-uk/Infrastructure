using System;
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
    public class OAuthClientSecretAuthenticationClientTests
    {
        //[Test()]
        public void CreateOAuthClientSecretExpectSuccess()
        {
            var mockSettings = new Mock<IApplicationSettingsRepository>();
            mockSettings.Setup(s => s.GetStringValue("ClientId")).Returns("EasilogMobileApp");
            mockSettings.Setup(s => s.GetStringValue("ClientSecret")).Returns("E4092008CB5C45718718828DE56B39F3");
            mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("http://192.168.1.114:29065"));
            //mockSettings.Setup(s => s.GetUrl("AuthorisationServiceUrl")).Returns(new Uri("https://easilog-api-dev.azurewebsites.net"));
            var mockLogger = new Mock<ILogger>();
            var client = new OAuthNonInteractiveAuthenticationClient(mockSettings.Object, mockLogger.Object);
            client.SetCredentials(UserCredentials.AsNonInteractive());
            client.BeginLoginAsync().WaitAndUnwrapException();
            Assert.AreEqual(client.CurrentConnectionStatus, ClientConnectionStatus.Connected);
        }
    }
}

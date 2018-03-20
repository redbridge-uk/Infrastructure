using System;
using System.Configuration;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace Redbridge.Services.WebApi.Security
{
    public static class AppBuilderSecurityExtensions
    {
        public static void ConfigureAuth0Security (this IAppBuilder app)
        {
            var issuer = $"https://{ConfigurationManager.AppSettings["Auth0Domain"]}/";
            var audience = ConfigurationManager.AppSettings["Auth0ClientID"];
            var secret = Convert.FromBase64String(ConfigurationManager.AppSettings["Auth0ClientSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    },
                });

        }
    }
}

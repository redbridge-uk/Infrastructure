using System.Threading.Tasks;
using Redbridge.Configuration;
using Redbridge.Diagnostics;

namespace Redbridge.Identity
{
    public class AnonymousAuthenticationClient : AuthenticationClient
    {
        protected AnonymousAuthenticationClient(IApplicationSettingsRepository settings, ILogger logger) : base(settings, logger) { }

        public override string AuthenticationMethod => "None";

        public override string Username => string.Empty;

        public override string AccessToken => string.Empty;

        public override string ClientType => "Anonymous";

        protected override Task OnBeginLoginAsync()
        {
            return Task.CompletedTask;
        }
    }
}

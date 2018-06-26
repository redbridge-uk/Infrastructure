using System.Collections.Generic;

namespace Redbridge.Identity
{
    public class OAuthRefreshTokenAccessTokenRequestData
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }

        public IDictionary<string, string> AsDictionary()
        {
            var formData = new Dictionary<string, string>
            {
                {"grant_type", "refresh_token"},
                {"refresh_token", RefreshToken },
                {"client_id", ClientId},
                {"client_secret", ClientSecret},
            };
            return formData;
        }
    }
}

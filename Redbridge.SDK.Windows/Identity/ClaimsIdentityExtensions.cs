using System;
using System.Security.Claims;

namespace Redbridge.Identity
{
    public static class ClaimsIdentityExtensions
    {
        public static Guid? GetGuidClaim (this ClaimsIdentity identity, string claimType)
        {
            var claimValue = identity.FindFirst(claimType)?.Value;
            Guid result;

            if ( Guid.TryParse(claimValue, out result) )
            {
                return result;
            }

            return null;
        }
    }
}

using System;
using System.Security.Claims;
using NUnit.Framework;
using Redbridge.Security;

namespace Redbridge.Windows.Tests
{
    public class MyTrialContext : ApiCallContext<MyTrialContext, Guid>
    {
		public MyTrialContext() : this(DateTime.UtcNow) {}

		public MyTrialContext(DateTime systemTime) : base(systemTime) {}

		public MyTrialContext(ClaimsPrincipal profile) : base(profile) {}

		public static MyTrialContext ImpersonateFromEmail(string targetEmail)
		{
			if (string.IsNullOrWhiteSpace(targetEmail)) throw new ArgumentNullException(nameof(targetEmail));
			var emailClaim = new Claim(ClaimTypes.Email, targetEmail);
			
			var context = new MyTrialContext(new ClaimsPrincipal(new ClaimsIdentity(new[]
			{
				emailClaim,
			}, "Impersonation")));
			return context;
		}

        protected override Guid? OnParseClaimKey(Claim claim)
        {
			if (Guid.TryParse(claim.Value, out Guid result))
				return result;

			return null;
        }
    }

    [TestFixture()]
    public class ApiCallContextExampleTests
    {
        [Test()]
		public void ImpersonateFromEmailExpectSuccess()
		{
			var context = MyTrialContext.ImpersonateFromEmail("user.one@somewhere.com");
			Assert.IsFalse(context.UserId.HasValue);
		}
    }
}

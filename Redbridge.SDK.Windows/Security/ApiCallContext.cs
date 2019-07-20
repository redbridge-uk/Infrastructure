using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Redbridge.SDK;
using Redbridge.SDK.Security;

namespace Redbridge.Security
{
    public abstract class ApiCallContext<TContext, TUserKey> : ApiCallContext
    where TContext : IApiCallContext, new()
    where TUserKey : struct
    {
        public static TContext Anonymous => new TContext();

        protected ApiCallContext() : base() { }
        protected ApiCallContext(DateTime systemTime) : base(systemTime) {}
        protected ApiCallContext(ClaimsPrincipal profile) : base(profile) {}
        protected ApiCallContext(CultureInfo cultureInfo, ClaimsPrincipal profile) : base(cultureInfo, profile) { }
        protected ApiCallContext(DateTime systemTime, CultureInfo cultureInfo, ClaimsPrincipal profile) : base(systemTime, cultureInfo, profile) { }

        public TUserKey? UserId
        {
            get
            {
                var claim = Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return claim != null ? OnParseClaimKey(claim) : null;
            }
        }

        protected abstract TUserKey? OnParseClaimKey(Claim claim);

		public void AuditCreate(IAudited<TUserKey> auditedType)
		{
			if (!IsAuthenticated) throw new SecurityAuthenticationException("The current user is not authorized.");
			if (!UserId.HasValue) throw new SecurityAuthenticationException("The current user id is not defined.");
			auditedType.Created = SystemTime;
			auditedType.Updated = SystemTime;
			auditedType.CreatedBy = UserId.Value;
			auditedType.UpdatedBy = UserId.Value;
		}

		public void AuditUpdate(IAudited<TUserKey> auditedType)
		{
			if (!IsAuthenticated) throw new SecurityAuthenticationException("The current user is not authorized.");
			if (!UserId.HasValue) throw new SecurityAuthenticationException("The current user id is not defined.");
			auditedType.Updated = SystemTime;
			auditedType.UpdatedBy = UserId.Value;
		}

		public void AuditDelete(IAudited<TUserKey> auditedType)
		{
			if (!IsAuthenticated) throw new SecurityAuthenticationException("The current user is not authorized.");
			if (!UserId.HasValue) throw new SecurityAuthenticationException("The current user is not configured correctly.");
			auditedType.Updated = SystemTime;
			auditedType.UpdatedBy = UserId.Value;
			auditedType.Deleted = SystemTime;
			auditedType.DeletedBy = UserId;
		}
    }

    public abstract class ApiCallContext : IApiCallContext
    {
        private readonly ClaimsPrincipal _principal;

        protected ApiCallContext() : this(DateTime.UtcNow) 
        {
            Culture = new CultureInfo("en-GB");
		}

        protected ApiCallContext(DateTime systemTime)
        {
            Culture = new CultureInfo("en-GB");
            SystemTime = systemTime;
        }

        protected ApiCallContext(ClaimsPrincipal profile) : this()
        {
            _principal = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        protected ApiCallContext(CultureInfo cultureInfo, ClaimsPrincipal profile)
        {
            _principal = profile ?? throw new ArgumentNullException(nameof(profile));
            Culture = cultureInfo;
            SystemTime = DateTime.UtcNow;
        }

        protected ApiCallContext(DateTime systemTime, CultureInfo cultureInfo, ClaimsPrincipal profile)
        {
            _principal = profile ?? throw new ArgumentNullException(nameof(profile));
            Culture = cultureInfo;
            SystemTime = systemTime;
        }

        public bool IsAuthenticated => _principal?.Identity != null && _principal.Identity.IsAuthenticated;

        public CultureInfo Culture { get; private set; }
        public DateTime SystemTime { get; }

        protected ClaimsPrincipal Principal => _principal;

        public string FirstName
        {
            get
            {
                var claim = _principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
                return claim?.Value;
            }
        }

        public string Surname
        {
            get
            {
                var claim = _principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
                return claim?.Value;
            }
        }

        public virtual string FullName => $"{FirstName} {Surname}";

        public string EmailAddress
        {
            get
            {
                var claim = _principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                return claim?.Value;
            }
        }

        public object GetFormat(Type formatType)
        {
            return Culture.GetFormat(formatType);
        }
    }
}

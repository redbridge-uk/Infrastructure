using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Redbridge.ApiManagement;
using Redbridge.Data;

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
        protected ApiCallContext(DateTime systemTime, ClaimsPrincipal profile) : base(systemTime, profile) { }

        public TUserKey? UserId
        {
            get
            {
                var claim = Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return claim != null ? OnParseClaimKey(claim) : null;
            }
        }

        protected abstract TUserKey? OnParseClaimKey(Claim claim);

		public T AuditCreate<T>(T auditedType) where T: IAudited<TUserKey>
        {
			if (!IsAuthenticated) throw new SecurityAuthenticationException("The current user is not authorized.");
			if (!UserId.HasValue) throw new SecurityAuthenticationException("The current user id is not defined.");
			auditedType.Created = SystemTime;
			auditedType.Updated = SystemTime;
			auditedType.CreatedBy = UserId.Value;
			auditedType.UpdatedBy = UserId.Value;
            return auditedType;
        }

		public T AuditUpdate<T>(T auditedType) where T : IAudited<TUserKey>
        {
			if (!IsAuthenticated) throw new SecurityAuthenticationException("The current user is not authorized.");
			if (!UserId.HasValue) throw new SecurityAuthenticationException("The current user id is not defined.");
			auditedType.Updated = SystemTime;
			auditedType.UpdatedBy = UserId.Value;
            return auditedType;
        }

		public T AuditDelete<T>(T auditedType) where T : IAudited<TUserKey>
        {
			if (!IsAuthenticated) throw new SecurityAuthenticationException("The current user is not authorized.");
			if (!UserId.HasValue) throw new SecurityAuthenticationException("The current user is not configured correctly.");
			auditedType.Updated = SystemTime;
			auditedType.UpdatedBy = UserId.Value;
			auditedType.Deleted = SystemTime;
			auditedType.DeletedBy = UserId;
            return auditedType;
        }
    }

    public abstract class ApiCallContext : IApiCallContext
    {
        private readonly ClaimsPrincipal _principal;

        protected ApiCallContext() : this(DateTime.UtcNow) 
        {
		}

        protected ApiCallContext(DateTime systemTime)
        {
            SystemTime = systemTime;
        }

        protected ApiCallContext(ClaimsPrincipal profile) : this()
        {
            _principal = profile ?? throw new ArgumentNullException(nameof(profile));
        }

        protected ApiCallContext(DateTime systemTime, ClaimsPrincipal profile)
        {
            _principal = profile ?? throw new ArgumentNullException(nameof(profile));
            SystemTime = systemTime;
        }

        public bool IsAuthenticated => _principal?.Identity != null && _principal.Identity.IsAuthenticated;

        public virtual CultureInfo Culture
        {
            get
            {
                var cultureClaim = _principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country);
                return cultureClaim != null
                    ? CultureInfo.GetCultureInfo(cultureClaim.Value)
                    : CultureInfo.GetCultureInfo("en-GB");
            }
        }

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

using System;
using System.Net;
using System.Threading.Tasks;
using Redbridge.IO;
using Redbridge.Web.Messaging;

namespace Redbridge.IntegrationTesting
{
    public class UserSession : IDisposable
	{
		private readonly IWebRequestSignatureService _sessionManager;

		public UserSession(IWebRequestSignatureService sessionManager, ObjectFactory objectFactory, string name, ITestScenario scenario)
		{
			if (sessionManager == null) throw new ArgumentNullException(nameof(sessionManager));
			if (objectFactory == null) throw new ArgumentNullException(nameof(objectFactory));
			if (scenario == null) throw new ArgumentNullException(nameof(scenario));
			if (string.IsNullOrWhiteSpace(name)) throw new NotSupportedException("A name must be supplied.");

			_sessionManager = sessionManager;
			ObjectFactory = objectFactory;
			Scenario = scenario;
			Name = name;
		}

		public string EmailAddress => _sessionManager.Authority.Username;

		public bool IsConnected => _sessionManager.Authority.IsConnected;

		public string Name { get; }

		protected ITestScenario Scenario { get; }

		protected ObjectFactory ObjectFactory { get; }

		public async Task<UserSession> Connect()
		{
			if (string.IsNullOrEmpty(EmailAddress))
			{
				// Need to look it up from the local session store instead.
				throw new NotImplementedException("Unable to look up adminsitrator details from configuration and no lookup strategy written yet.");
			}

			await _sessionManager.Authority.BeginLoginAsync();
			await OnAfterConnectionAsync();
			return this;
		}

		protected virtual Task OnAfterConnectionAsync() 
		{
			return Task.FromResult(true);
		}

		public async Task<string> VisitLink(string linkUrl)
		{
			try
			{
				var request = WebRequest.CreateHttp(linkUrl);
				var response = await request.GetResponseAsync();
				return response.GetResponseStream().AsString();
			}
			catch (Exception e)
			{
				throw new ApplicationException($"Failed to fetch page contents for url {linkUrl}", e);
			}
		}

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _sessionManager?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserSession() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

using System;
using System.IO;
using Redbridge.Configuration;
using Redbridge.Console.Diagnostics;
using Redbridge.Diagnostics;
using Redbridge.IO;
using Redbridge.Web.Messaging;

namespace Redbridge.IntegrationTesting
{
    public abstract class TestScenario<TUserSession> : ITestScenario
		where TUserSession: UserSession
	{
		private readonly UserSessionCollection _sessionCollection = new UserSessionCollection();

		private readonly EmbeddedResourceReader _resourceReader = new EmbeddedResourceReader(typeof(TUserSession).Assembly);
		private readonly IApplicationSettingsRepository _applicationSettingsRepository = new WindowsApplicationSettingsRepository();

        protected TestScenario() : this(string.Empty) { }
        protected TestScenario(string scenarioCode) : this(scenarioCode, string.Empty) { }

        protected TestScenario(string scenarioCode, string description)
		{
            ScenarioCode = scenarioCode ?? throw new ArgumentNullException(nameof(scenarioCode));
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Logger = new ConsoleLogWriter();
			var administratorUser = _applicationSettingsRepository.GetStringValue("administratorUser");
			var administratorPassword = _applicationSettingsRepository.GetStringValue("administratorPassword");
			Administrator = CreateSession("Administrator", false, administratorUser, administratorPassword);
			Anonymous = CreateSession("Anonymous", false);
		}

		public string ScenarioCode { get; }
		public string Description { get; }
		public ILogger Logger { get; }
		public UserSession Administrator { get; }
		public UserSession Anonymous { get; }
		public UserSession this[string name] => _sessionCollection[name];
		public UserSessionCollection Sessions => _sessionCollection;

		public void Dispose()
		{
			_sessionCollection?.Dispose();
		}

		protected abstract string DomainName { get; }

		public virtual string CreateRandomEmailAddress()
		{
			return Guid.NewGuid().ToString().Replace("-", "") + $"@{DomainName}";
		}

		public UserSession CreateSession(string sessionName)
		{
			return CreateSession(sessionName, true);
		}

		protected abstract IWebRequestSignatureService CreateSessionManager();

		private UserSession CreateSession(string sessionName, bool addToSessions, string username = "", string password = "")
		{
			if (string.IsNullOrWhiteSpace(sessionName)) throw new ArgumentNullException(nameof(sessionName));
			var session = CreateUserSession();
			if (addToSessions) _sessionCollection.Add(session);
			return session;
		}

		protected abstract TUserSession CreateUserSession();

		public void VerifyAll() { }

		public Stream GetEmbeddedResourceStream (string path)
		{
			return _resourceReader.GetStream(path);
		}
	}
}

using System;
using Redbridge.Diagnostics;

namespace Redbridge.IntegrationTesting
{
    public interface ITestScenario : IDisposable
    {
        string ScenarioCode { get; }
        string Description { get; }
        ILogger Logger { get; }
        UserSession Administrator { get; }
        UserSession Anonymous { get; }
        UserSession this[string name] { get; }
        UserSessionCollection Sessions { get; }
    }
}
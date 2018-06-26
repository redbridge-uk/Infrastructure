using System;
namespace Redbridge.Identity
{
    public interface IExternalAuthenticationProvider
    {
        string ProviderName { get; }
    }
}

using System;
using Microsoft.Extensions.Logging;
using Redbridge.DependencyInjection;

namespace Redbridge.Identity
{
    public class ContainerAuthenticationClientFactory : NamedContainerFactory<IAuthenticationClient>, IAuthenticationClientFactory
    {
        public ContainerAuthenticationClientFactory(IContainer container, ILogger logger) : base(container, logger)
        {
        }
    }
}

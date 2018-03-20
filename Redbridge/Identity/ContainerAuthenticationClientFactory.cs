using System;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;

namespace Redbridge.Identity
{
    public class ContainerAuthenticationClientFactory : NamedContainerFactory<IAuthenticationClient>, IAuthenticationClientFactory
    {
        public ContainerAuthenticationClientFactory(IContainer container, ILogger logger) : base(container, logger)
        {
        }
    }
}

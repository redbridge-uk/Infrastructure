using Redbridge.ApiManagement;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;

namespace Redbridge.SDK
{
    public abstract class ContainerUnitOfWorkFactory<TUnit> : ContainerFactory<TUnit>, IUnitOfWorkFactory<TUnit>
        where TUnit : IWorkUnit
    {
        public ContainerUnitOfWorkFactory(IContainer resolver, ILogger logger) : base(resolver, logger) {}
    }
}

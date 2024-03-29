﻿using Microsoft.Extensions.Logging;

namespace Redbridge.DependencyInjection
{
    public abstract class ContainerUnitOfWorkFactory<TUnit> : ContainerFactory<TUnit>, IUnitOfWorkFactory<TUnit>
        where TUnit : IWorkUnit
    {
        protected ContainerUnitOfWorkFactory(IContainer resolver, ILogger logger) : base(resolver, logger) {}
    }
}

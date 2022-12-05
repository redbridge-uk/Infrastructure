using System;
using Microsoft.Extensions.Logging;
using Redbridge.Diagnostics;

namespace Redbridge.DependencyInjection
{

	public abstract class ContainerFactory<T>
	{
		private readonly IContainer _resolver;
        private readonly ILogger _logger;

        protected ContainerFactory(IContainer resolver, ILogger logger)
		{
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
		}

        protected ILogger Logger => _logger;

		public T Create()
		{
            try
            {
                return _resolver.Resolve<T>();
            }
			catch (Exception e)
			{
                _logger.LogError("Unable to resolve instance of type {0}. Exception messsage: {1}", typeof(T), e.Message);
				throw e;
			}
		}
	}
}

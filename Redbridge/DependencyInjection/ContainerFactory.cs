using System;
using Redbridge.Diagnostics;

namespace Redbridge.DependencyInjection
{

	public abstract class ContainerFactory<T>
	{
		private readonly IContainer _resolver;
        private readonly ILogger _logger;

		public ContainerFactory(IContainer resolver, ILogger logger)
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
                _logger.WriteError($"Unable to resolve instance of type {typeof(T)}. Exception messsage: {e.Message}");
				throw e;
			}
		}
	}
}

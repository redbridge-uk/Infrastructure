using System;
using Microsoft.Extensions.Logging;

namespace Redbridge.DependencyInjection
{
    public abstract class NamedContainerFactory<T>
    {
        private readonly IContainer _resolver;
        private readonly ILogger _logger;

        protected NamedContainerFactory(IContainer resolver, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        protected ILogger Logger => _logger;

        public T Create(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));

            try
            {
                _logger.LogDebug($"NamedContainerFactory: attempting to resolve named type {name}");
                return _resolver.Resolve<T>(name);
            }
            catch (Exception e)
            {
                _logger.LogError($"Unable to resolve named instance of type {typeof(T)}. Exception messsage: {e.Message}");
                throw e;
            }
        }
    }
}

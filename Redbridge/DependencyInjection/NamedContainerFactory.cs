using System;
using Redbridge.Diagnostics;

namespace Redbridge.DependencyInjection
{
    public abstract class NamedContainerFactory<T>
    {
        private readonly IContainer _resolver;
        private readonly ILogger _logger;

        public NamedContainerFactory(IContainer resolver, ILogger logger)
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
                _logger.WriteDebug($"NamedContainerFactory: attempting to resolve named type {name}");
                return _resolver.Resolve<T>(name);
            }
            catch (Exception e)
            {
                _logger.WriteError($"Unable to resolve named instance of type {typeof(T)}. Exception messsage: {e.Message}");
                throw e;
            }
        }
    }
}

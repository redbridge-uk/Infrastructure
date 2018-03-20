using System;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;

namespace Redbridge.Forms
{
	public class ViewModelFactory : IViewModelFactory
	{
		private readonly IContainer _container;
        private readonly ILogger _logger;

        public ViewModelFactory(IContainer container, ILogger logger)
		{
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            if (container == null) throw new ArgumentNullException(nameof(container));
            _logger = logger;
			_container = container;
		}

		public T CreateModel<T>() where T:IViewModel
		{
			return _container.Resolve<T>();
		}

		public IViewModel CreateModel(Type type)
		{
            try
            {
                _logger.WriteDebug($"Attempting to create page for model type {type.FullName}...");
                return (IViewModel)_container.Resolve(type);
            }
            catch (Exception e)
            {
				_logger.WriteError($"Failed to create model for type {type.FullName} due to error {e.Message}.");
				throw new ViewModelFactoryException($"Unable to create view model type {type.FullName}. Error message given is {e.Message}. Ensure your viewmodel components are registered with the container and check the inner exception for more details.", e);
            }
		}

		public IViewModel CreateModel(Type type, string name)
		{
			return (IViewModel)_container.Resolve(type, name);
		}
	}
}

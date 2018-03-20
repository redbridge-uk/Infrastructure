using System;
using Redbridge.DependencyInjection;
using Redbridge.Diagnostics;
using Xamarin.Forms;

namespace Redbridge.Forms
{
	public class ViewFactory : IViewFactory
	{
		private readonly IContainer _container;
		private readonly ILogger _logger;

		public ViewFactory(IContainer container, ILogger logger)
		{
			if (logger == null) throw new ArgumentNullException(nameof(logger));
			if (container == null) throw new ArgumentNullException(nameof(container));
			_container = container;
			_logger = logger;
		}

		public Page CreatePage(object model, bool ignoreGenerics = false)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			var modelTypeName = model.GetType().Name;

            if (ignoreGenerics )
            {
                var genericPointReference = modelTypeName.IndexOf("`", StringComparison.Ordinal);

                if ( genericPointReference > 0 )
                    modelTypeName = modelTypeName.Substring(0, genericPointReference);
            }

			try
			{
				_logger.WriteDebug($"Attempting to create page for model type {modelTypeName}...");
				var page = _container.Resolve<IView>(modelTypeName);
				page.BindingContext = model;
				return (Page)page;
			}
			catch (Exception e)
			{
				_logger.WriteError($"Failed to create page for model type {modelTypeName} due to error {e.Message}.");
				throw new ViewFactoryException($"Unable to create view from model type {modelTypeName}. Error message given is {e.Message}. Ensure your view is registered with the container and check the inner exception for more details.", e);
			}
		}

		public Page CreatePageFromModel<T>()
		{
			var model = _container.Resolve<T>();
			return CreatePage(model);
		}
	}
}

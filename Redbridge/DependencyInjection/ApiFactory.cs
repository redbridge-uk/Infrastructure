using System;
using Redbridge.ApiManagement;
using Redbridge.DependencyInjection;

namespace Redbridge.SDK
{
	public class ApiFactory : IApiFactory
	{
		private readonly IContainer _container;

		public ApiFactory(IContainer resolver)
		{
            _container = resolver ?? throw new ArgumentNullException(nameof(resolver));
		}

		public T CreateApi<T>() 
			where T : class, IApiCall
		{
			var instance = _container.Resolve<T>();
            if (instance == null) throw new DependencyResolutionException($"Dependency resolution failure: could not resolve type {typeof(T).Name}");
			return instance;
		}
	}
}

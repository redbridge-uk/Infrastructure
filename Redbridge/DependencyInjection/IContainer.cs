using System;
using System.Collections.Generic;

namespace Redbridge.DependencyInjection
{
	public interface IContainer
	{
		void RegisterInstance<T>(T instance);
		T Resolve<T>();
		T Resolve<T>(string name);
		IEnumerable<T> ResolveAll<T>();
}
}

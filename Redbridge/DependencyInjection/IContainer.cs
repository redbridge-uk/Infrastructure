using System;
using System.Collections.Generic;

namespace Redbridge.DependencyInjection
{
	public interface IContainer
	{
		void RegisterInstance(object instance);
		void RegisterInstance<T>(T instance);
		T Resolve<T>();
        T Resolve<T>(ContainerDependencyOverride dependencyOverride);
		T Resolve<T>(string name);
		IEnumerable<T> ResolveAll<T>();
        void RegisterType<T>(Type type, string name);
		void RegisterType<T, T1>(LifeTime lifeTime = LifeTime.Transient) where T1 : T;
		void RegisterType<T, T1>(string name, LifeTime lifeTime = LifeTime.Transient) where T1 : T;
		object Resolve(Type type);
		object Resolve(Type type, string name);
}
}

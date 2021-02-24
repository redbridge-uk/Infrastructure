using System;
using System.Collections.Generic;
using Redbridge.DependencyInjection;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Redbridge.Unity
{
    public class UnityContainerResolver : IContainer
	{
		private readonly IUnityContainer _container;

		public static UnityContainerResolver New()
		{
			return new UnityContainerResolver(new UnityContainer());
		}

		public UnityContainerResolver(IUnityContainer container)
		{
            _container = container ?? throw new ArgumentNullException(nameof(container));
		}

		public void RegisterInstance<T>(T instance)
		{
			_container.RegisterInstance<T>(instance);
		}

		public void RegisterInstance(object instance)
		{
			_container.RegisterInstance(instance);
		}

		public void RegisterType<T, T1>() where T1 : T
		{
			_container.RegisterType<T, T1>();
		}

		public void RegisterType<T, T1>(LifeTime lifeTime = LifeTime.Transient) where T1 : T
		{
			switch (lifeTime)
			{
				case LifeTime.Transient:
					_container.RegisterType<T, T1>();
					break;
					
				case LifeTime.Container:
					_container.RegisterType<T, T1>(new ContainerControlledLifetimeManager());
					break;

				case LifeTime.Hierarchical:
					_container.RegisterType<T, T1>(new HierarchicalLifetimeManager());
					break;
			}

		}

		public void RegisterType<T, T1>(string name) where T1 : T
		{
			_container.RegisterType<T, T1>(name);
		}

		public void RegisterType<T, T1>(string name, LifeTime lifeTime = LifeTime.Transient) where T1 : T
		{
			switch (lifeTime)
			{
				case LifeTime.Transient:
					_container.RegisterType<T, T1>(name);
					break;

				case LifeTime.Container:
					_container.RegisterType<T, T1>(name, new ContainerControlledLifetimeManager());
					break;

				case LifeTime.Hierarchical:
					_container.RegisterType<T, T1>(name, new HierarchicalLifetimeManager());
					break;
			}
		}

		public object Resolve(Type type)
		{
			return _container.Resolve(type);
		}

		public object Resolve(Type type, string name)
		{
			return _container.Resolve(type, name);
		}

		public T Resolve<T>()
		{
			return _container.Resolve<T>();
		}

		public T Resolve<T>(ContainerDependencyOverride dependencyOverride)
		{
            if (dependencyOverride == null) throw new ArgumentNullException(nameof(dependencyOverride));
            var overrider = new DependencyOverride(dependencyOverride.Type, dependencyOverride.Instance);
            return _container.Resolve<T>(overrider);
		}

		public T Resolve<T>(string name)
		{
			return _container.Resolve<T>(name);
		}

		public IEnumerable<T> ResolveAll<T>()
		{
			return _container.ResolveAll<T>();
		}

        public void RegisterType<T>(Type type, string name)
        {
            _container.RegisterType(typeof(T), type, name);
        }
	}
}

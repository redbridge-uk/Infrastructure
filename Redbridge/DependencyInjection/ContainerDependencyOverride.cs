using System;
namespace Redbridge.DependencyInjection
{
	public class ContainerDependencyOverride
	{
		public ContainerDependencyOverride(Type type, object instance)
		{
            Type = type ?? throw new ArgumentNullException(nameof(type));
			Instance = instance ?? throw new ArgumentNullException(nameof(instance));
		}

        public Type Type { get; private set; }
        public object Instance { get; private set; }
	}


    public class ContainerDependencyOverride<T> : ContainerDependencyOverride
    {
        public ContainerDependencyOverride(T instance) : base(typeof(T), instance)
        {
            if (instance.Equals(default(T))) throw new ArgumentNullException(nameof(instance));
        }
    }

    public class ContainerInjectionMember
    {
        public Type Type { get; private set; }
        public Func<object> InstanceFactory { get; private set; }

        public ContainerInjectionMember (Type type, Func<object> instanceFactory)
        {
            Type = type;
            InstanceFactory = instanceFactory ?? throw new ArgumentNullException(nameof(instanceFactory));
        }
    }
}

using System;
namespace Redbridge.DependencyInjection
{
	public class ContainerDependencyOverride
	{
		public ContainerDependencyOverride(Type type, object instance)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));
			if (instance == null) throw new ArgumentNullException(nameof(instance));
			Type = type;
			Instance = instance;
		}

		public Type Type { get; }
		public object Instance { get; }
	}
}

using System;
using Unity;

namespace Redbridge.Unity
{
    public static class UnityContainerExtensions
    {
        public static UnityContainerResolver ToResolver (this IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return new UnityContainerResolver(container);
        }
    }
}
using System;
using System.Collections.Generic;
using Redbridge.SDK;

namespace Redbridge.DependencyInjection
{
	public enum LifeTime
	{
		Transient,

		Container,

		Hierarchical,

		PerThread,
	}
	
}

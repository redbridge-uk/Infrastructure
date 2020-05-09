using System;
using System.Collections.Generic;

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

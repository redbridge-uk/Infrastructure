using System;
using System.Collections.ObjectModel;

namespace Redbridge.IntegrationTesting
{
	public class UserSessionCollection : KeyedCollection<string, UserSession>, IDisposable
	{
		protected override string GetKeyForItem(UserSession item)
		{
			return item.Name;
		}

		public void Dispose() { }
	}
}
